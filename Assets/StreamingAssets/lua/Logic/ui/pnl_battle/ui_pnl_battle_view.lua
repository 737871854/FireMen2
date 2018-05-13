local CSpriteHelper = Import("logic/ui/sprite_helper").CSpriteHelper

CUIBattleView = class()

--视图的构造函数，定义相关的变量并且初始化
CUIBattleView.Init = function(self, prefabPath)
	self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)
	
	local rootTransform = self._root.transform
		
	self._water 		= {}		-- 水标	
	self._head			= {}		-- 头像	
	for i = 0, 2 do
		self._water[i] 		= rootTransform:Find("Panel"..i.."/Water")	
		self._head[i] 		= {}
		self._head[i].play 	= rootTransform:Find("Panel"..i.."/Head/Head_Play"):GetComponent("Image")
		self._head[i].dead 	= rootTransform:Find("Panel"..i.."/Head/Head_Dead"):GetComponent("Image")
		self._head[i].progress_backward = rootTransform:Find("Panel"..i.."/Head/Head_Progress_Backward"):GetComponent("Image")
		self._head[i].progress_forward 	= rootTransform:Find("Panel"..i.."/Head/Head_Progress_Forward"):GetComponent("Image")
		self._head[i].coin 	= rootTransform:Find("Panel"..i.."/Head/Coin"):GetComponent("Text") 
		self._head[i].score = rootTransform:Find("Panel"..i.."/Head/Score"):GetComponent("Text")
		self._head[i].continue_time 	= rootTransform:Find("Panel"..i.."/Head/Continue_Time"):GetComponent("Text")
		self._head[i].continue_image 	= rootTransform:Find("Panel"..i.."/Head/Head_Continue"):GetComponent("Image")
		self._head[i].xp 				= rootTransform:Find("Panel"..i.."/Head/XP"):GetComponent("Image")
		self._head[i].coin_title 		= rootTransform:Find("Panel"..i.."/Head/Coin_Title"):GetComponent("Image")
	end	
	
	-- Boss图像
	self._boss_head = {}
	self._boss_head.obj 		= rootTransform:Find("Boss").gameObject
	self._boss_head.background 	= rootTransform:Find("Boss/ui_battle_boss_progress_backward"):GetComponent("Image")
	self._boss_head.progress0 	= rootTransform:Find("Boss/ui_battle_boss_progress_forward0"):GetComponent("Image")
	self._boss_head.progress1 	= rootTransform:Find("Boss/ui_battle_boss_progress_forward1"):GetComponent("Image")
	self._boss_head.progress2 	= rootTransform:Find("Boss/ui_battle_boss_progress_forward2"):GetComponent("Image")
	self._boss_head.head 		= rootTransform:Find("Boss/Head"):GetComponent("Image")
	self._boss_head.num 		= rootTransform:Find("Boss/Num"):GetComponent("Text")
	
	-- 劫持玩家进度
	self._holdProgress = rootTransform:Find("Hold_Progress"):GetComponent("Image")
	
	-- 射击点
	self._hit_target = {}
	self._hit_target.obj = rootTransform:Find("HitPoint").gameObject
	self._hit_target[1] = rootTransform:Find("HitPoint/Frame_Target0"):GetComponent("Image")
	self._hit_target[2] = rootTransform:Find("HitPoint/Frame_Target1"):GetComponent("Image")
	self._hit_target[3] = rootTransform:Find("HitPoint/Frame_Target2"):GetComponent("Image")
	--self._hit_target.target0_ret = rootTransform:Find("HitPoint/Frame_Target0"):GetComponent("RectTransform")
	--self._hit_target.target1_ret = rootTransform:Find("HitPoint/Frame_Target1"):GetComponent("RectTransform")
	--self._hit_target.target2_ret = rootTransform:Find("HitPoint/Frame_Target2"):GetComponent("RectTransform")
	
	-- 屏幕破碎
	self._screen_crack = {}
	self._screen_crack.obj = rootTransform:Find("CrackScreen").gameObject
	self._screen_crack[1] = rootTransform:Find("CrackScreen/Frame_CrackScreen1"):GetComponent("Image")
	self._screen_crack[2] = rootTransform:Find("CrackScreen/Frame_CrackScreen2"):GetComponent("Image")
	self._screen_crack[3] = rootTransform:Find("CrackScreen/Frame_CrackScreen3"):GetComponent("Image")

	
	-- 找到所有Image
	self._images  = CSpriteHelper:FindAllImage(rootTransform)
	-- 加载Atlas
	self._sprites = CSpriteHelper:LoadAllSprites("spritesatlas\\panel_battle_atlas")
	
	for i = 0, 2 do
		self._head[i].play.sprite =	CSpriteHelper:GetSpriteByName(self._sprites, "ui_head_player"..i)
		self._head[i].dead.sprite =	CSpriteHelper:GetSpriteByName(self._sprites, "ui_head_player"..i.."_grey")
		self._head[i].progress_backward.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "ui_head_progress_backward")
		self._head[i].progress_forward.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "ui_head_progress_forward"..i)
		self._head[i].continue_image.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "ui_head_small_continue")	
		self._head[i].xp.sprite 		= CSpriteHelper:GetSpriteByName(self._sprites, "xp"..i)
		self._head[i].coin_title.sprite = CSpriteHelper:GetSpriteByName(self._sprites, "ui_head_coin_title")
	end	
	
	self._boss_head.background.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "ui_battle_boss_progress_backward")
	self._boss_head.progress0.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "ui_battle_boss_progress_forward0")
	self._boss_head.progress1.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "ui_battle_boss_progress_forward1")
	self._boss_head.progress2.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "ui_battle_boss_progress_forward2")
	
	self._holdProgress.sprite 			= CSpriteHelper:GetSpriteByName(self._sprites, "ui_battle_boss_progress_forward0")
	
	self._hit_target[1].sprite	= CSpriteHelper:GetSpriteByName(self._sprites, "Frame_Target0")
	self._hit_target[2].sprite	= CSpriteHelper:GetSpriteByName(self._sprites, "Frame_Target1")
	self._hit_target[3].sprite	= CSpriteHelper:GetSpriteByName(self._sprites, "Frame_Target2")
	
	self._screen_crack[1].sprite = CSpriteHelper:GetSpriteByName(self._sprites, "Frame_CrackScreen1")
	self._screen_crack[2].sprite = CSpriteHelper:GetSpriteByName(self._sprites, "Frame_CrackScreen2")
	self._screen_crack[3].sprite = CSpriteHelper:GetSpriteByName(self._sprites, "Frame_CrackScreen3")
	
	--self._boss_head.head.sprite = CSpriteHelper:GetSpriteByName(self._sprites, "")
	
	--[[
	-- 初始化UI
	for i = 0, self._images.Count - 1 do
		for j = 0, self._sprites.Count - 1 do
			if self._images[i].name == self._sprites[j].name then
				self._images[i].sprite = self._sprites[j]
				break
			end
		end
	end
	]]
			
	self._root:SetActive(false)
end

CUIBattleView.Destroy = function(self)
	CSpriteHelper:Clear()
	GameObject.Destroy(self._root)   
end