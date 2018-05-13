local LoginScene = Import("logic/modules/login_scene").CLoginScene
local CoinScene = Import("logic/modules/coin_scene").CCoinScene
local SelectScene = Import("logic/modules/select_scene").CSelectScene
local BattleScene = Import("logic/modules/battle_scene").CBattleScene
local MovieScene = Import("logic/modules/movie_scene").CMovieScene
local SettingScene = Import("logic/modules/setting_scene").CSettingScene
local scene_load_info = Import("logic/config/info_scene_load").Data

local SceneConfig = {
	["login"] = {
		["class"] = LoginScene,
	},
	["coin"] = {
		["class"] = CoinScene,
	},
	["select"] = {
		["class"] = SelectScene,
	},
	["movie"] = {
		["class"] = MovieScene,
	},
	["battle0_0"] = {
		["class"] = BattleScene,
	},
	["battle0_1"] = {
		["class"] = BattleScene,
	},
	["battle0_2"] = {
		["class"] = BattleScene,
	},
	["battle1"] = {
		["class"] = BattleScene,
	},
	["battle2"] = {
		["class"] = BattleScene,
	},
	["setting"] = {
		["class"] = SettingScene,
	}
	
}

CSceneManager = class()

CSceneManager.Init = function(self)
	self._isLoadingScene = false
end

CSceneManager.IsLoadingScene = function(self)
	return self._isLoadingScene
end

CSceneManager.Update = function(self)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnUpdate()
	end
end

CSceneManager.FixedUpdate = function(self)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnFixedUpdate()
	end
end

CSceneManager.LateUpdate = function(self)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnLateUpdate()
	end
end

CSceneManager.OnLevelWasLoaded = function(self)
	self._isLoadingScene = false
	if self._current_scene then
		self._current_scene:OnLevelWasLoaded()
	end
end

CSceneManager.OnApplicationQuit = function(self)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnApplicationQuit()
	end
end
--调用了changeScene函数后就等着调用OnLevelWasLoaded
CSceneManager.ChangeScene = function(self, name, assetName)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnDestroy()
	end
    gGame:GetUIMgr():Clear()
    --gGame:GetObjMgr():Clear()
	gControl:ClearAll()
	self._isLoadingScene = true

    local classConfig = SceneConfig[name]
	local scene_cfg = scene_load_info[assetName]
	if classConfig == nil or not scene_cfg then
		warn("sceneType:"..name.." or asset:"..assetName.." not exist!")
	end
 
   	self._current_scene = classConfig["class"]:New()
    ioo.SetLoadScene(scene_cfg["path"],scene_cfg["level"])
    self._current_scene:SetPreLoad(name)
    ioo.ChangeScene()
	collectgarbage("collect")
end

CSceneManager.ChangeSceneDirect = function(self, name, assetName)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnDestroy()
	end
    gGame:GetUIMgr():Clear()
    --gGame:GetObjMgr():Clear()
	gControl:ClearAll()
	self._isLoadingScene = true

    local classConfig = SceneConfig[name]
	local scene_cfg = scene_load_info[assetName]
	if classConfig == nil or not scene_cfg then
		warn("sceneType:"..name.." or asset:"..assetName.." not exist!")
	end
 
   	self._current_scene = classConfig["class"]:New()
    ioo.SetLoadScene(scene_cfg["path"],scene_cfg["level"])
    --self._current_scene:SetPreLoad()
    ioo.ChangeSceneDirect()
	collectgarbage("collect")
end

CSceneManager.GetCurrentScene = function(self)
	return self._current_scene
end
