local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 

local UIPnlSelectLogic = Import("logic/ui/pnl_select/ui_pnl_select_logic").CUISelectLogic

-- 投币场景
CSelectScene = class(SceneBase)

CSelectScene.Init = function(self)
	SceneBase.Init(self)
	
	self._select = nil	
	
end

CSelectScene.OnLevelWasLoaded = function(self)
	self._camera = Camera.main
	self._hasEnterGame = false
	
	print("ChangeScene: Enter select Scene, ready to Select!!!")
end

CSelectScene.OnUpdate = function(self)
	if self._hasEnterGame then return end
	ioo.StopAll()
	self._hasEnterGame = true
	self._select = gGame:GetUIMgr():CreateWindowCustom("select", UIPnlSelectLogic)
	ioo.PlayBackMusic("music_panel_select", true)
	
	--注册逻辑消息	
	ioo.RegesterListener("Map_Select_End", LuaHelper.EventHandle(function(data) self:MapSelectEnd(data) end), "scene_select_map_end")
end

CSelectScene.OnLateUpdate = function(self)
end

CSelectScene.OnFixedUpdate = function(self)	
	if not self._select then
		return
	end
	
	-- 更新所有玩家水标
	for i = 0, ioo.playerCount - 1 do
		if ioo.IsPlaying(i) then
			self._select:FixedUpdatePos(i, ioo.WaterPosition(i))
		end	
	end	
end


CSelectScene.OnApplicationQuit = function(self)
end

CSelectScene.OnDestroy = function(self)
	self._camera = nil
	gGame:GetUIMgr():DestroyWindow("select")
	
	-- 移除逻辑消息	
	ioo.RemoveListener("Map_Select_End", "scene_select_map_end")
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 内部使用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 结束地图选择
CSelectScene.MapSelectEnd = function(self, data)
	local mapID = data
	self._select:EndTimeUp()	
	local name = nil
	local assetName = nil
	
	if mapID == 0 then
		name = "battle0_1"
		assetName = "battleScene0_1"
		else if mapID == 1 then
			name = "battle1"
			assetName = "battleScene1"
			else if mapID == 2 then
				name = "battle2"
				assetName = "battleScene2"
			end		
		end	
	end	
	
	gGame:ChangeScene(name,assetName)
end
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
