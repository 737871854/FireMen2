local Tool = Import("logic/common/tool").CTool
local CUIBaseDialog = Import("logic/ui/common/ui_base_dialog").CUIBaseDialog

CUIFloatDialog = class(CUIBaseDialog)

CUIFloatDialog.Init = function(self, parentObj)
	local path = "Assets/Prefabs/ui/tips/Nego_Warning.prefab"
	-- 默认不是模态的
	CUIBaseDialog.Init(self, path, parentObj)
	self._isActive = true
	self._entryCount = 0
end

CUIFloatDialog.AddText = function(self, text, destoryTime)
	if not self._isActive then
        self:ShowSwitch(true)
    end
	local cloneTargetObj = Tool.GetChildRecursive(self._baseDialogObj, "TxtC_Warning")
	local cloneObj = GameObject.Instantiate(cloneTargetObj)
	local timeDestoryCom = cloneObj:GetComponent("UITimeDestory")
	timeDestoryCom.DestoryDelegate = LuaHelper.VoidDelegate( function() self:DestoryEntryCallback() end)
	cloneObj:SetActive(true)
	local texParentObj = Tool.GetChildRecursive(self._baseDialogObj, "ImgBg")
	cloneObj.transform:SetParent(texParentObj.transform, false)
	local texCom = cloneObj:GetComponent("Text")
	texCom.text = text
	self._entryCount = self._entryCount + 1
end

CUIFloatDialog.ShowSwitch = function(self, isShow)
	self._baseDialogObj:SetActive(isShow)
	self._isActive = isShow
end

CUIFloatDialog.DestoryEntryCallback = function(self)
	self._entryCount = self._entryCount - 1
	if self._entryCount == 0 then
		self:ShowSwitch(false)
	end 
end
