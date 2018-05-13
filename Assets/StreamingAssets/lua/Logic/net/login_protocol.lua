local Cprotocol = Import("logic/net/protocol").Cprotocol
local LoginDefine = Import("logic/base/login_define").Define

--encodType：编码方式，默认UTF-8
Cprotocol.rpc_client_version_return = function(self, result, encodType)
	local loginData = gGame:GetDataMgr():GetLoginData()
	loginData:SetEncodType(encodType)
end

--charset：字符集，默认cn
Cprotocol.rpc_client_charset_filter = function(self, charset)
	local loginData = gGame:GetDataMgr():GetLoginData()
	loginData:SetCharset(charset)
	gGame:GetProtocolMgr():askRpc("rpc_server_use_login_key", {LoginDefine.TEST_LOGIN_KEY})
end

-- class result {
--     int result;
--     string msg;
-- }
Cprotocol.rpc_client_use_login_key_return = function(self, result)
	print("==============登陆结果00："..(result.msg))
end

Cprotocol.rpc_client_uid_list_equip = function(self, list, result)
	if #list < 1 then
		--没有角色，则新建角色
		gGame:GetProtocolMgr():askRpc("rpc_server_new_uid", {LoginDefine.TEST_LOGIN_ICON, LoginDefine.TEST_LOGIN_SCHOOL, LoginDefine.TEST_LOGIN_USERNAME})
	end
end

-- class result {
--     int result;
--     string msg;
-- }
Cprotocol.rpc_client_new_uid_return = function(self, result)
	print("==============新建角色异常："..(result.msg))
end

Cprotocol.rpc_client_login_return = function(self, result)
	print("==============登陆结果11："..(result.msg))
end

Cprotocol.rpc_client_close_reason = function(self, result, msg)
	print("==============断开连接："..(msg))
end