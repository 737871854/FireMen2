CUIPnlResultView = class()

--视图的构造函数，定义相关的变量并且初始化
CUIPnlResultView.Init = function(self, prefabPath)

    self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)

    local rootTransform = self._root.transform

    self._BtnClose = rootTransform:Find("BtnClose"):GetComponent("Button")
    self._TextC_Time = rootTransform:Find("BtnClose/TextC_Time"):GetComponent("Text")
    self._root:SetActive(false)

end

