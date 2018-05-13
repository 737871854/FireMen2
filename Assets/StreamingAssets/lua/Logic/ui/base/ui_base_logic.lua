CUIBaseLogic = class()

--构造函数，初始化View，绑定事件等
CUIBaseLogic.Init = function(self, id)
    self._id = id
    self._parentLogic = nil
    self._childLogicList = {}
    self:BindUIEvent()
    self._isShow = false
end

CUIBaseLogic.SetParentTran = function(self, parentTran)
    self._view._root.transform:SetParent(parentTran, false)
end

CUIBaseLogic.SetParentLogic = function(self, parentLogic)
    self._parentLogic = parentLogic
    self._parentLogic:AddChildLogic(self) 
end

CUIBaseLogic.AddChildLogic = function(self, childLogic)
    local childId = childLogic:GetId()
    self._childLogicList[childId] = childLogic
end

CUIBaseLogic.GetParentLogic = function(self)
    return self._parentLogic
end

--获取孩子逻辑
CUIBaseLogic.GetChildLogic = function(self, id)
    return self._childLogicList[id]
end

CUIBaseLogic.GetId = function(self)
    return self._id
end

-- 设置显示
CUIBaseLogic.SetActive = function(self, isActive)
    self._view._root:SetActive(isActive)
end

-- create的回调方法
CUIBaseLogic.OnCreate = function(self)
end

-- destory的回调方法
CUIBaseLogic.OnDestroy = function(self)
end

CUIBaseLogic.ShowSwitch = function(self, isShow)
    if isShow and self._isShow ~= isShow then
        self:OnShow()
        for k, v in pairs(self._childLogicList) do
            if v then
                v:ShowSwitch(isShow)
            end
        end
    elseif not isShow and self._isShow ~= isShow then
        self:OnHide()
        for k, v in pairs(self._childLogicList) do
            if v then
                v:ShowSwitch(isShow)
            end
        end
    end
    self._isShow = isShow
end

-- 如果显示有动画的话，就重写这个方法
CUIBaseLogic.OnShow = function(self)
    self:SetActive(true)
end

-- 如果显示有动画的话，就重写这个方法
CUIBaseLogic.OnHide = function(self)
    self:SetActive(false)
end

CUIBaseLogic.Destroy = function(self)
    if self._id then
        self:OnDestroy()
        self._id = nil
        GameObject.Destroy(self._view._root)
    end
end

-- 连锁的Destroy方法
CUIBaseLogic.TraceDestroy = function(self)
    for k, v in pairs(self._childLogicList) do
        if v then
            v:Destroy()
        end
    end
    -- 因为切换场景的时候，会自动把上一个场景的Asset全部Clear，如果这里就不需用自己去清了
    --ioo.resourceManager:ReleaseAsset(self._view._asset)
    self:Destroy()
    if self._parentLogic then
        self._parentLogic._childLogicList[self._id] = nil
    end
end

CUIBaseLogic.BindUIEvent = function(self)
end

CUIBaseLogic.GetParentLogic = function(self)
    return self._parentLogic
end
-- CUIBaseLogic.Open = function(self)
--     self:OnOpen()
-- end

-- CUIBaseLogic.OnOpen = function(self)

-- end