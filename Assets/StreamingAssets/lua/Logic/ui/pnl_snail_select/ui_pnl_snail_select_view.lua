CUIPnlSnailSelectView = class()

--视图的构造函数，定义相关的变量并且初始化
CUIPnlSnailSelectView.Init = function(self, prefabPath)

    self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)

    local rootTransform = self._root.transform

    self._Nego_PnlBtnSnail = rootTransform:Find("Nego_PnlBtnSnail")
    self._BtnSnail1 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail1"):GetComponent("Button")
    self._ImgC_Select1 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail1/ImgC_Select"):GetComponent("Image")
    self._BtnSnail2 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail2"):GetComponent("Button")
    self._ImgC_Select2 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail2/ImgC_Select"):GetComponent("Image")
    self._BtnSnail3 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail3"):GetComponent("Button")
    self._ImgC_Select3 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail3/ImgC_Select"):GetComponent("Image")
    self._BtnSnail4 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail4"):GetComponent("Button")
    self._ImgC_Select4 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail4/ImgC_Select"):GetComponent("Image")
    self._BtnSnail5 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail5"):GetComponent("Button")
    self._ImgC_Select5 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail5/ImgC_Select"):GetComponent("Image")
    self._BtnSnail6 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail6"):GetComponent("Button")
    self._ImgC_Select6 = rootTransform:Find("Nego_PnlBtnSnail/BtnSnail6/ImgC_Select"):GetComponent("Image")
    self._root:SetActive(false)

end

