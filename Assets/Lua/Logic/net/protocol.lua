Cprotocol = class()

Cprotocol.Init = function(self)
	
end

-----------------------------------------------------------
--网络内置事件,ID号为101~103

--连上服务器时回调
Cprotocol.OnConnect = function(self)
	print("OnConnect")
--	self._updatingPto = "";
end

--断开服务器时回调
Cprotocol.OnDisconnect = function(self)
	print("OnDisconnect")
end

--产生异常时回调
Cprotocol.OnException = function(self)
	print("OnException")
end

--协议下发，当前后端协议配置的MD5不一致时下发
Cprotocol.rpc_client_update_pto = function(self, protocolConfig)
	if Application.platform == RuntimePlatform.WindowsEditor then
		local f = assert(io.open("rpc_lua_table.lua", 'w'))
	    f:write(protocolConfig)
	    f:close()
	end
end