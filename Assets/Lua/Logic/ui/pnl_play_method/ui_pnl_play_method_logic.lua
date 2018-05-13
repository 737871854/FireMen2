local CUIBaseLogic = Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUIPnlPlayMethodView = Import("logic/ui/pnl_play_method/ui_pnl_play_method_view").CUIPnlPlayMethodView
local UIPnlSnailSelectLogic = Import("logic/ui/pnl_snail_select/ui_pnl_snail_select_logic").CUIPnlSnailSelectLogic 

local EventDefine = Import("logic/base/event_define").Define

local BetMethod = Import("logic/data/bet_data").BetMethod
local BetType = Import("logic/data/bet_data").BetType
local BetSelectType = Import("logic/data/bet_data").BetSelectType


CUIPnlPlayMethodLogic = class(CUIBaseLogic)

--构造函数，初始化View，绑定事件等
CUIPnlPlayMethodLogic.Init = function(self, id)
    self._prefabPath = "Assets/Prefabs/ui/snail/PnlPlayMethod.prefab"
    self._view = CUIPnlPlayMethodView:New(self._prefabPath)
    CUIBaseLogic.Init(self, id)
    
    self._snailSelect = gGame:GetUIMgr():CreateWindowAuto(UIPnlSnailSelectLogic)
    self._snailSelect:SetParentLogic(self)
end
--绑定UI事件监听
CUIPnlPlayMethodLogic.BindUIEvent = function(self)
    --编写格式如下:
    --UIEventListener.Get(self._view.XXX.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnXXX() end )
    UIEventListener.Get(self._view._BtnC_Bet.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnC_Bet() end )
    UIEventListener.Get(self._view._BtnC_Plus.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnC_Plus() end )
    UIEventListener.Get(self._view._BtnC_Add.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnC_Add() end )
    UIEventListener.Get(self._view._Btn_Auto.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtn_Auto() end )
    UIEventListener.Get(self._view._Btn_ReSelect.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtn_ReSelect() end )
    UIEventListener.Get(self._view._Btn_RepeatLast.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtn_RepeatLast() end )

    UIEventListener.Get(self._view._TogC_Q1.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandTog() end )
    UIEventListener.Get(self._view._TogC_Q2.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandTog() end )
    UIEventListener.Get(self._view._TogC_Q3.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandTog() end )
    UIEventListener.Get(self._view._TogC_Q4.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandTog() end )
    UIEventListener.Get(self._view._TogC_Q5.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandTog() end )
    UIEventListener.Get(self._view._TogC_R2.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandTog() end )
    UIEventListener.Get(self._view._TogC_R3.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandTog() end )
    UIEventListener.Get(self._view._TogC_R4.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandTog() end )
    UIEventListener.Get(self._view._TogC_R5.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandTog() end )

    UIEventListener.Get(self._view._TogC_DanTuo.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandDanTuo() end )
end

CUIPnlPlayMethodLogic.OnCreate = function(self)
    self._betData = gGame:GetDataMgr():GetBetData()
    self._numbers = {} --存储所有的数字go
    self._times = 1 --赌注倍数
    for i=1,6 do
        local prefabName = "Assets/Prefabs/ui/snail/ImgNum"..i..".prefab"
        self._numbers[i],self._temp = gGame:GetUIMgr():InitAsset(prefabName)
        self._numbers[i]:SetActive(false)
    end
    self:SetBetType(BetType.Single)
    self._view._TogC_Q1.isOn  = true 
    self:canDanTuo(false)
    local txtMoney = "<color=red>0</color>元"
    self._view._TextC_BetMoney.text = txtMoney

    --self:HandTog()
    --暂时屏蔽重复上次选择
    --self._view._Btn_RepeatLast.interactable =false

    RegTrigger(EventDefine.CLEAR_BET_DATA, function () self:ClearBetInfo() end, "pnl_play_method")
    RegTrigger(EventDefine.CHANGE_BET_TYPE,function(_betType) self:SetBetType(_betType) end,"pnl_play_method")
    RegTrigger(EventDefine.ADD_SNAIL_INDEX,function (_index)  self:AddNumber(_index) end, "pnl_play_method" )
    RegTrigger(EventDefine.CHANGE_SELECT_DanTuo,function (_selectType)
            if _selectType == BetSelectType.Dan then
                self:ChangeToDan()
            else
                self:ChangeToTuo()
            end
        end, "pnl_play_method" )
end
-- destory的回调方法
CUIPnlPlayMethodLogic.OnDestroy = function(self)
    warn("CUIPnlPlayMethodLogic.OnDestroy")
    for i=1,6 do
        GameObject.Destroy(self._numbers[i])
    end
    self._numbers = {}
    UnRegTrigger(EventDefine.CLEAR_BET_INFO, "pnl_play_method")
    UnRegTrigger(EventDefine.CHANGE_BET_TYPE, "pnl_play_method")
    UnRegTrigger(EventDefine.ADD_SNAIL_INDEX, "pnl_play_method" )
    UnRegTrigger(EventDefine.CHANGE_SELECT_DanTuo, "pnl_play_method" )
    self._betData:ClearAll()
end
--处理胆拖选项点击时的显示
CUIPnlPlayMethodLogic.HandDanTuo = function (self)
    local canPlay = true
    if self._view._TogC_DanTuo.isOn then
        canPlay = false
        --self._view._TogC_R2.isOn = true --自动选择任二
        self._view._Nego_Select.gameObject:SetActive(false)
        self._view._Nego_TextDan.gameObject:SetActive(true)
        self._view._Nego_TextTuo.gameObject:SetActive(true)
    else
        self._view._Nego_Select.gameObject:SetActive(true)
        self._view._Nego_TextDan.gameObject:SetActive(false)
        self._view._Nego_TextTuo.gameObject:SetActive(false)
    end
    self._view._TogC_Q1.interactable = canPlay
    self._view._TogC_Q2.interactable = canPlay
    self._view._TogC_Q3.interactable = canPlay
    self._view._TogC_Q4.interactable = canPlay
    self._view._TogC_Q5.interactable = canPlay

    self._betData:SetDanTuo(self._view._TogC_DanTuo.isOn)
end

--选中某种玩法的设置
CUIPnlPlayMethodLogic.HandTog = function (self)
    warn("Hand Play Method Tog")
    if self._view._TogC_Q1.isOn then 
        self._betData:SetBetMethod(BetMethod.Q1)
        self:canDanTuo(false)
    end
    if self._view._TogC_Q2.isOn then self._betData:SetBetMethod(BetMethod.Q2) self:canDanTuo(false) end
    if self._view._TogC_Q3.isOn then self._betData:SetBetMethod(BetMethod.Q3) self:canDanTuo(false) end
    if self._view._TogC_Q4.isOn then self._betData:SetBetMethod(BetMethod.Q4) self:canDanTuo(false) end
    if self._view._TogC_Q5.isOn then self._betData:SetBetMethod(BetMethod.Q5) self:canDanTuo(false) end

    if self._view._TogC_R2.isOn then self._betData:SetBetMethod(BetMethod.R2) self:canDanTuo(true) end
    if self._view._TogC_R3.isOn then self._betData:SetBetMethod(BetMethod.R3) self:canDanTuo(true) end
    if self._view._TogC_R4.isOn then self._betData:SetBetMethod(BetMethod.R4) self:canDanTuo(true) end
    if self._view._TogC_R5.isOn then self._betData:SetBetMethod(BetMethod.R5) self:canDanTuo(true) end
end

--按钮BtnC_Bet响应函数
CUIPnlPlayMethodLogic.HandlerBtnC_Bet = function(self)

    if self._betData:CanBet() then
        gGame:GetUIMgr():OpenAlertDialog("提示", "恭喜你，下注成功", "确认", nil, false)
        self._betData:ClearAll()
        GameAudio.Instance:PlayAudio(6)
    else
        gGame:GetUIMgr():OpenAlertDialog("不可下注", "号码不够，请继续选号", "确认", nil, false)
        GameAudio.Instance:PlayAudio(5)
    end
    -- gGame:GetUIMgr():OpenFloatDialog("恭喜你，下注成功！")
    -- gGame:GetSceneMgr():GetCurrentScene():ShowBetInfo(false)
end

--按钮BtnC_Plus响应函数
CUIPnlPlayMethodLogic.HandlerBtnC_Plus = function(self)
    --gGame:GetSceneMgr():GetCurrentScene():ShowBetInfo(false)
    if self._times == 1 then return end
    self._times = self._times - 1
    self:diplayBetMoney()
end

--按钮BtnC_Add响应函数
CUIPnlPlayMethodLogic.HandlerBtnC_Add = function(self)
    if self._times == 99 then return end
    self._times = self._times + 1
    self:diplayBetMoney()
end


--按钮Btn_Auto响应函数
CUIPnlPlayMethodLogic.HandlerBtn_Auto = function(self)
    self._betData:ClearAll()
    local num = self._betData._betMethod
    if num > BetMethod.QEnd then
        num = num - 10
    end
    self._snailSelect:GetRandSelect(num)
    GameAudio.Instance:PlayAudio(4)
end

--按钮Btn_ReSelect响应函数
CUIPnlPlayMethodLogic.HandlerBtn_ReSelect = function(self)
    self._betData:ClearAll()
end

--按钮Btn_RepeatLast响应函数
CUIPnlPlayMethodLogic.HandlerBtn_RepeatLast = function(self)
    gGame:ChangeScene("game","gameScene")
end
-------------------------------------------------内部函数-------------------------------
CUIPnlPlayMethodLogic.diplayBetMoney = function (self)
    local money = self._betData:GetBetMoney()
    --if money < 2 then money = 2 end
    money = money * self._times
    local txtMoney = "合计：<color=red>"..money.."</color>元"
    self._view._TextC_BetMoney.text = txtMoney
    self._view._TextC_Num.text = self._times.."倍"
end
--当前是否可以点击胆拖按钮
CUIPnlPlayMethodLogic.canDanTuo = function (self, canDanTuo)
    if canDanTuo then 
        self._view._TogC_DanTuo.interactable = true
    else
        self._view._TogC_DanTuo.interactable = false
        self._view._TogC_DanTuo.isOn =  false
    end
end
-------------------------------------------------外部调用函数---------------------------
--添加当前玩法买蜗牛的数字
CUIPnlPlayMethodLogic.AddNumber = function (self, num)
    --warn("Add Number to dan tuo")
    if self._view._TogC_DanTuo.isOn then
        local dan = self._view._Nego_TextDan:GetComponent("Toggle")
        if dan.isOn then
            self._numbers[num].transform:SetParent(self._view._Nego_DanParent.transform, false)
        else
            self._numbers[num].transform:SetParent(self._view._Nego_TuoParent.transform, false)
        end
    else
        self._numbers[num].transform:SetParent(self._view._Nego_Parent.transform, false)
    end
    self._numbers[num]:SetActive(true)
    self:diplayBetMoney()
    --根据当前状态显示
end

CUIPnlPlayMethodLogic.SetBetType = function (self, betType)
    if betType == BetType.Single then self._view._TextC_Status.text = "单选" end
    if betType == BetType.Multi then self._view._TextC_Status.text = "复选" end
    if betType == BetType.DanTuo then self._view._TextC_Status.text = "胆拖" end
end

--改为选择拖号
CUIPnlPlayMethodLogic.ChangeToTuo = function(self)
    local tuo = self._view._Nego_TextTuo:GetComponent("Toggle")
    tuo.isOn = true
end

CUIPnlPlayMethodLogic.ChangeToDan = function(self)
    local dan = self._view._Nego_TextDan:GetComponent("Toggle")
    dan.isOn = true
end


--获得当前胆拖选择状态
CUIPnlPlayMethodLogic.GetSelectType = function (self)
    if self._view._TogC_DanTuo.isOn then
        local dan = self._view._Nego_TextDan:GetComponent("Toggle")
        if dan.isOn then
            return BetSelectType.Dan
        else
            return BetSelectType.Tuo
        end
    else
        return BetSelectType.NONE
    end
end

CUIPnlPlayMethodLogic.GetIsDanTuo = function (self)
    return self._view._TogC_DanTuo.isOn
end

CUIPnlPlayMethodLogic.CanBet = function (self)
   self._view._BtnC_Bet.interactable = false
end
--清理下注信息，但是这里不改变下注方法和倍数，只是清理押中的蜗牛和下注金额
CUIPnlPlayMethodLogic.ClearBetInfo = function (self)
    for i=1,6 do
        if self._numbers[i] ~= nil then
            self._numbers[i].transform:SetParent(nil,false)
            self._numbers[i]:SetActive(false)
        end
    end
    if self._view._TogC_DanTuo.isOn then
        self:SetBetType(BetType.DanTuo)
    else
        self:SetBetType(BetType.Single)
    end
    --切换玩法之后，默认选择膽碼
    local dan = self._view._Nego_TextDan:GetComponent("Toggle")
    dan.isOn = true
    self._snailSelect:HandlerEventClear()
    self:diplayBetMoney()
end





