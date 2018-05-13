local CSpriteHelper = Import("logic/ui/sprite_helper").CSpriteHelper

CUISummaryView = class()

--视图的构造函数，定义相关的变量并且初始化
CUISummaryView.Init = function(self, prefabPath)
	self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)
	
	local rootTransform = self._root.transform

	-- 找到所有Image
	self._images  = CSpriteHelper:FindAllImage(rootTransform)
	-- 加载Atlas
	self._sprites = CSpriteHelper:LoadAllSprites("spritesatlas\\panel_summary_atlas")
	
	self._title_defeat 	= rootTransform:Find("ui_summary_defeat"):GetComponent("Image")
	self._title_rank 	= rootTransform:Find("ui_summary_rank"):GetComponent("Image")
		
	self._title_defeat.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "ui_summary_defeat")
	self._title_rank.sprite 	= CSpriteHelper:GetSpriteByName(self._sprites, "ui_summary_rank")

end

CUISummaryView.Destroy = function(self)
	CSpriteHelper:Clear()
	GameObject.Destroy(self._root)   
end
