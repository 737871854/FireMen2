local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 

local UIPnlBattleLogic 	= Import("logic/ui/pnl_battle/ui_pnl_battle_logic").UIPnlBattleLogic
local UIPnlSummaryLogic = Import("logic/ui/pnl_summary/ui_pnl_summary_logic").UIPnlSummaryLogic
local UIPnlWaterLogic 	= Import("logic/ui/pnl_water/ui_pnl_water_logic").UIPnlWaterLogic

CBattleScene = class(SceneBase)

CBattleScene.Init = function(self)
	SceneBase.Init(self)
	
	self._battle = nil
end

CBattleScene.OnLevelWasLoaded = function(self)
	self._camera = Camera.main
	self._hasEnterGame = false
	print("ChangeScene: Enter Battle Scene, ready to Battle")
end

CBattleScene.OnUpdate = function(self)
	if self._hasEnterGame then return end
	self._hasEnterGame = true
	self._battle = gGame:GetUIMgr():CreateWindowCustom("battle", UIPnlBattleLogic)	
	
	self:PlayMusic()
		
	--注册逻辑消息	
	ioo.RegesterListener("Event_To_Summary", LuaHelper.EventHandle(function(data) self:OpenSummaryPanel(data) end), "battle_scene")
	ioo.RegesterListener("Event_Game_Over", LuaHelper.EventHandle(function() self:GameOver() end), "battle_scene")
	ioo.RegesterListener("Event_Level_Pass", LuaHelper.EventHandle(function(data) self:LevelPass(data) end), "battle_scene")
	ioo.RegesterListener("Event_Fill_Water", LuaHelper.EventHandle(function(data) self:FillWater(data) end), "battle_scene")
	ioo.RegesterListener("Event_Hold_Player", LuaHelper.EventHandle(function(data) self:RespondHold(data) end), "battle_scene")
	ioo.RegesterListener("Event_Boss_Born", LuaHelper.EventHandle(function() self:BossBorn() end), "battle_scene")
	ioo.RegesterListener("Event_Boss_Dead", LuaHelper.EventHandle(function() self:BossDead() end), "battle_scene")
	ioo.RegesterListener("Event_Screen_Crack", LuaHelper.EventHandle(function(data) self:AddScreenCrack(data) end), "battle_scene")
end

CBattleScene.OnLateUpdate = function(self)
end

CBattleScene.OnFixedUpdate = function(self)
	if not self._battle then
		return
	end

	-- 更新所有玩家水标和分数值
	for i = 0, ioo.playerCount - 1 do
		if ioo.IsPlaying(i) then
			self._battle:FixedUpdatePos(i, ioo.WaterPosition(i))
			self._battle:RefreshHeadInfo(i, ioo.HPProgress(i), ioo.Score(i))		
		end	
		
		if ioo.IsContinue(i) then
			self._battle:OnPlayerContinue(i, ioo.ContinueTime(i))		
		end
		
		if ioo.IsDead(i) then
			self._battle:OnPlayerDead(i)
		end
	end	
	
	-- 更新劫持玩家进度
	if self._playerIsBeenHold == true then
		self._battle:UpdateHoldUI(ioo.gameMode.HoldValue)
	end
	
	-- 更新Boss血条
	if self._bossIsLive == true then
		self._battle:UpdateBossHealth(ioo.gameMode.BossProgress)
	end
	
	-- 屏幕破碎
	self._battle:UpdateScreenCrack()
	
	-- 射击点
	if ioo.gameMode.usingHittingParList ~= nil then
		if ioo.gameMode.usingHittingParList.Count == 0 then
			self._battle:ClearHittingPart()
		else		
			for i = 0, ioo.gameMode.usingHittingParList.Count - 1 do	
				self._battle:UpdateHittingPart(i, ioo.gameMode.usingHittingParList[i])
			end		
		end	
	end
	
	-- 更新加水界面
	if not self._water then
		return
	end
	
	local timer = ioo.gameMode.WaterTime
	local water = ioo.gameMode.WaterValue
	self._water:UpdateWaterInfo(timer, water)
end

CBattleScene.OnApplicationQuit = function(self)
end

CBattleScene.OnDestroy = function(self)
	self:StopMusic()
	
	gGame:GetUIMgr():DestroyWindow("battle")
	
	-- 移除逻辑消息	
	ioo.RemoveListener("Event_To_Summary", "battle_scene")
	ioo.RemoveListener("Event_Game_Over", "battle_scene")
	ioo.RemoveListener("Event_Level_Pass", "battle_scene")
	ioo.RemoveListener("Event_Fill_Water", "battle_scene")
	ioo.RemoveListener("Event_Hold_Player", "battle_scene")
	ioo.RemoveListener("Event_Boss_Born", "battle_scene")
	ioo.RemoveListener("Event_Boss_Dead", "battle_scene")
	ioo.RemoveListener("Event_Screen_Crack","battle_scene")
	
end

-- 预加载
CBattleScene.SetPreLoad = function(self, level)
	self._level = level
	
	if level == "battle0_0" then
		-- 预加载UI面板
		ioo.AddPreLoadPanel("prefabs\\ui\\panel_water")
		-- Agent
		ioo.AddPreLoadPrefab("huge_fire_monster", 4)
		ioo.AddPreLoadPrefab("mini_fire_monster", 4)
		ioo.AddPreLoadPrefab("smoke_fire_monster", 4)
		ioo.AddPreLoadPrefab("citizen0", 1)
		ioo.AddPreLoadPrefab("citizen1", 1)
		ioo.AddPreLoadPrefab("citizen2", 1)
		ioo.AddPreLoadPrefab("helicopter", 1)
		ioo.AddPreLoadPrefab("coin", 30)
		ioo.AddPreLoadPrefab("freeze", 1)
		ioo.AddPreLoadPrefab("super_water", 2)
		ioo.AddPreLoadPrefab("support", 1)
		ioo.AddPreLoadPrefab("sand_box", 2)
		ioo.AddPreLoadPrefab("elite_monster", 3)
		-- 特效
		ioo.AddPreLoadPrefab("effect_mini_fire_monster_bron", 4)
		ioo.AddPreLoadPrefab("effect_smoke_monster_born", 4)
	end
	
	if level == "battle0_1" then
		
	end
	
	if level == "battle0_2" then
		-- Agent				
		ioo.AddPreLoadPrefab("bull_demon_king",1)
	end
		
	if level == "battle1" then
		-- 预加载UI面板
		ioo.AddPreLoadPanel("prefabs\\ui\\panel_water")
		-- Agent
		ioo.AddPreLoadPrefab("huge_fire_monster", 4)
		ioo.AddPreLoadPrefab("mini_fire_monster", 4)
		ioo.AddPreLoadPrefab("smoke_fire_monster", 4)
		ioo.AddPreLoadPrefab("citizen0", 2)
		ioo.AddPreLoadPrefab("citizen1", 2)
		ioo.AddPreLoadPrefab("citizen2", 2)
		ioo.AddPreLoadPrefab("helicopter", 1)
		ioo.AddPreLoadPrefab("coin", 20)
		ioo.AddPreLoadPrefab("bucket", 1)
		ioo.AddPreLoadPrefab("freeze", 1)
		ioo.AddPreLoadPrefab("super_water", 2)
		ioo.AddPreLoadPrefab("support", 1)
		ioo.AddPreLoadPrefab("npc0", 1)
		ioo.AddPreLoadPrefab("npc1", 1)
		ioo.AddPreLoadPrefab("npc2", 1)
		ioo.AddPreLoadPrefab("npc3", 1)
		ioo.AddPreLoadPrefab("npc4", 1)
		ioo.AddPreLoadPrefab("wolf", 2)
		ioo.AddPreLoadPrefab("elite_monster", 6)
		ioo.AddPreLoadPrefab("bear", 1)
		-- 特效
		ioo.AddPreLoadPrefab("effect_mini_fire_monster_bron", 4)
		ioo.AddPreLoadPrefab("effect_smoke_monster_born", 4)		
	end
	
	if level == "battle2" then
		-- 预加载UI面板
		ioo.AddPreLoadPanel("prefabs\\ui\\panel_water")
		-- Agent
		ioo.AddPreLoadPrefab("huge_fire_monster", 4)
		ioo.AddPreLoadPrefab("mini_fire_monster", 4)
		ioo.AddPreLoadPrefab("smoke_fire_monster", 4)
		ioo.AddPreLoadPrefab("citizen0", 2)
		ioo.AddPreLoadPrefab("citizen1", 2)
		ioo.AddPreLoadPrefab("citizen2", 2)
		ioo.AddPreLoadPrefab("helicopter", 1)
		ioo.AddPreLoadPrefab("coin", 20)
		ioo.AddPreLoadPrefab("freeze", 1)
		ioo.AddPreLoadPrefab("super_water", 2)
		ioo.AddPreLoadPrefab("support", 1)
		ioo.AddPreLoadPrefab("life_boat", 2)
		ioo.AddPreLoadPrefab("dragon", 4)
		ioo.AddPreLoadPrefab("pterosaur", 1)
		-- 特效
		ioo.AddPreLoadPrefab("effect_mini_fire_monster_bron", 4)
		ioo.AddPreLoadPrefab("effect_smoke_monster_born", 4)		
	end
	--[[
	ioo.AddPreLoadPrefab("hydrant", 1)
	ioo.AddPreLoadPrefab("extinguisher", 1)
	ioo.AddPreLoadPrefab("box", 1)
	ioo.AddPreLoadPrefab("move_extinguisher", 1)
	ioo.AddPreLoadPrefab("super_water", 1)
	ioo.AddPreLoadPrefab("telephone", 1)
	ioo.AddPreLoadPrefab("water", 1)
	ioo.AddPreLoadPrefab("water_ bottle", 1)
	ioo.AddPreLoadPrefab("water_wheel", 1)
	ioo.AddPreLoadPrefab("well", 1)
	]]
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 内部使用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 打开结算面板
CBattleScene.OpenSummaryPanel = function(self, data)
	if data == true then
		self._summary = gGame:GetUIMgr():CreateWindowCustom("summary", UIPnlSummaryLogic)
	else
		self._summary = gGame:GetUIMgr():CreateWindowCustom("summary", UIPnlSummaryLogic)
	end	
	self._summary:SetLevelIsPass(data)
end

-- 打开或关闭加水UI界面
CBattleScene.FillWater = function(self, data)
	if data == true then		
		self._water = gGame:GetUIMgr():CreateWindowCustom("water", UIPnlWaterLogic)
	else
		if self._water ~= nil then
			gGame:GetUIMgr():DestroyWindow("water")
			self._water = nil
		end		
	end	
end

-- 打开或关闭解救玩家被劫持状态UI
CBattleScene.RespondHold = function(self, data)
	if data == true then
		self._playerIsBeenHold = true
		self._battle:ActiveHoldUI()
	else 
		self._playerIsBeenHold = false
		self._battle:DisActiveHoldUI()
	end
end

-- 显示屏幕破碎特效
CBattleScene.AddScreenCrack = function(self, data)
	self._battle:AddScreenCrack(data)
end

-- Boss出生
CBattleScene.BossBorn = function(self)
	self._bossIsLive = true
	local id = nil
	if self._level == "battle0_2" then
		id = 0
		else if self._level == "battle1" then
			id = 1
			else if self._level == "battle2" then
				id = 2
			end
		end
	end
	self._battle:BossBorn(id)
end

-- Boss死亡
CBattleScene.BossDead = function(self)
	self._bossIsLive = false
	self._battle:BossDead()
end

-- 游戏结束
CBattleScene.GameOver = function(self)
	self._summary = nil
	gGame:ChangeSceneDirect("coin","coinScene")
end

-- 关卡通过
CBattleScene.LevelPass = function(self, data)
	local direct = false
	local name = nil
	local assetName = nil
	if data == 2 then
		name = "battle0_1"
		assetName = "battleScene0_1"
		else if data == 3 then
			name = "battle0_2"
			assetName = "battleScene0_2"
			else if data == 4 or data == 5 or data == 6 then
				direct = true
				name = "coin"
				assetName = "coinScene"
			end	
		end	
	end
	
	if direct then
		gGame:ChangeSceneDirect(name, assetName)		
	else
		gGame:ChangeScene(name, assetName)
	end
end

-- 播放音效
CBattleScene.PlayMusic = function(self)
	print(self._level)
	if self._level == "battle0_0" then
		ioo.PlayBackMusic("music_scene0_0", true)			
	end
	
	--[[
	 if self._level == "battle0_1" then
		ioo.PlayBackMusic("music_scene0_1", true)
	end	
	]]
   	
	if self._level == "battle0_2" then
		ioo.PlayBackMusic("music_scene0_2", true)
	end
	
	if self._level == "battle1" then
		ioo.PlayBackMusic("music_scene1", true)
	end
	
	if self._level == "battle2" then
		ioo.PlayBackMusic("music_scene2", true)
	end
	
end

-- 结束音效
CBattleScene.StopMusic = function(self)
	if self._level == "battle0_0" then
		ioo.StopBackMusic("music_scene0_0")			
	end
	
	--[[
	if self._level == "battle0_1" then
		ioo.StopBackMusic("music_scene0_1")
	end	
	]]    
	
	if self._level == "battle0_2" then
		ioo.StopBackMusic("music_scene0_2")
	end
	
	if self._level == "battle1" then
		ioo.StopBackMusic("music_scene1")
	end
	
	if self._level == "battle2" then
		ioo.StopBackMusic("music_scene2")
	end
	
end
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------