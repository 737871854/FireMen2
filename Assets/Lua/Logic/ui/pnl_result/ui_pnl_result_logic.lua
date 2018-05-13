local CUIBaseLogic = Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUIPnlResultView = Import("logic/ui/pnl_result/ui_pnl_result_view").CUIPnlResultView

CUIPnlResultLogic = class(CUIBaseLogic)

--构造函数，初始化View，绑定事件等
CUIPnlResultLogic.Init = function(self, id)
    self._prefabPath = "Assets/Prefabs/ui/snail/PnlResult.prefab"
    self._view = CUIPnlResultView:New(self._prefabPath)
    CUIBaseLogic.Init(self, id)
end

--绑定UI事件监听
CUIPnlResultLogic.BindUIEvent = function(self)
    --编写格式如下:
    --UIEventListener.Get(self._view.XXX.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnXXX() end )
    UIEventListener.Get(self._view._BtnClose.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnClose() end )
end

CUIPnlResultLogic.OnCreate = function(self)
    self._view._BtnClose.interactable = false
    self._time = 15
    self._timerId = gGame:GetTimeMgr():AddTimer(1, function()
        self._time = self._time - 1
        if self._time < 11 then
            self._view._BtnClose.interactable = true
        end
        --self._view._ImgC_ProgBar.fillAmount = self._time / 300
        --self._view._TextC_ProgTime.text = "" .. min .. ":" .. sec
        self._view._TextC_Time.text = self._time
        if self._time <= 0 then
        	self:HandlerBtnClose()
        end
    end , true)
end
--按钮BtnClose响应函数
CUIPnlResultLogic.HandlerBtnClose = function(self)
    gGame:GetTimeMgr():RemoveTimer(self._timerId)
    
    gGame:ChangeScene("main","betScene")
end

