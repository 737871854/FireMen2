local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 
-- 测试
local LoginState = {
	kUninit = 1,
	KTryLogin = 2,
	KFinish = 3,
	KError = 4,
}
--local LoginState = Import("logic/game").LoginState


CLoginScene = class(SceneBase)

CLoginScene.Init = function(self)
	self:SetLoginState(LoginState.kUninit)
	self._http_path = "http://192.168.0.21/q5/server_config.json"
	self._www = nil
	self._errorMsg = ""
	self._hasShowError = false
	self._selectHostId = nil
	self._account = nil
	self._password = nil
end

CLoginScene.OnLevelWasLoaded = function(self)
end

CLoginScene.GetLoginState = function (self)
	return self._loginState
end

CLoginScene.SetLoginState = function (self, state)
	self._loginState = state
end

CLoginScene.OnUpdate = function(self)
	if self:GetLoginState() == LoginState.kUninit then
		if self._www ~= nil then
			self._www:Dispose()
			self._www = nil
		end
		self._www = UnityEngine.WWW.New(self._http_path)
		self:SetLoginState(LoginState.kDownloadServerConfig)
		print("start download server config "..self._http_path)
	elseif self:GetLoginState() == LoginState.kDownloadServerConfig then
		if not self._www.isDone then return end
		if self._www.error == nil or self._www.error == "" then
			print("download server config ok :", self._www.text)
			local data = Tool.Json2Table(self._www.text) or {}
			gGame:SetServerConfig(data)
			gGame:GetUIMgr():OpenMain("Server")
			self:SetLoginState(LoginState.kWaitForSelectServer)
			print("wait select server")
		else
			self._errorMsg = "download server config error "..self._www.error	
			self:SetLoginState(LoginState.kError)
		end
		self._www:Dispose()
		self._www = nil
	elseif self:GetLoginState() == LoginState.kWaitForConnectServer then
		local errorMsg = ioo.networkManager:GetErrorMsg()
		if errorMsg ~= "" then
			local str = string.format("悲剧，连不上服务器，"..errorMsg);
			gGame:GetUIManager():OpenAlertDialog("错误", str, "确定", nil, true);
			ioo.networkManager:Logout()
			self:SetLoginState(LoginState.kWaitForSelectServer)
			return;
		end
		if not ioo.networkManager:IsConnectSuccess() then return end
		print("connect success, wait for input account and password")
		gGame:GetUIManager():GetCurMainLogic():ShowSwitch(false)
		local uiLogin = gGame._uiManager:OpenMain("Login")
		uiLogin:SetHostid(self._selectHostId)
		self:UploadClientVersion()
		self:SetLoginState(LoginState.kCheckVersion)
	elseif self:GetLoginState() == LoginState.kCheckVersion then

	elseif self:GetLoginState() == LoginState.kWaitForInputUserInfo then

	elseif self:GetLoginState() == LoginState.kError then
		if self._hasShowError then return end
		self._hasShowError = true
		gGame:GetUIManager():OpenAlertDialog("错误", self._errorMsg, "确定", nil, true);
	elseif self:GetLoginState() == LoginState.kWaitForEnterScene then
	elseif self:GetLoginState() == LoginState.kTryReconnect then
		self:CheckAccountAndPassword(gGame:GetAccount(), gGame:GetPassword())
	elseif self:GetLoginState() == LoginState.kEnterScene then
		self:EnterScene()
		self:SetLoginState(LoginState.kFinish)
	end
end

CLoginScene.UploadClientVersion = function(self)
	gGame:GetProtocol().rpc_server_version(Tool.GetVersion(), gGame:GetRpc():GetPtoMd5() )
end

CLoginScene.CheckVersionFinish = function(self, res, encoding)
	self:SetLoginState(LoginState.kWaitForInputUserInfo)
	if res == 1 then
		gGame:GetUIManager():OpenFloatDialog("协议版本不对");
	end
end

CLoginScene.ConnectServer = function(self, serverIndex)
	if self:GetLoginState() ~= LoginState.kWaitForSelectServer then
		warn(string.format("登陆阶段出错？卡住了? %d", self:GetLoginState()))
		return
	end
	local config = gGame:GetServerConfig()[serverIndex]
	assert(config ~= nil, serverIndex)
	ioo.networkManager:Logout()
    ioo.networkManager:SendConnect(config.addr, config.port)
	self._selectHostId = config.hostid 
	self:SetLoginState(LoginState.kWaitForConnectServer)
end

CLoginScene.ReturnToSelectServer = function(self)
	if self:GetLoginState() <= LoginState.kWaitForConnectServer then
		return
	else
		ioo.networkManager:Logout()
		gGame:GetUIManager():GetCurMainLogic():ShowSwitch(false)
		gGame:GetUIManager():OpenMain("Server")
		self._selectHostId = nil
		self:SetLoginState(LoginState.kWaitForSelectServer)
		print("connection loss, select server again")
	end
end

CLoginScene.CheckAccountAndPassword = function(self, account, password)
	if self:GetLoginState() ~= LoginState.kWaitForInputUserInfo 
	and self:GetLoginState() ~= LoginState.kTryReconnect
		then return 
	end
	--account = "jyq0047"
	-- account = "lk0300"
	self._account = account
	self._password = password
	gGame:GetProtocol().rpc_server_login({1, 2, self._selectHostId, account, "4", "5", "6", account, "8", Util.GetLocalIPAddress(), {"1", 2, "3", "4"}})
	self:SetLoginState(LoginState.kWaitForCheckUserInfo)
	print("wait for check user account")
end

CLoginScene.CheckAccountAndPasswordCb = function(self, result)
	if result["result"] ~= 0 then 
		local str = string.format("登陆失败,错误代码:%d, 信息:%s", result["result"], result["msg"]);
		gGame:GetUIManager():OpenAlertDialog("错误", str, "确定", function() print("ok") end, true);
		self:SetLoginState(LoginState.kWaitForInputUserInfo)
	else
		gGame:GetUIManager():GetCurMainLogic():ShowSwitch(false)
	end
end

CLoginScene.LoginFinish = function(self)
	gGame:SetLoginFinish(true)
	self:SetLoginState(LoginState.kEnterScene)
end

CLoginScene.EnterScene = function (self)
	gGame:ChangeScene("main")
end

CLoginScene.SetStateReconnect = function(self)
	self:SetLoginState(LoginState.kTryReconnect)
end
