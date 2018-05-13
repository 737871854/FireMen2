local CSpriteHelper = Import("logic/ui/sprite_helper").CSpriteHelper

CUISummaryView = class()

--��ͼ�Ĺ��캯����������صı������ҳ�ʼ��
CUISummaryView.Init = function(self, prefabPath)
	self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)
	
	local rootTransform = self._root.transform

	-- �ҵ�����Image
	self._images  = CSpriteHelper:FindAllImage(rootTransform)
	-- ����Atlas
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
