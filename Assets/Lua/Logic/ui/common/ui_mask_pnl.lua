CUIMaskPnl = class()

CUIMaskPnl.Init = function(self, parentObj)
    self._rootObj, self._asset = gGame:GetUIMgr():InitAsset("PnlMask")
    self._rootObj.transform:SetParent(parentObj.transform, false)
    self._rootObj:SetActive(false)
    self._callBack = nil
end

CUIMaskPnl.SwitchMaskPnl = function(self, isShow)
    self._rootObj:SetActive(isShow)
end

CUIMaskPnl.ShowSwitch = function (self, b)
    self._rootObj:SetActive(b)
end

CUIMaskPnl.GetRoot = function (self)
    return self._rootObj
end

CUIMaskPnl.SetAsFirstSibling = function (self)
    self._rootObj.transform:SetAsFirstSibling()
end

CUIMaskPnl.HandlerPnlMask = function(self)
    if self._callBack then
        self._callBack()
    end
end

CUIMaskPnl.AttachMaskPnl = function(self, Obj)
    -- 如果只有一个的话，就是siblingIndex = 1
    local parentSiblingIndex = Obj.transform:GetSiblingIndex()
    if parentSiblingIndex == 1 then
        self._rootObj.transform:SetSiblingIndex(parentSiblingIndex - 1)
    else
        self._rootObj.transform:SetSiblingIndex(parentSiblingIndex)
    end
end

-- 现在的回调函数是function() ... end
CUIMaskPnl.SetCallBack = function(self, callBack)
    self._callBack = callBack
    UIEventListener.Get(self._rootObj).onClick = LuaHelper.VoidDelegate( function() self:HandlerPnlMask() end )
end

CUIMaskPnl.ReleaseAsset = function(self)
    ioo.resourceManager:ReleaseAsset(self._asset)
    GameObject.Destroy(self._rootObj)
end

CUIMaskPnl.Destroy = function (self)
    self:ReleaseAsset()
end