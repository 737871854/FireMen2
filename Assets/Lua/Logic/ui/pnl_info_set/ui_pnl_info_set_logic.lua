local CUIBaseLogic = Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUIPnlInfoSetView = Import("logic/ui/pnl_info_set/ui_pnl_info_set_view").CUIPnlInfoSetView

CUIPnlInfoSetLogic = class(CUIBaseLogic)

--构造函数，初始化View，绑定事件等
CUIPnlInfoSetLogic.Init = function(self, id)
    self._prefabPath = "Assets/Prefabs/ui/snail/PnlInfoSet.prefab"
    self._view = CUIPnlInfoSetView:New(self._prefabPath)
    CUIBaseLogic.Init(self, id)
end

--绑定UI事件监听
CUIPnlInfoSetLogic.BindUIEvent = function(self)
    --编写格式如下:
    --UIEventListener.Get(self._view.XXX.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnXXX() end )
    UIEventListener.Get(self._view._BtnC_Exit.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnC_Exit() end )
    UIEventListener.Get(self._view._BtnC_Account.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnC_Account() end )
    UIEventListener.Get(self._view._BtnC_Set.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnC_Set() end )
    UIEventListener.Get(self._view._BtnC_Help.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnC_Help() end )
    UIEventListener.Get(self._view._BtnAClose.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnAClose() end )
    UIEventListener.Get(self._view._BtnSClose.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnSClose() end )
    UIEventListener.Get(self._view._BtnHClose.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnHClose() end )
end

--按钮BtnC_Exit响应函数
CUIPnlInfoSetLogic.HandlerBtnC_Exit = function(self)
    Application.Quit()
end

--按钮BtnC_Account响应函数
CUIPnlInfoSetLogic.HandlerBtnC_Account = function(self)
    warn("HandlerBtnC_Account")
    self._view._Nego_Account.gameObject:SetActive(true)
end

--按钮BtnC_Set响应函数
CUIPnlInfoSetLogic.HandlerBtnC_Set = function(self)
    warn("HandlerBtnC_Set")
    self._view._Nego_Set.gameObject:SetActive(true)
end

--按钮BtnC_Help响应函数
CUIPnlInfoSetLogic.HandlerBtnC_Help = function(self)
    warn("HandlerBtnC_Help")
    self._view._Nego_Help.gameObject:SetActive(true)
end

--按钮BtnAClose响应函数
CUIPnlInfoSetLogic.HandlerBtnAClose = function(self)
    self._view._Nego_Account.gameObject:SetActive(false)
end

--按钮BtnSClose响应函数
CUIPnlInfoSetLogic.HandlerBtnSClose = function(self)
    self._view._Nego_Set.gameObject:SetActive(false)
end

--按钮BtnHClose响应函数
CUIPnlInfoSetLogic.HandlerBtnHClose = function(self)
    self._view._Nego_Help.gameObject:SetActive(false)
end

