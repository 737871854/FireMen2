local CSpriteHelper = Import("logic/ui/sprite_helper").CSpriteHelper

CUIWaterView = class()

--视图的构造函数，定义相关的变量并且初始化
CUIWaterView.Init = function(self, prefabPath)

	self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)
	
    local rootTransform = self._root.transform
	
	self._fill_water = {}		-- 加水
	self._fill_water.red		= 	rootTransform:Find("Frame_WaterRed"):GetComponent("Image")
	self._fill_water.ground 	= 	rootTransform:Find("Frame_WaterGround"):GetComponent("Image")
	self._fill_water.text		= 	rootTransform:Find("Frame_PullText"):GetComponent("Image")
	self._fill_water.timeUp 	= 	rootTransform:Find("TimeUp"):GetComponent("Text")
	self._fill_water.progress 	= 	rootTransform:Find("Image_Fill"):GetComponent("Image")
	self._fill_water.splash		= 	rootTransform:Find("Image_Splash"):GetComponent("Image")
	
	self._animator_man		= rootTransform:Find("Image_Man"):GetComponent("Animator")
	self._animator_column 	= rootTransform:Find("Image_Column"):GetComponent("Animator")
	
	-- 找到所有Image
	self._images  = CSpriteHelper:FindAllImage(rootTransform)
	-- 加载Atlas
	self._sprites = CSpriteHelper:LoadAllSprites("spritesatlas\\panel_water_atlas")
	
	self._fill_water.red.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "Frame_WaterRed")
	self._fill_water.ground.sprite	= CSpriteHelper:GetSpriteByName(self._sprites, "Frame_WaterGround")
	self._fill_water.text.sprite	= CSpriteHelper:GetSpriteByName(self._sprites, "Frame_PullText")
	
	
	--[[
	self._coinNumbers 	= {}
	for i = 0, 2 do
		self._coinNumbers[i] 	= rootTransform:Find("Coin"..i.."/CoinNumber"):GetComponent("Text")
	end
	
	-- 找到所有Image
	self._images  = CSpriteHelper:FindAllImage(rootTransform)
	-- 加载Atlas
	self._sprites = CSpriteHelper:LoadAllSprites("spritesatlas\\panel_start_atlas")
	
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

CUIWaterView.Destroy = function(self)
	CSpriteHelper:Clear()
	GameObject.Destroy(self._root)   
end