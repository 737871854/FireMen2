CSpriteHelper = class()


-- 加载并返回所有Sprite
CSpriteHelper.LoadAllSprites = function(self, path)

	self._sprites = ioo.LoadAllSprites(path)
	
	return self._sprites
end

-- 找到所有Image
CSpriteHelper.FindAllImage = function(self, tran)

	self._images		= ioo.FindAllImage(tran)
	
	return self._images
end

-- 从List<Sprite>中找到指定Sprite
CSpriteHelper.GetSpriteByName = function(self, sprites, name)
	for i = 0, sprites.Count - 1 do
		if sprites[i].name == name then
			return sprites[i]
		end
	end
end

-- 清除所有缓存图集
CSpriteHelper.Clear = function(self)
	self._sprites = nil
	self._images = nil
	ioo.Clear()
end