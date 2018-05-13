CUIProgBarAndTextView = class()

--视图的构造函数，定义相关的变量并且初始化
CUIProgBarAndTextView.Init = function(self, prefabPath)

    self._root, self._asset = gGame:GetUIMgr():InitAsset(prefabPath)

    local rootTransform = self._root.transform

    self._TextC_RewordNum = rootTransform:Find("TextC_RewordNum"):GetComponent("Text")
    self._ImgC_ProgBar = rootTransform:Find("ImgC_ProgBar"):GetComponent("Image")
    self._TextC_ProgTime = rootTransform:Find("TextC_ProgTime"):GetComponent("Text")
    self._TextC_NowTime = rootTransform:Find("TextC_NowTime"):GetComponent("Text")

    self._ImgC_TenTimerBG = rootTransform:Find("ImgC_TenTimerBG"):GetComponent("Image")
    self._ImgC_TenTimer0 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer0"):GetComponent("Image")
    self._ImgC_TenTimer1 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer1"):GetComponent("Image")
    self._ImgC_TenTimer2 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer2"):GetComponent("Image")
    self._ImgC_TenTimer3 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer3"):GetComponent("Image")
    self._ImgC_TenTimer4 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer4"):GetComponent("Image")
    self._ImgC_TenTimer5 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer5"):GetComponent("Image")
    self._ImgC_TenTimer6 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer6"):GetComponent("Image")
    self._ImgC_TenTimer7 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer7"):GetComponent("Image")
    self._ImgC_TenTimer8 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer8"):GetComponent("Image")
    self._ImgC_TenTimer9 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer9"):GetComponent("Image")
    self._ImgC_TenTimer10 = rootTransform:Find("ImgC_TenTimerBG/ImgC_TenTimer10"):GetComponent("Image")

    self._root:SetActive(false)

end

