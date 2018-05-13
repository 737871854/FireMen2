local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 

local UIPnlSettingLogic = Import("logic/ui/pnl_setting/ui_pnl_setting_logic").CUISettingLogic

-- Í¶±Ò³¡¾°
CSettingScene = class(SceneBase)

CSettingScene.Init = function(self)
	SceneBase.Init(self)
	
	self._coin = nil
end


CSettingScene.OnLevelWasLoaded = function(self)
	self._camera = Camera.main
	print("ChangeScene: Enter Setting Scene, ready to Setting!!!")
end

CSettingScene.OnUpdate = function(self)
	if self._hasEnterGame then return end
	self._hasEnterGame = true
	
	self._coin = gGame:GetUIMgr():CreateWindowCustom("setting", UIPnlSettingLogic)
end

CSettingScene.OnLateUpdate = function(self)
end

CSettingScene.OnFixedUpdate = function(self)
end


CSettingScene.OnApplicationQuit = function(self)
end

CSettingScene.OnDestroy = function(self)
	self._camera = nil
	gGame:GetUIMgr():DestroyWindow("setting")
end