--======================================================================
--（c）copyright 2015 175game.com All Rights Reserved
--======================================================================
-- filename: game.lua
-- author: lxt  created: 2015/12/28
-- descrip: 游戏的入口，大部分管理类的持有者，除了CControlManager的gControl
--======================================================================
local CLogicEnter = Import("logic/common/logic_enter").CLogicEnter
local CProtocolManager = Import("logic/net/protocol_manager").CProtocolManager
local CDataManager = Import("logic/data/data_manager").CDataManager
local CUIManager = Import("logic/ui/base/ui_manager").CUIManager
local CTimeManager = Import("logic/common/timer_manager").CTimerManager
local EventSource = Import("logic/common/event_source").EventSource
local CSceneManager = Import("logic/modules/scene_manager").CSceneManager

CGame = class(CLogicEnter)

CGame.Init = function(self)
	CLogicEnter.Init(self)
	print(" **** Init Rpc **** ")
	--self._protocolMgr = CProtocolManager:New()
	self._dataMgr = CDataManager:New()
	self._uiMgr = CUIManager:New()
	self._timerManager = CTimeManager:New()
	self.__global_EventSource__ = EventSource:New()
	self._sceneManager = CSceneManager:New()
end

--服务器下发协议
CGame.OnSocket = function(self, key, value)
	self._protocolMgr:OnSocket(key, value)
end

CGame.GetProtocolMgr = function(self)
	return self._protocolMgr
end

CGame.GetDataMgr = function(self)
	return self._dataMgr
end

CGame.GetUIMgr = function(self)
	return self._uiMgr
end

CGame.GetSceneMgr = function (self)
	return self._sceneManager
end

CGame.GetTimeMgr = function (self)
	return self._timerManager
end


CGame.ChangeScene = function(self, name, assetName)
	self._sceneManager:ChangeScene(name, assetName)
end

CGame.ChangeSceneDirect = function(self, name, assetName)
	self._sceneManager:ChangeSceneDirect(name, assetName)
end

CGame.Update = function(self)
	self._sceneManager:Update()
end

CGame.LateUpdate = function(self)
	self._sceneManager:LateUpdate()
end

CGame.FixedUpdate = function(self)
	self._sceneManager:FixedUpdate()
	if self._timerManager then
		self._timerManager:Update()
	end
end

CGame.OnLevelWasLoaded = function(self)
	self._sceneManager:OnLevelWasLoaded()	
end

CGame.OnApplicationQuit = function(self)
	self._sceneManager:OnApplicationQuit()
	self._uiMgr:Clear()
	self._dataMgr:ClearAllData()
end


--下面三个函数用来进行全局事件监听
CGame.FireGlobalEvent = function (self, name, ...)
	-- if not self.__global_EventSource__ then
	-- 	self.__global_EventSource__ = EventSource:New()
	-- end
	self.__global_EventSource__:FireEvent(name, ...)
end

CGame.DelGlobalEvent = function (self, name, handler)
	if not self.__global_EventSource__ then return end
	self.__global_EventSource__:DelListener(name, handler)
end

CGame.HandleGlobalEvent = function (self, name, func)
	-- if not self.__global_EventSource__ then
	-- 	self.__global_EventSource__ = EventSource:New()
	-- end
	local handler = self.__global_EventSource__:AddListener(name, func)
	return handler
end