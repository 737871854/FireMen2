local Tool = Import("logic/common/tool").CTool
local CUIMaskPnl = Import("logic/ui/common/ui_mask_pnl").CUIMaskPnl

CUIBaseDialog = class()

CUIBaseDialog.Init = function(self, path, parentObj, isMode)
    self._baseDialogAsset = nil
    self._baseDialogObj = nil
    self._isMode = isMode or false
    self._parentObj = parentObj or nil
    self._maskPnl = nil
    if path and parentObj then
        self:Instantiate(path, parentObj)
    else
        error("Base Dialog Init Error")
    end
    if self._isMode then 
        self:InstantiateMaskPanel()

    end
	--self:SetTitle(title or "")
	--self:SetLabelContext(context or "")
end

CUIBaseDialog.Instantiate = function(self, path, parentObj)
    self._baseDialogObj, self._baseDialogAsset = Tool.InstantiateToParent(path, parentObj.transform)
end

CUIBaseDialog.InstantiateMaskPanel = function(self)
    self._maskPnl = CUIMaskPnl:New(self._parentObj)
    self._maskPnl:AttachMaskPnl(self._baseDialogObj)
    self._maskPnl:SwitchMaskPnl(true)
    --local dialogSibingIndex = self._gameObject.transform:GetSiblingIndex()
    --local maskPanelSibingIndex = dialogSibingIndex
    --self._maskPanelObj = Tool.InstantiateToParent("Assets/Prefabs/ui/MaskPanel.prefab", self._parentObj)
    --self._maskPanelObj.transform:SetSiblingIndex(maskPanelSibingIndex)
    --local rectTran = self._maskPanelObj:GetComponent("RectTransform")
    --rectTran.offsetMin = Vector2.zero
    --rectTran.offsetMax = Vector2.zero
end

CUIBaseDialog.ReleaseAsset = function(self)
    ioo.resourceManager:ReleaseAsset(self._baseDialogAsset)
    if self._maskPnl then
        self._maskPnl:ReleaseAsset()
    end
    GameObject.Destroy(self._baseDialogObj)
end

CUIBaseDialog.ShowSwitch = function(self, isShow)
    if isShow then
        self._maskPnl:AttachMaskPnl(self._baseDialogObj)
    end
    self._baseDialogObj:SetActive(isShow)
    self._maskPnl:SwitchMaskPnl(isShow)
end

CUIBaseDialog.SetTextContext = function(self, texName , context, parentObj)
    parentObj = parentObj or self._baseDialogObj
    local obj = Tool.GetChildRecursive(parentObj, texName)
    local textCom = obj:GetComponent("Text")
    textCom.text = context 
end

CUIBaseDialog.SetBtnCallBack = function(self, buttonName, callBack, parentObj)
    parentObj = parentObj or self._baseDialogObj
    local obj = Tool.GetChildRecursive(parentObj, buttonName)
    local btnCom = obj:GetComponent("Button")
    UIEventListener.Get(obj).onClick = LuaHelper.VoidDelegate( callBack )
end

CUIBaseDialog.GetBaseDialogObj = function(self)
    return self._baseDialogObj
end
