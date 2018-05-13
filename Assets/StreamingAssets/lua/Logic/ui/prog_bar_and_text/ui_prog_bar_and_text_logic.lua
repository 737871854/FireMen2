local CUIBaseLogic = Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUIProgBarAndTextView = Import("logic/ui/prog_bar_and_text/ui_prog_bar_and_text_view").CUIProgBarAndTextView



CUIProgBarAndTextLogic = class(CUIBaseLogic)


--构造函数，初始化View，绑定事件等
CUIProgBarAndTextLogic.Init = function(self, id)
    self._prefabPath = "Assets/Prefabs/ui/snail/ProgBarAndText.prefab"
    self._view = CUIProgBarAndTextView:New(self._prefabPath)
    CUIBaseLogic.Init(self, id)

end

--绑定UI事件监听
CUIProgBarAndTextLogic.BindUIEvent = function(self)
    --编写格式如下:
    --UIEventListener.Get(self._view.XXX.gameObject).onClick = LuaHelper.VoidDelegate( function() self:HandlerBtnXXX() end )
end

CUIProgBarAndTextLogic.OnCreate = function(self)
    GameAudio.Instance:PlayAudio(0)
    local red = Color.New(1,0,0,1)
    local green = Color.New(0,1,0,1)
    self._time = 60
    self._timerId = gGame:GetTimeMgr():AddTimer(1, function()
        self._time = self._time - 1
        self._view._ImgC_ProgBar.fillAmount = self._time / 300
        if self._time < 30 then
            self._view._ImgC_ProgBar.color = red
        else
            self._view._ImgC_ProgBar.color = green
        end
        local sec = self._time % 60
        local min =(self._time - sec) / 60
        if min < 10 then
            min = "0" .. min
        end
        if sec < 10 then
            sec = "0" .. sec
        end
        self._view._TextC_ProgTime.text = "" .. min .. ":" .. sec
        if self._time <= 10 then 
            self._view._ImgC_TenTimerBG.gameObject:SetActive(true)
            self:HandlerTimerClear()
            self:HandlerTimerShow(self._time)
        end

        if self._time == 30 then 
            GameAudio.Instance:PlayAudio(7)
            GameAudio.Instance:Pause(0)
        end

        if self._time <= -1 then 
            gGame:GetTimeMgr():RemoveTimer(self._timerId)
            gGame:ChangeScene("game","gameScene")
        end
    end , true)
end

CUIProgBarAndTextLogic.OnDestroy = function(self)
    self._view._ImgC_TenTimerBG.gameObject:SetActive(false)
    gGame:GetTimeMgr():RemoveTimer(self._timerId)
    GameAudio.Instance:Pause(7)
end

CUIProgBarAndTextLogic.ReStartTime = function (self)

end

CUIProgBarAndTextLogic.HandlerTimerClear = function(self)
        self._view._ImgC_TenTimer0.gameObject:SetActive(false)
        self._view._ImgC_TenTimer1.gameObject:SetActive(false)
        self._view._ImgC_TenTimer2.gameObject:SetActive(false)
        self._view._ImgC_TenTimer3.gameObject:SetActive(false)
        self._view._ImgC_TenTimer4.gameObject:SetActive(false)
        self._view._ImgC_TenTimer5.gameObject:SetActive(false)
        self._view._ImgC_TenTimer6.gameObject:SetActive(false)
        self._view._ImgC_TenTimer7.gameObject:SetActive(false)
        self._view._ImgC_TenTimer8.gameObject:SetActive(false)
        self._view._ImgC_TenTimer9.gameObject:SetActive(false)
        self._view._ImgC_TenTimer10.gameObject:SetActive(false)
end

CUIProgBarAndTextLogic.HandlerTimerShow = function(self, timer)
    if timer == 1 then
        self._view._ImgC_TenTimer1.gameObject:SetActive(true)
        return
    end
    if timer == 0 then
        self._view._ImgC_TenTimer0.gameObject:SetActive(true)
        return
    end
    if timer == 2 then
        self._view._ImgC_TenTimer2.gameObject:SetActive(true)
        return
    end
    if timer == 3 then
        self._view._ImgC_TenTimer3.gameObject:SetActive(true)
        return
    end
    if timer == 4 then
        self._view._ImgC_TenTimer4.gameObject:SetActive(true)
        return
    end
    if timer == 5 then
        self._view._ImgC_TenTimer5.gameObject:SetActive(true)
        return
    end
    if timer == 6 then
        self._view._ImgC_TenTimer6.gameObject:SetActive(true)
        return
    end
    if timer == 7 then
        self._view._ImgC_TenTimer7.gameObject:SetActive(true)
        return
    end
    if timer == 8 then
        self._view._ImgC_TenTimer8.gameObject:SetActive(true)
        return
    end
    if timer == 9 then
        self._view._ImgC_TenTimer9.gameObject:SetActive(true)
        return
    end
    if timer == 10 then
        self._view._ImgC_TenTimer10.gameObject:SetActive(true)
        return
    end
end
