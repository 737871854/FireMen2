local CUIBaseLogic = Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUISelectView = Import("logic/ui/pnl_select/ui_pnl_select_view").CUISelectView
local EventDefine = Import("logic/base/event_define").Define

CUISelectLogic = class(CUIBaseLogic)

-- 构造函数，初始化View，绑定函数
CUISelectLogic.Init = function(self, id)
	self._prefabPath = "prefabs\\ui\\panel_select"
	self._view = CUISelectView:New(self._prefabPath)
	
	self._selectID = 0	
	
	CUIBaseLogic.Init(self, id)
	
	self._initilized = true
end

-- 绑定UI事件监听
CUISelectLogic.BindUIEvent = function(self)
	--注册 IO消息
		
	--注册逻辑消息	
	ioo.RegesterListener("Character_Is_Been_Spray", LuaHelper.EventHandle(function(data) self:CharacterBeenSpray(data) end), "pnl_select_character")
	ioo.RegesterListener("Character_Select_End", LuaHelper.EventHandle(function() self:CharacterSelectEnd() end), "pnl_select_character_end")
	ioo.RegesterListener("Map_Is_Been_Spray", LuaHelper.EventHandle(function(data) self:MapBeenSpray(data) end), "pnl_select_map")
end

-- 创建
CUISelectLogic.OnCreate = function(self)
	--注册后台数据更改消息
	--RegTrigger(EventDefine.COIN_DATA_COIN_CHANGE, function(id) self:RefreshCoinNum(id) end, "pnl_select")
	RegTrigger(EventDefine.COIN_DATA_PLAYER_SURE, function() self:PlayerUseCoin() end, "pnl_select")

	self:UpdateTime(0)
end

-- 销毁
CUISelectLogic.OnDestroy = function(self)
	--移除后台数据更改消息
	UnRegTrigger(EventDefine.COIN_DATA_PLAYER_SURE, "pnl_select")

	-- 移除 IO消息
		
	-- 移除逻辑消息	
	ioo.RemoveListener("Character_Is_Been_Spray", "pnl_select_character")
	ioo.RemoveListener("Character_Select_End", "pnl_select_character_end")
	ioo.RemoveListener("Map_Is_Been_Spray", "pnl_select_map")
	
	self._view:Destroy() 
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 被注册的方法(Lua-Lua) Begine--------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 玩家拍确认按钮
CUISelectLogic.PlayerUseCoin = function(select,id)
	
	--local sequence = DG.Tweening.DOTween.Sequence()
	--sequence:Append(self._view._btnStart.transform:DOLocalMove(Vector3.New(0,0,0),2,false))
	--sequence:Append(self._view._btnAdd.transform:DOLocalMove(Vector3.New(0,0,0),2,false))
	--sequence:SetLoops(1, DG.Tweening.LoopType.Restart)
	--sequence:OnComplete(function() self:OnButtonMoveEnd() end)
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 被注册的方法(C#-Lua) Begine--------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 角色正被选择
CUISelectLogic.CharacterBeenSpray = function(self, data)
	if data[1] == 0 or data[1] >= 1 then
		self._view._progress_c[data[0]].gameObject:SetActive(false)
	else
		self._view._progress_c[data[0]].gameObject:SetActive(true)
		self._view._progress_c[data[0]].fillAmount = data[1]
	end
end

-- 结束角色选择
CUISelectLogic.CharacterSelectEnd = function(self)
	for i = 0, #self._view._progress_c do
		self._view._progress_c[i].gameObject:SetActive(false)
	end
	self._view._map_title.gameObject:SetActive(true)
	gGame:GetTimeMgr():RemoveTimer(self._timerId)
	self:UpdateTime(1)	
end

-- 地图正被选择
CUISelectLogic.MapBeenSpray = function(self, data)
	if data[1] == 0 or data[1] >= 1 then
		self._view._progress_m[data[0]].gameObject:SetActive(false)		
	else
		self._view._progress_m[data[0]].gameObject:SetActive(true)
		self._view._progress_m[data[0]].fillAmount = data[1]
	end
end

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 内部使用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 选择倒计时
CUISelectLogic.UpdateTime = function(self, flag)
	self._idleTime = 20
	self._view._timeUp.text = self._idleTime
	self._timerId = gGame:GetTimeMgr():AddTimer(1, function() 
		self._idleTime = self._idleTime - 1
		self._view._timeUp.text = self._idleTime
		if self._idleTime <= 0 then
			if flag == 0 then
				ioo.TriggerListener("N0_Character_Is_Selected")
			else
				ioo.TriggerListener("No_Map_Is_Selected")
			end		
		end
	end, true)
end


--[[
-- 被选中的地图
CUISelectLogic.OnSelect = function(self)
	for i = 0, #self._view._maps do
		if i ~= self._selectID then
			self._view._maps[i].material = self._grey
			local sequence = DG.Tweening.DOTween.Sequence()
			sequence:Append(self._view._maps[i].transform:DOScale(Vector3.New(1,1,1) * 0.5, 0.1))
			sequence:SetLoops(1, DG.Tweening.LoopType.Restart)
		else
			self._view._maps[i].material = self._normal
			local sequence = DG.Tweening.DOTween.Sequence()
			sequence:Append(self._view._maps[i].transform:DOScale(Vector3.New(1,1,1) * 0.6, 0.1))
			sequence:SetLoops(1, DG.Tweening.LoopType.Restart)
			--sequence:OnComplete(function() self:OnButtonMoveEnd() end)
		end
	end	
end
]]

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 场景调用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 玩家水标位置更新
CUISelectLogic.FixedUpdatePos = function(self, id, pos)
	if not self._view._water[id].gameObject.activeSelf then
		self._view._water[id].gameObject:SetActive(true)
	end	
	
	self._view._water[id].transform.position = pos
	
	--local layer = 2^LayerMask.NameToLayer('Default')
	--local _ray = Camera.main:ScreenPointToRay(data[1])
	--local _flag, _hit = Physics.Raycast(_ray, nil)
end

-- 移除倒计时
CUISelectLogic.EndTimeUp = function(self)
	gGame:GetTimeMgr():RemoveTimer(self._timerId)
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
