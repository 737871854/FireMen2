local Tool = Import("logic/common/tool").CTool
local CUIMaskPnl = Import("logic/ui/common/ui_mask_pnl").CUIMaskPnl

CUIFloatMenu = class()

local kValue = 10

CUIFloatMenu.Init = function(self, parentObj, targetPos)
	local containerPath = "Assets/Prefabs/ui/common/MenuButton.prefab"
    self._containerAsset = ioo.resourceManager:LoadAsset(containerPath, GameObject.GetClassType())
    local prefab = self._containerAsset:GetAsset()
    self._rootObj = GameObject.Instantiate(prefab)
    self._containerObj = Tool.GetChildRecursive(self._rootObj, "PnlFunction")
    self._imgObj = Tool.GetChildRecursive(self._rootObj, "ImgC_JianTou")

	self._entryAssetList = {}
	self._entryObjList = {}
	-- 遮罩层
	self._maskPnl = nil
	self._oneHeight = 0
	self._spaceValue = 0
	self._cardTop = 0
	self._cardBotm = 0
	self._topValue = 0    --菜单栏不能超出的上边界数值
	self._bottomValue = 0  --菜单栏不能超出的下边界数值
	self._rightValue = 0   --菜单栏不能超出的右边界数值

	self:getModuleData()   --获取菜单栏一些数据
	self:setTransPos(parentObj, targetPos)  -- 设置菜单栏位置的相对数据
	self:InitMaskPnl()
end

CUIFloatMenu.getModuleData = function(self)
	local group = self._containerObj:GetComponent("GridLayoutGroup")
	self._oneHeight = math.floor(group.cellSize.y)
	self._spaceValue = math.max(group.spacing.y, kValue)
	self._cardTop = group.padding.top - kValue
	self._cardBotm = group.padding.bottom - kValue
end

CUIFloatMenu.setTransPos = function(self, parentObj, targetPos)
	local scalex = gGame:GetUIManager():GetCurMainLogic():GetMainCanvasObj():GetComponent("RectTransform").rect.width / Screen.width
	local scaley = gGame:GetUIManager():GetCurMainLogic():GetMainCanvasObj():GetComponent("RectTransform").rect.height / Screen.height

	local rectTransform = parentObj:GetComponent("RectTransform")
	local pivot = rectTransform.pivot
	local parentPos = gGame:GetUIManager():GetUICamera():WorldToScreenPoint(rectTransform.position)
	parentPos.x = parentPos.x * scalex
	parentPos.y = parentPos.y * scaley

	self._topValue = parentPos.y + (1 - pivot.y) * rectTransform.rect.height
	self._bottomValue = parentPos.y - rectTransform.rect.height * pivot.y
	self._rightValue = parentPos.x + (1 - pivot.x) * rectTransform.rect.width

	local imgObjWidth = self._imgObj:GetComponent("RectTransform").rect.width
	local containWidth = self._containerObj:GetComponent("RectTransform").rect.width
	targetPos.x = targetPos.x * scalex
	targetPos.y = targetPos.y * scaley
	if targetPos.x + imgObjWidth + containWidth >= self._rightValue then
		targetPos.x = self._rightValue - (imgObjWidth + containWidth + kValue)
	end

	self._rootObj.transform.anchoredPosition = targetPos
    self._rootObj.transform:SetParent(gGame:GetUIManager():GetCurMainLogic():GetMainCanvasObj().transform, false)
end

CUIFloatMenu.initContainPos = function(self)
	local rootObjHeight = self._rootObj:GetComponent("RectTransform").rect.height
	local containPos = self._containerObj.transform.anchoredPosition
	local pointPos = self._rootObj.transform.anchoredPosition
	local imgPos = self._imgObj.transform.anchoredPosition
	local containHeight = (#self._entryObjList) * self._oneHeight + (#self._entryObjList - 1) * self._spaceValue + self._cardBotm + self._cardTop
	local containWidth = self._containerObj:GetComponent("RectTransform").rect.width
	local conValue = math.floor(containHeight / 2)
	local imgObjWidth = self._imgObj:GetComponent("RectTransform").rect.width
	local tmpvalueW = math.floor(imgObjWidth * 5 / 6)

	containPos.x = imgPos.x + tmpvalueW
	if (pointPos.y + conValue) >= self._topValue then
		containPos.y = self._topValue - rootObjHeight - pointPos.y
	elseif (pointPos.y - conValue) <= self._bottomValue then
		containPos.y = self._bottomValue + conValue + kValue - pointPos.y

	else
		containPos.y = conValue - rootObjHeight

	end

	self._containerObj.transform.anchoredPosition = containPos
end

CUIFloatMenu.InitMaskPnl = function(self)
	self._maskPnl = CUIMaskPnl:New(gGame:GetUIManager():GetCurMainLogic():GetMainCanvasObj().transform)
    self._maskPnl:AttachMaskPnl(self._rootObj)
    self._maskPnl:SwitchMaskPnl(true)
    self._maskPnl:SetCallBack(function() 
    	self:Destroy() 
    end)
end

CUIFloatMenu.Add = function(self, btnStr, btnCb)
	local entryPath = "Assets/Prefabs/ui/common/BtnFunction.prefab"
	local entryObj, entryAsset = Tool.InstantiateToParent(entryPath, self._containerObj.transform)
	table.insert(self._entryAssetList, entryAsset)
	table.insert(self._entryObjList, entryObj)
	--local entryContextText = entryObj.Find("TxtC_Function"):GetComponent("Text")
	local entryContextText = Tool.GetChildRecursive(entryObj, "TxtC_Function"):GetComponent("Text")
	entryContextText.text = btnStr

	UIEventListener.Get(entryObj).onClick = LuaHelper.VoidDelegate( function() 
		btnCb() 
		self:Destroy()
		gGame:GetUIManagerEx():ClearFloatMenu()
		end )
end

CUIFloatMenu.Destroy = function(self)
	self._maskPnl:ReleaseAsset()
	-- GameObject.Destroy(self._containerObj)
	GameObject.Destroy(self._rootObj)
	-- for k, v in pairs(self._entryObjList) do
	-- 	GameObject.Destroy(v)
	-- end
end