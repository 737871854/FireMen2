local Cprotocol = Import("logic/net/protocol").Cprotocol
local Crpc = Import("logic/net/rpc").Crpc

--Import各个模块的协议接口--BEGIN
Import("logic/net/login_protocol")
--Import各个模块的协议接口--END

CProtocolManager = class()

CProtocolManager.Init = function(self)
	self._protocol = Cprotocol:New()
	self._rpc = Crpc:New(self)
end

CProtocolManager.OnSocket = function(self, key, value)
	self._rpc:OnPacket(key, value)
end

CProtocolManager.GetRpc = function(self)
	return self._rpc
end

CProtocolManager.GetProtocol = function(self)
	return self._protocol
end

--上行协议
CProtocolManager.askRpc = function(self, rpcName, rpcArgs, needWait)
	rpcArgs = rpcArgs or {}
	needWait = needWait or false
	if #rpcArgs < 1 then
		self._protocol[rpcName]()
	else
		self._protocol[rpcName](unpack(rpcArgs))
	end
end

--协议下行
CProtocolManager.receiveRpc = function(self, rpcName, rpcArgs)
	rpcArgs = rpcArgs or {}
	if #rpcArgs < 1 then
		self._protocol[rpcName](self._protocol)
	else
		self._protocol[rpcName](self._protocol, unpack(rpcArgs))
	end
end