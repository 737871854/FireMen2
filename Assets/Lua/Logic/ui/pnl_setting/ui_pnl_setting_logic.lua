local CUIBaseLogic = Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUISettingView = Import("logic/ui/pnl_setting/ui_pnl_setting_view").CUISettingView
local EventDefine = Import("logic/base/event_define").Define
local SettingDefine = Import("logic/base/setting_define").Define
local Tool = Import("logic/common/tool").CTool

CUISettingLogic = class(CUIBaseLogic)

--构造函数，初始化View，绑定事件等
CUISettingLogic.Init = function(self, id)
    self._prefabPath = "prefabs\\ui\\panel_setting"
    self._view = CUISettingView:New(self._prefabPath)
	
	-- 0：主页 1：设置页 2：查账页
	self._curPage = 0
	-- 当前页选中项
	self._index = 0
	-- 锁定
	self._lock = false
	
    CUIBaseLogic.Init(self, id)	
end

--绑定UI事件监听
CUISettingLogic.BindUIEvent = function(self)
	--注册 IO消息
	IOLuaHelper.Instance:RegesterListener(2, LuaHelper.OnIOEventHandle(function() self:HandleButtonA() end), "pnl_setting_A")
	IOLuaHelper.Instance:RegesterListener(3, LuaHelper.OnIOEventHandle(function() self:HandleButtonB() end), "pnl_setting_B")
	
	--注册语言更改消息
	RegTrigger(EventDefine.Setting_Data_Language, function() self:RefreshSetInfo() end, "pnl_setting")
end

CUISettingLogic.OnCreate = function(self)
	self._settingData = gGame:GetDataMgr():GetSettingData()
   
	self._rate 		= self._settingData._rate
	self._hasCoin 	= self._settingData._hasCoin
	self._rate 		= self._settingData._rate
	self._volume	= self._settingData._volume
	self._level		= self._settingData._level
	self._language  = self._settingData._language
	self._clearCoin		= false
	self._clearAccount	= false
	self:RefreshSetInfo()
end

CUISettingLogic.OnDestroy = function(self)
   --移除 IO消息
   IOLuaHelper.Instance:RemoveListener(2, "pnl_setting_A")
   IOLuaHelper.Instance:RemoveListener(3, "pnl_setting_B")
  
   --移除语言更改消息
   RegTrigger(EventDefine.Setting_Data_Language, "pnl_setting")
  
   GameObject.Destroy(self._view._root)
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 被注册的方法 Begine--------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--设置按钮A操作
CUISettingLogic.HandleButtonA = function(self)
	-- 主页
	if self._curPage == 0 then
		self:OPInHomePage_A()
	-- 设置页
	elseif self._curPage == 1 then
		self:OPInSetPage_A()
	-- 查账页
	elseif self._curPage == 2 then
		self:OPAccountPage_A()
	else
	
	end
end

--设置按钮B操作
CUISettingLogic.HandleButtonB = function(self)
	if not self._lock then
		self._index = self._index + 1
		-- 主页
		if self._curPage == 0 then
			self._index = self._index % (#self._view._homeCheckMark + 1)
		-- 设置页
		elseif self._curPage == 1 then
			if self._view._setMain.gameObject.activeSelf then
				self._index = self._index % (#self._view._setCheckMark + 1)
			end			
		elseif self._curPage == 2 then
			self._index = self._index % (#self._view._accountCheckMark + 1)		
		end
		
		self:SelectPageItem()
		
	-- 目前能锁定选项的页面只有设置页面
	else
	    -- 币率
		if self._index == 0 then
			self._rate = self._rate + 1
			self._rate = self._rate % self._settingData._maxRate			
			self._settingData:SetGameRate(self._rate)
			self._view._setValue[self._index].text = self._rate
		-- 音量
		elseif self._index == 1 then
			self._volume = self._volume + 1			
			self._volume = self._volume % self._settingData._maxVolume			
			self._settingData:SetGameVolume(self._volume)
			self._view._setValue[self._index].text = self._volume
		-- 难度
		elseif self._index == 2 then
			self._level = self._level + 1
			self._level = self._level % self._settingData._maxLevel
			self._settingData:SettGameLevel(self._level)
			self._view._setValue[self._index].text = self._level
		-- 语言
		elseif self._index == 3 then
			self._language = self._language + 1
			self._language = self._language % self._settingData._maxLanguage
			self._settingData:SetGameLanguage(self._language)
			self._view._setValue[self._index].text = SettingDefine.Lanauage[self._language]
		-- 出票
		elseif self._index == 4 then
			
		-- 清除查账 投币
		elseif self._index == 5 then
			self._clearCoin = not self._clearCoin
			if self._clearCoin then
				self._view._setValue[self._index].text = SettingDefine.YesNo[self._language][0]
			else
				self._view._setValue[self._index].text = SettingDefine.YesNo[self._language][1]
			end			
		elseif self._index == 6 then
			self._clearAccount = not self._clearAccount
			if self._clearAccount then
				self._view._setValue[self._index].text = SettingDefine.YesNo[self._language][0]
			else
				self._view._setValue[self._index].text = SettingDefine.YesNo[self._language][1]
			end			
		end
	end
	
end
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 内部使用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 主页A操作
CUISettingLogic.OPInHomePage_A = function(self)
	if self._index == 0 then
		self._curPage = 1
		self:ShowPage()
	elseif self._index == 1 then
		self._curPage = 2
		self:ShowPage()
	else
		self._curPage 	= 0
		self._index 	= 0
		self:Exit()
	end
end

-- 设置页A操作
CUISettingLogic.OPInSetPage_A = function(self)
	if self._index < #self._view._setLable - 3 then
		self._lock = not self._lock
		self:SetSelectItemColor()	
	elseif self._index == #self._view._setLable - 3 then
		self._lock = not self._lock
		self:SetSelectItemColor()	
		if self._clearCoin and not self._lock then
			self._settingData:ClearCoin()
		end
	elseif self._index == #self._view._setLable - 2 then
		self._lock = not self._lock
		self:SetSelectItemColor()
		if self._clearAccount and not self._lock then
			self._settingData:ClearAccount()
		end
	else
		-- 射击校验页
		if self._index == #self._view._setCheckMark - 1 then
			self._view._page[self._curPage].gameObject:SetActive(false)
			self._view._shotPage.gameObject:SetActive(true)
		-- 返回主页
		else
			self._curPage = 0
			self:ShowPage()
		end
	end
end

-- 查账页A操作
CUISettingLogic.OPAccountPage_A = function(self)
	if self._view._accountMain.gameObject.activeSelf then
		if self._index == 0 then
			self._view._accountMain.gameObject:SetActive(false)
			self._view._account.gameObject:SetActive(true)
		elseif self._index == 1 then
			self._view._accountMain.gameObject:SetActive(false)
			self._view._totalPage.gameObject:SetActive(true)
		else
			self._curPage = 0
			self:ShowPage()
		end
	elseif self._view._account.gameObject.activeSelf then
		self._view._accountMain.gameObject:SetActive(true)
		self._view._account.gameObject:SetActive(false)
	else
		self._view._accountMain.gameObject:SetActive(true)
		self._view._totalPage.gameObject:SetActive(false)
	end	
end

--显示指定页面（主页面，设置页面，查账页面）
CUISettingLogic.ShowPage = function(self)
	self._index = 0
	for i = 0, #self._view._page do
		if i == self._curPage then
			self._view._page[i].gameObject:SetActive(true)
		else
			self._view._page[i].gameObject:SetActive(false)
		end
	end
	
	-- 切换页面后，重置当前选项
	self:SelectPageItem()
end

--切换当前页当前项
CUISettingLogic.SelectPageItem = function(self)
	if self._curPage == 0 then
		for i = 0, #self._view._homeCheckMark do
			if self._index == i then
				self._view._homeCheckMark[i].gameObject:SetActive(true)
			else
				self._view._homeCheckMark[i].gameObject:SetActive(false)
			end
		end
	elseif self._curPage == 1 then
		for i = 0, #self._view._setCheckMark do
			if self._index == i then
				self._view._setCheckMark[i].gameObject:SetActive(true)
			else
				self._view._setCheckMark[i].gameObject:SetActive(false)
			end
		end
	else
		if self._view._setMain.gameObject.activeSelf then
			for i = 0, #self._view._accountCheckMark do
				if self._index == i then
					self._view._accountCheckMark[i].gameObject:SetActive(true)
				else
					self._view._accountCheckMark[i].gameObject:SetActive(false)
				end
			end
		end
	end
	
end

--填充整个页面数据
CUISettingLogic.RefreshSetInfo = function(self)
	local Setting 	=	{}
	if self._settingData._language == 0 then
		Setting = SettingDefine.Chinese
	else 
		Setting = SettingDefine.English
	end
	 
	-- 主页 
	for i = 0, #self._view._homeLable do
		self._view._homeLable[i].text = Setting["homePage"][i]
	end
	
	-- 设置页
	for i = 0, #self._view._setLable do
		self._view._setLable[i].text = Setting["setPage"][i]
	end
	
	-- 查账页
	for i = 0, #self._view._accountLable do
		self._view._accountLable[i].text = Setting["account"][i]
	end
	
	-- 查账子页面-总记录
	for i= 0, #self._view._totalKeys do
		self._view._totalKeys[i].text = Setting["total"][i]
	end
	
	-- 查账子页面-近期记录
	for i = 0, #self._view._accountKeys do
		self._view._accountKeys[i].text = Setting["recent"][i]
	end
	
	self._view._setValue[0].text = self._rate
	self._view._setValue[1].text = self._volume
	self._view._setValue[2].text = self._level
	self._view._setValue[3].text = SettingDefine.Lanauage[self._language]
	if self._clearCoin then
				self._view._setValue[5].text = SettingDefine.YesNo[self._language][0]
			else
				self._view._setValue[5].text = SettingDefine.YesNo[self._language][1]
	end		

	if self._clearAccount then
				self._view._setValue[6].text = SettingDefine.YesNo[self._language][0]
			else
				self._view._setValue[6].text = SettingDefine.YesNo[self._language][1]
	end		 
	
	
	--TODO: 查账信息还待完善
	---最近三个月记录
	local monthRecord = ioo.GetMonthData()
	for i = 0, monthRecord.Count - 1 do
		for j = 0, #self._view._accountValues[i] do
			if j == 0 or j == 1 or j == 2 then
				self._view._accountValues[i][j].text = monthRecord[i][j]
			else
				self._view._accountValues[i][j].text = Tool.Sec2String(monthRecord[i][j])
			end
		end
	end
	
	--总记录信息
	local totalRecord = ioo.TotalRecord()
	for i = 0, #self._view._totalValues do
		if i == 1 or i == 2 then
			self._view._totalValues[i].text = Tool.Sec2String(totalRecord[i])
		else
			self._view._totalValues[i].text = totalRecord[i]
		end		
	end
end

CUISettingLogic.SetSelectItemColor = function(self)
	if self._lock then
		for i = 0, #self._view._setLable - 2 do
			if i == self._index then
				self._view._setLable[i].color = Color.red
			else
				self._view._setLable[i].color = Color.black
			end
		end
	else
		for i = 0, #self._view._setLable - 2 do			
			self._view._setLable[self._index].color = Color.black
		end
	end
		
end

--退出后台
CUISettingLogic.Exit = function(self, _listener, _args, _params)
	gGame:ChangeSceneDirect("coin", "coinScene")
end
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------