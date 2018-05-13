local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 

local UIPnlCoinLogic = Import("logic/ui/pnl_coin/ui_pnl_coin_logic").CUICoinLogic

-- Í¶±Ò³¡¾°
CCoinScene = class(SceneBase)

CCoinScene.Init = function(self)
	SceneBase.Init(self)
	
	self._coin = nil	
	
	math.randomseed(os.time())
	self._index = math.random(0,3)
end


CCoinScene.OnLevelWasLoaded = function(self)
	self._camera = Camera.main
	self._hasEnterGame = false
	
	print("ChangeScene: Enter Coin Scene, ready to Coin!!!")
end

CCoinScene.OnUpdate = function(self)
	if self._hasEnterGame then return end
	self._hasEnterGame = true
	self._coin = gGame:GetUIMgr():CreateWindowCustom("coin", UIPnlCoinLogic)
	ioo.PlayBackMusic("music_standby_0"..self._index, true)
end

CCoinScene.OnLateUpdate = function(self)
end

CCoinScene.OnFixedUpdate = function(self)
end


CCoinScene.OnApplicationQuit = function(self)
end

CCoinScene.OnDestroy = function(self)
	self._camera = nil
	ioo.StopBackMusic("music_standby_0"..self._index)
	gGame:GetUIMgr():DestroyWindow("coin")
end

--[[
	CCoinScene.ToSelect = function(self)
	ioo.StopAll()
	gGame:GetUIMgr():CreateWindowCustom("select", UIPnlSelectLogic)
	gGame:GetUIMgr():DestroyWindow("coin")
end
]]
