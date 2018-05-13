local EventDefine = Import("logic/base/event_define").Define

CLoginData = class()

CLoginData.Init = function(self)
	self._encodType = nil
	self._charset = nil
end

CLoginData.SetEncodType = function(self, encodType)
	self._encodType = encodType
	--EventTrigger(EventDefine.LOGIN_DATA_UPDATE)
end

CLoginData.GetEncodType = function(self)
	return self._encodType
end

CLoginData.SetCharset = function(self, charset)
	self._charset = charset
	--EventTrigger(EventDefine.LOGIN_DATA_UPDATE)
end

CLoginData.GetCharset = function(self)
	return self._charset
end

CLoginData.ClearALL = function(self)

end