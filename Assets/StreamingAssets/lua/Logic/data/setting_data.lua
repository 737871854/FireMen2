local EventDefine = Import("logic/base/event_define").Define
local EventSource = Import("logic/common/event_source").EventSource

CSettingData = class()

CSettingData.Init = function(self)	
	self._maxRate 		= 6
	self._maxVolume 	= 10
	self._maxLevel 		= 3
	self._maxLanguage 	= 2

	self._hasCoin 	= {}
	self._rate 		= ioo.gameRate
	self._volume	= ioo.gameVolume
	self._level		= ioo.gameLevel
	self._language  = ioo.gameLanguage
	
	for i = 0, 2 do
		self._hasCoin[i] = ioo.HasCoin(i)
		if self._hasCoin[i] > 99 then
			self._hasCoin[i] = 99
		end
	end
	
	--注册 Keyboard消息(0:投币)
	IOLuaHelper.Instance:RegesterListener(0, LuaHelper.OnIOEventHandle(function(id) self:AddCoin(id) end), "setting_data_coin")
	IOLuaHelper.Instance:RegesterListener(1, LuaHelper.OnIOEventHandle(function(id) self:PlayerSure(id) end), "setting_data_sure")
end

--修改币率
CSettingData.SetGameRate = function(self, _value)
	self._rate = _value
	ioo.gameRate = self._rate
	self:Save()	
end

--修改音量
CSettingData.SetGameVolume = function(self, _volume)
	 self._volume = _volume
	 ioo.gameVolume = self._volume
	 self:Save()
end

--修改难度
CSettingData.SettGameLevel = function(self, _level)
	 self._level = _level
	 ioo.gameVolume = self._level
	 self:Save()
end

--修改游戏语言
CSettingData.SetGameLanguage = function(self, _language)
	 self._language = _language
	 ioo.gameLanguage = self._language
	 self:Save()
	 EventTrigger(EventDefine.Setting_Data_Language)
end

--修改出票模式
CSettingData.SetGameTicket = function(self, _ticket)

end

--清除投币
CSettingData.ClearCoin = function(self)
	for i = 0, #self._hasCoin - 1 do
		self._hasCoin[i] = 0
	end
	ioo.ClearCoin()
	self:Save()
end

--清除查账
CSettingData.ClearAccount = function(self)
	ioo.ClearMonthInfo()
	ioo.ClearTotalRecord()
	self:Save()
	EventTrigger(EventDefine.Setting_Data_Language)
end

-- 币数变化
CSettingData.AddCoin = function(self, id) 	
	ioo.PlaySound2D("sfx_sound_change")
	self._hasCoin[id] = self._hasCoin[id] + 1
	if self._hasCoin[id] >= 99 then
		self._hasCoin[id] = 99
	end
	ioo.AddCoin(id)
	self:Save() 
	EventTrigger(EventDefine.COIN_DATA_COIN_CHANGE, id)
end

-- 玩家按确认键
CSettingData.PlayerSure = function(self, id)
	if self._hasCoin[id] >= self._rate and not ioo.playerManager:GetPlayer(id):IsPlaying() then
		ioo.playerManager:GetPlayer(id):UseCoin()
		self:UseCoin(id)
		EventTrigger(EventDefine.COIN_DATA_PLAYER_SURE, id)
	end
end

-- 使用游戏币
CSettingData.UseCoin = function(self, id)
	ioo.PlaySound2D("sfx_sound_sure")
	self._hasCoin[id] = self._hasCoin[id] - self._rate
	ioo.UseCoin(id)
	ioo.LogNumberOfGame(id)
	self:Save()
	EventTrigger(EventDefine.COIN_DATA_COIN_CHANGE, id)
end

--保存到本地
CSettingData.Save = function(self)
	ioo.Save()
end

CSettingData.ClearALL = function(self)
 --移除 Keyboard消息
  IOLuaHelper.Instance:RemoveListener(0, "setting_data_coin")
  IOLuaHelper.Instance:RemoveListener(1, "setting_data_sure")
end
