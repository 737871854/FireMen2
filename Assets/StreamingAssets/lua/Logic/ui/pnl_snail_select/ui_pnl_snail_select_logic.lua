local CUIBaseLogic = Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUIPnlSnailSelectView = Import("logic/ui/pnl_snail_select/ui_pnl_snail_select_view").CUIPnlSnailSelectView
local EventDefine = Import("logic/base/event_define").Define

CUIPnlSnailSelectLogic = class(CUIBaseLogic)

--构造函数，初始化View，绑定事件等
CUIPnlSnailSelectLogic.Init = function(self, id)
    self._prefabPath = "Assets/Prefabs/ui/snail/PnlSnailSelectAni.prefab"
    self._view = CUIPnlSnailSelectView:New(self._prefabPath)
    CUIBaseLogic.Init(self, id)
    -- 每个按钮的点击选中状态，这个控件可以考虑用Tog制作
    self._betData = gGame:GetDataMgr():GetBetData()
    self:HandlerEventClear()
    --gGame:HandleGlobalEvent(EventDefine.CLEAR_BET_INFO, function () self:HandlerEventClear() end)
    --self._betData.AddListener(EventDefine.CLEAR_BET_INFO, self:HandlerEventClear())
end

--绑定UI事件监听
CUIPnlSnailSelectLogic.BindUIEvent = function(self)
    --编写格式如下:
    --UIEventListener.Get(self._view.XXX.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnXXX() end )
    UIEventListener.Get(self._view._BtnSnail1.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnSnail1() end )
    UIEventListener.Get(self._view._BtnSnail2.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnSnail2() end )
    UIEventListener.Get(self._view._BtnSnail3.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnSnail3() end )
    UIEventListener.Get(self._view._BtnSnail4.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnSnail4() end )
    UIEventListener.Get(self._view._BtnSnail5.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnSnail5() end )
    UIEventListener.Get(self._view._BtnSnail6.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnSnail6() end )
end

--按钮BtnSnail1响应函数
CUIPnlSnailSelectLogic.HandlerBtnSnail1 = function(self)
    if self._snail1 then return end
    if self._betData:SelectSnail(1) == false then return end
    self._snail1 = true
    self._view._ImgC_Select1.gameObject:SetActive(true)
    GameAudio.Instance:PlayAudio(4)
    --gGame.FireGlobalEvent(EventDefine.CLEAR_BET_INFO)
    --self._betData.FireEvent(EventDefine.CLEAR_BET_INFO)
end

--按钮BtnSnail2响应函数
CUIPnlSnailSelectLogic.HandlerBtnSnail2 = function(self)
    if self._snail2 then
     return 
    end
    if self._betData:SelectSnail(2) == false then return end
    self._snail2 = true
    self._view._ImgC_Select2.gameObject:SetActive(true)
    GameAudio.Instance:PlayAudio(4)
end

--按钮BtnSnail3响应函数
CUIPnlSnailSelectLogic.HandlerBtnSnail3 = function(self)
    if self._snail3 then return end
    if self._betData:SelectSnail(3) == false then return end
    self._snail3 = true
    self._view._ImgC_Select3.gameObject:SetActive(true)
    GameAudio.Instance:PlayAudio(4)
end

--按钮BtnSnail4响应函数
CUIPnlSnailSelectLogic.HandlerBtnSnail4 = function(self)
    if self._snail4 then return end
    if self._betData:SelectSnail(4) == false then return end
    self._snail4 = true
    self._view._ImgC_Select4.gameObject:SetActive(true)
    GameAudio.Instance:PlayAudio(4)
end

--按钮BtnSnail5响应函数
CUIPnlSnailSelectLogic.HandlerBtnSnail5 = function(self)
    if self._snail5 then return end
    if self._betData:SelectSnail(5) == false then return end
    self._snail5 = true
    self._view._ImgC_Select5.gameObject:SetActive(true)
    GameAudio.Instance:PlayAudio(4)
 end

--按钮BtnSnail6响应函数
CUIPnlSnailSelectLogic.HandlerBtnSnail6 = function(self)
    if self._snail6 then return end
    if self._betData:SelectSnail(6) == false then return end
    self._snail6 = true
    self._view._ImgC_Select6.gameObject:SetActive(true)
    GameAudio.Instance:PlayAudio(4)
end

CUIPnlSnailSelectLogic.HandlerEventClear = function(self)
    warn("CUIPnlSnailSelectLogic.HandlerEventClear")
    self._snail1 = false
    self._snail2 = false
    self._snail3 = false
    self._snail4 = false
    self._snail5 = false
    self._snail6 = false
    self._view._ImgC_Select1.gameObject:SetActive(false)
    self._view._ImgC_Select2.gameObject:SetActive(false)
    self._view._ImgC_Select3.gameObject:SetActive(false)
    self._view._ImgC_Select4.gameObject:SetActive(false)
    self._view._ImgC_Select5.gameObject:SetActive(false)
    self._view._ImgC_Select6.gameObject:SetActive(false)
end

CUIPnlSnailSelectLogic.CallBtn = function (self, index)
    if index == 1 then
        self:HandlerBtnSnail1()
    end
    if index == 2 then
        self:HandlerBtnSnail2()
    end
    if index == 3 then
        self:HandlerBtnSnail3()
    end
    if index == 4 then
        self:HandlerBtnSnail4()
    end
    if index == 5 then
        self:HandlerBtnSnail5()
    end
    if index == 6 then
        self:HandlerBtnSnail6()
    end
end

CUIPnlSnailSelectLogic.GetRandSelect = function (self, num)
    local t = {1,2,3,4,5,6}
    for i=1,num do
        local index = math.random(1,#t)
        --local func = "HandlerBtnSnail"..t[index]
        self:CallBtn(t[index])
        table.remove(t,index)
    end
end

