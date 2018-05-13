local Tool = Import("logic/common/tool").CTool
local CUIBaseDialog = Import("logic/ui/common/ui_base_dialog").CUIBaseDialog
CUIAlertDialog = class (CUIBaseDialog)


CUIAlertDialog.Init = function(self, parentObj, isMode)
	local path = "Assets/Prefabs/ui/tips/Nego_DoubleWindow.prefab"
	local btnPath = "Assets/Prefabs/ui/tips/PnlBtnContent.prefab"
	CUIBaseDialog.Init(self, path, parentObj, isMode)
	local btnParentObj = Tool.GetChildRecursive(self._baseDialogObj, "Nego_BtnMiddle")
	self._middleBtnObj, self._middleBtnAsset = Tool.InstantiateToParent(btnPath, btnParentObj.transform)
end

CUIAlertDialog.SetMiddleBtnCallback = function(self, callBack)
	self:SetBtnCallBack( "BtnContent", callBack, self._middleBtnObj)
end

CUIAlertDialog.ReleaseAsset = function(self)
	CUIBaseDialog.ReleaseAsset(self)
	ioo.resourceManager:ReleaseAsset(self._middleBtnAsset)
	GameObject.Destroy(self._middleBtnObj)
end

