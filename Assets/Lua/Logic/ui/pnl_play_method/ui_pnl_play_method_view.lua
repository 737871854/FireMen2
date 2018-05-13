CUIPnlPlayMethodView = class()

--视图的构造函数，定义相关的变量并且初始化
CUIPnlPlayMethodView.Init = function(self, prefabPath)

    self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)

    local rootTransform = self._root.transform

    self._BtnC_Bet = rootTransform:Find("ImgMethodBg/BtnC_Bet"):GetComponent("Button")
    self._BtnC_Plus = rootTransform:Find("ImgMethodBg/BtnC_Plus"):GetComponent("Button")
    self._BtnC_Add = rootTransform:Find("ImgMethodBg/BtnC_Add"):GetComponent("Button")
    self._TextC_Num = rootTransform:Find("ImgMethodBg/ImgBg/TextC_Num"):GetComponent("Text")
    self._TextC_BetMoney = rootTransform:Find("ImgLeftBg/TextC_BetMoney"):GetComponent("Text")
    self._TextC_Status = rootTransform:Find("ImgLeftBg/ImgStatus/TextC_Status"):GetComponent("Text")
    self._Nego_TextDan = rootTransform:Find("ImgLeftBg/Nego_TextDan")
    self._Nego_DanParent = rootTransform:Find("ImgLeftBg/Nego_TextDan/Nego_DanParent")
    self._Nego_TextTuo = rootTransform:Find("ImgLeftBg/Nego_TextTuo")
    self._Nego_TuoParent = rootTransform:Find("ImgLeftBg/Nego_TextTuo/Nego_TuoParent")
    self._Nego_Select = rootTransform:Find("ImgLeftBg/Nego_Select")
    self._Nego_Parent = rootTransform:Find("ImgLeftBg/Nego_Select/Nego_Parent")
    self._TogC_DanTuo = rootTransform:Find("ImgRightBg/TogC_DanTuo"):GetComponent("Toggle")
    self._Btn_Auto = rootTransform:Find("ImgRightBg/Btn_Auto"):GetComponent("Button")
    self._Btn_ReSelect = rootTransform:Find("ImgRightBg/Btn_ReSelect"):GetComponent("Button")
    self._Btn_RepeatLast = rootTransform:Find("ImgRightBg/Btn_RepeatLast"):GetComponent("Button")
    self._TogC_Q1 = rootTransform:Find("PlayMethodTabs/TogC_Q1Tabs"):GetComponent("Toggle")
    self._TogC_Q2 = rootTransform:Find("PlayMethodTabs/TogC_Q2Tabs"):GetComponent("Toggle")
    self._TogC_Q3 = rootTransform:Find("PlayMethodTabs/TogC_Q3Tabs"):GetComponent("Toggle")
    self._TogC_Q4 = rootTransform:Find("PlayMethodTabs/TogC_Q4Tabs"):GetComponent("Toggle")
    self._TogC_Q5 = rootTransform:Find("PlayMethodTabs/TogC_Q5Tabs"):GetComponent("Toggle")
    self._TogC_R2 = rootTransform:Find("PlayMethodTabs/TogC_R2"):GetComponent("Toggle")
    self._TogC_R3 = rootTransform:Find("PlayMethodTabs/TogC_R3"):GetComponent("Toggle")
    self._TogC_R4 = rootTransform:Find("PlayMethodTabs/TogC_R4"):GetComponent("Toggle")
    self._TogC_R5 = rootTransform:Find("PlayMethodTabs/TogC_R5"):GetComponent("Toggle")
    self._root:SetActive(false)

end

