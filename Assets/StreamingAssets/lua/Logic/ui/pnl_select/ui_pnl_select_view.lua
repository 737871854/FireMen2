local CSpriteHelper = Import("logic/ui/sprite_helper").CSpriteHelper

CUISelectView = class()

--视图的构造函数，定义相关的变量并且初始化
CUISelectView.Init = function(self, prefabPath)
	self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)
	
	local rootTransform = self._root.transform
		
	-- 找到所有Image
	self._images  = CSpriteHelper:FindAllImage(rootTransform)
	-- 加载Atlas
	self._sprites = CSpriteHelper:LoadAllSprites("spritesatlas\\panel_select_atlas")
	
	local select_bar = CSpriteHelper:GetSpriteByName(self._sprites, "ui_select_character_bar")	-- 进度条sprite
		
	self._map_title = rootTransform:Find("ui_map_title")
	self._timeUp = rootTransform:Find("TimeUp"):GetComponent("Text")							-- 倒计时
		
	self._water 		= {}		-- 水标
	self._progress_c 	= {}		-- 角色进度条
	self._progress_m 	= {}		-- 角色进度条
	for i = 0, 2 do
		self._water[i] 		= rootTransform:Find("Panel"..i)
		self._progress_c[i] 	= rootTransform:Find("Character/progress"..i):GetComponent("Image")
		self._progress_c[i].sprite = select_bar
		self._progress_m[i] 	= rootTransform:Find("Map/progress"..i):GetComponent("Image")
		self._progress_m[i].sprite = select_bar
	end	
	
	-- 初始化UI
	for i = 0, self._images.Count - 1 do
		for j = 0, self._sprites.Count - 1 do
			if self._images[i].name == self._sprites[j].name then
				self._images[i].sprite = self._sprites[j]
				break
			end
		end
	end
		
	self._root:SetActive(false)
end

CUISelectView.Destroy = function(self)
	CSpriteHelper:Clear()
	GameObject.Destroy(self._root)   
end