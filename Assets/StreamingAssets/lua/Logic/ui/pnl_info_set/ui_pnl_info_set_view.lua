CUIPnlInfoSetView = class()

--视图的构造函数，定义相关的变量并且初始化
CUIPnlInfoSetView.Init = function(self, prefabPath)

    self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)

    local rootTransform = self._root.transform

    self._TextC_AccountMoney = rootTransform:Find("TextC_AccountMoney"):GetComponent("Text")
    self._BtnC_Exit = rootTransform:Find("BtnC_Exit"):GetComponent("Button")
    self._BtnC_Account = rootTransform:Find("BtnC_Account"):GetComponent("Button")
    self._BtnC_Set = rootTransform:Find("BtnC_Set"):GetComponent("Button")
    self._BtnC_Help = rootTransform:Find("BtnC_Help"):GetComponent("Button")
    self._Nego_Account = rootTransform:Find("Nego_Account")
    self._BtnAClose = rootTransform:Find("Nego_Account/BtnAClose"):GetComponent("Button")
    self._Nego_Set = rootTransform:Find("Nego_Set")
    self._BtnSClose = rootTransform:Find("Nego_Set/BtnSClose"):GetComponent("Button")
    self._Nego_Help = rootTransform:Find("Nego_Help")
    self._BtnHClose = rootTransform:Find("Nego_Help/BtnHClose"):GetComponent("Button")
    self._root:SetActive(false)

end

