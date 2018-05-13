local CSpriteHelper = Import("logic/ui/sprite_helper").CSpriteHelper

CUICoinView = class()

--视图的构造函数，定义相关的变量并且初始化
CUICoinView.Init = function(self, prefabPath)

	self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)
	
    local rootTransform = self._root.transform
	
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
		
     self._root:SetActive(false)

end

CUICoinView.Destroy = function(self)
	CSpriteHelper:Clear()
	GameObject.Destroy(self._root)   
end