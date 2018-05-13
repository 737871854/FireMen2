local UIFloatDialog = Import("logic/ui/common/ui_float_dialog").CUIFloatDialog
local UIAlertDialog = Import("logic/ui/common/ui_alert_dialog").CUIAlertDialog
local UIFloatMenu = Import("logic/ui/common/ui_float_menu").CUIFloatMenu
CUIManager = class()

CUIManager.Init = function(self)
	self._uiRootAsset = nil
	self._uiRootObj = nil
	-- 在ui_root.prefab中直接添加Canvas
	self._uiMainCanvasObj = nil
	self._uiSceneCanvasObj = nil
	self._uiCamera = nil
	self._counter = 0
	self._LogicInsList = {}
	self:InitUiRoot()
	self._floatDialog = nil
	self._floatMenu = nil
	self._DlgList = {}
end
--外部请勿调用
CUIManager.InitUiRoot = function(self)
	if self._uiRootObj ~= nil then return end
	self._uiRootAsset = ioo.resourceManager:LoadAsset("prefabs\\ui\\ui_root_ex", GameObject.GetClassType())
	self._uiRootObj = GameObject.Instantiate(self._uiRootAsset:GetAsset())
	GameObject.DontDestroyOnLoad(self._uiRootObj)
	--self._uiCamera = GameObject.FindGameObjectWithTag("UICamera"):GetComponent("Camera")
	self._uiCamera = self._uiRootObj.Find("ui_camera"):GetComponent("Camera")
	
	self._uiMainCanvasObj = self._uiRootObj.Find("main_canvas")
	self._uiSceneCanvasObj = self._uiRootObj.Find("scene_canvas")
	--self._uiMainCanvasObj:GetComponent("Canvas").worldCamera = self._uiCamera
    --self._uiSceneCanvasObj:GetComponent("Canvas").worldCamera = self._uiCamera
end

--传入全路径,initAsset放在这里就是为了可以统一在uiMgr中处理ui的Asset
CUIManager.InitAsset = function(self, prefabPath)
    local asset = ioo.resourceManager:LoadAsset(prefabPath, GameObject.GetClassType())
    local prefab = asset:GetAsset()
    local obj = GameObject.Instantiate(prefab)
    return obj, asset
end

-- 释放UImgr的资源
CUIManager.Release = function(self)
	if self._uiRootAsset then
		ioo.resourceManager:ReleaseAsset(self._uiRootAsset)
	end
end

CUIManager.AddLogicIns = function(self, id, inc)
	self._LogicInsList[id] = inc
end

CUIManager.GetLogicIns = function(self, id)
	return self._LogicInsList[id]
end

CUIManager.GenerateId = function(self)
	self._counter = self._counter + 1
	return self._counter
end
--內部使用的函數，請不要在此文件之外調用
CUIManager.CreateWindow = function(self, id, uiLogicClass, parentTran)
	assert(uiLogicClass ~= nil)
	parentTran = parentTran or self._uiMainCanvasObj.transform
	local LogicIns = uiLogicClass:New(id)
	--确保Init之后调用的
	LogicIns:OnCreate()
	LogicIns:SetParentTran(parentTran)
	-- 回调函数
	LogicIns:ShowSwitch(true)
	return LogicIns
end

-- 创造一个自动生成Id的界面,默认是在mainCanvas上
CUIManager.CreateWindowAuto = function(self, uiLogicClass, parentTran)
	local LogicInsId = self:GenerateId()
	local LogicIns = self:CreateWindow(LogicInsId, uiLogicClass, parentTran)
	self:AddLogicIns(LogicInsId, LogicIns)
	return LogicIns
end

-- 创造一个自定义Id的界面，(用于频繁打开关闭的界面使用)
-- customId使用"string",这些string最好在global_enum.lua的UICustomID中base
CUIManager.CreateWindowCustom = function(self, customId, uiLogicClass, parentTran)
	local LogicInsId = customId
	local LogicIns = self:GetLogicIns(LogicInsId)
	if LogicIns then		
		LogicIns:ShowSwitch(true)
		return LogicIns
	else
		LogicIns = self:CreateWindow(LogicInsId, uiLogicClass, parentTran)
		self:AddLogicIns(LogicInsId, LogicIns)
		return LogicIns
	end
end

-- 销毁一个界面
CUIManager.DestroyWindow = function(self, uiLogicId)
	local LogicIns = self:GetLogicIns(uiLogicId)				
	assert(LogicIns ~= nil, uiLogicId)
	-- 在UIManager去除这个LogicIns	
	self._LogicInsList[uiLogicId] = nil
	LogicIns:OnDestroy()
end

CUIManager.Clear = function(self)
	for k, v in pairs(self._LogicInsList) do
		if v then 
			v:OnDestroy()
		end
	end
	self._LogicInsList = {}
	self._counter = 0

	--清理所有的dialog
	if self._floatDialog ~= nil then
		self._floatDialog:ReleaseAsset()
		self._floatDialog = nil
	end

	for k, v in pairs(self._DlgList) do
		if v then 
			v:ReleaseAsset()
		end
	end
	self._DlgList = {}
end

-- uimanger自己的销毁
CUIManager.Destroy = function(self)
	GameObject.Destroy(self._uiRootObj)
	self:Clear()
end

CUIManager.GetSceneCanvasObj = function(self)
	return self._uiSceneCanvasObj
end

CUIManager.GetMainCanvasObj = function(self)
	return self._uiMainCanvasObj
end

CUIManager.GetUICamera = function(self)
	if not self._uiCamera then print("#_#Has no UI Camera!!!") end
	return self._uiCamera
end

--float
CUIManager.OpenFloatDialog = function(self, text)
    if not self._floatDialog then 
        self._floatDialog = UIFloatDialog:New(self._uiMainCanvasObj)
    end
    --这个飘窗应该放在画布最后面，不然会被挡着
    self._floatDialog._baseDialogObj.transform:SetAsLastSibling()
    self._floatDialog:AddText(text)
end

CUIManager.OpenAlertDialog = function(self, titleStr, contextStr, btnContextStr, btnCallback, isMode)
	local dlgId = self:GenerateId()
    local alertDialog = UIAlertDialog:New(self._uiMainCanvasObj, isMode)
    self._DlgList[dlgId] = alertDialog
    alertDialog:SetTextContext("TxtC_Title", titleStr)
    alertDialog:SetTextContext("TxtC_TipsContent", contextStr)
    alertDialog:SetTextContext("TxtC_BtnContent", btnContextStr)
    local modifyBtnCallback = function()
        if btnCallback then
            btnCallback()
        end
        self._DlgList[dlgId] = nil
        alertDialog:ReleaseAsset()
    end
    alertDialog:SetMiddleBtnCallback(modifyBtnCallback)
end

CUIManager.OpenFloatMenu = function(self, go, targetPos)
	self._floatMenu = UIFloatMenu:New(go, targetPos)
	return self._floatMenu
end

CUIManager.ClearFloatMenu = function(self)
	if self._floatMenu then 
		self._floatMenu:Destroy() 
	end
end

