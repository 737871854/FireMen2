local CUIBaseLogic = Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUIBattleView = Import("logic/ui/pnl_battle/ui_pnl_battle_view").CUIBattleView
local EventDefine = Import("logic/base/event_define").Define

UIPnlBattleLogic = class(CUIBaseLogic)

-- 构造函数，初始化View，绑定函数
UIPnlBattleLogic.Init = function(self, id)
	self._prefabPath = "prefabs\\ui\\panel_battle"
	self._view = CUIBattleView:New(self._prefabPath)
	
	-- 射击点信息
	self._hpInfo = {}
	-- 碎屏信息
	self._crackInfo = {}
	-- 碎片资源
	self._crackCache = {}
	
	CUIBaseLogic.Init(self, id)
	
	self._initilized = true
end

-- 绑定UI事件监听
UIPnlBattleLogic.BindUIEvent = function(self)
	--注册 IO消息
	
	
	--注册逻辑消息	
	
end

-- 创建
UIPnlBattleLogic.OnCreate = function(self)
	--注册后台数据更改消息
	RegTrigger(EventDefine.COIN_DATA_COIN_CHANGE, function(id) self:RefreshCoinNum(id) end, "pnl_battle")
	RegTrigger(EventDefine.COIN_DATA_PLAYER_SURE, function() self:PlayerUseCoin() end, "pnl_battle")
		
	--后台数据
	self._settingData 			= gGame:GetDataMgr():GetSettingData()	
	
	--打开界面更新界面数据
	for i = 0, 2 do
		self:RefreshCoinNum(i)
	end
end

-- 销毁
UIPnlBattleLogic.OnDestroy = function(self)
	--移除后台数据更改消息
	UnRegTrigger(EventDefine.COIN_DATA_COIN_CHANGE, "pnl_battle")
	UnRegTrigger(EventDefine.COIN_DATA_PLAYER_SURE, "pnl_battle")
	
	-- 移除 IO消息
	
	
	-- 移除逻辑消息	
	
	self._hpInfo = nil
	
	self._view:Destroy() 
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 被注册的方法 (Lua-Lua) Begine--------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 确认操作
UIPnlBattleLogic.HandleSure = function(self)
	gGame:ChangeScene("battle0", "battleScene0")
end

-- 刷新币数据
UIPnlBattleLogic.RefreshCoinNum = function(self,id)
	self._view._head[id].coin.text = self._settingData._hasCoin[id].."/"..self._settingData._rate
end

-- 玩家拍确认按钮
UIPnlBattleLogic.PlayerUseCoin = function(self,id)		
	
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
----------------------------------------------------------------------------------------- 被注册的方法(C#-Lua)Begine--------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- TODO:
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 内部使用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 场景调用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- 玩家水标位置更新
UIPnlBattleLogic.FixedUpdatePos = function(self, id, pos)
	if not self._view._water[id].gameObject.activeSelf then
		self._view._water[id].gameObject:SetActive(true)
	end	
	
	self._view._water[id].transform.position = pos
	
	--local layer = 2^LayerMask.NameToLayer('Default')
	--local _ray = Camera.main:ScreenPointToRay(data[1])
	--local _flag, _hit = Physics.Raycast(_ray, nil)
end

-- 刷新头像信息 参数：id：角色ID progress：进度条 score：得分
UIPnlBattleLogic.RefreshHeadInfo = function(self, id, progress, score)	
	self._view._head[id].play.gameObject:SetActive(true)
	self._view._head[id].dead.gameObject:SetActive(false)	
	self._view._head[id].score.gameObject:SetActive(true)
	self._view._head[id].continue_image.gameObject:SetActive(false)
	self._view._head[id].continue_time.gameObject:SetActive(false)
	self._view._head[id].coin.gameObject:SetActive(false)
	self._view._head[id].coin_title.gameObject:SetActive(false)
	
	self._view._head[id].progress_forward.fillAmount = progress
	self._view._head[id].score.text = score
end

-- 玩家续币倒计时
UIPnlBattleLogic.OnPlayerContinue = function(self, id, timer)
	--self._view._head[id].score.gameObject:SetActive(false)
	self._view._head[id].coin_title.gameObject:SetActive(true)
	self._view._head[id].coin.gameObject:SetActive(true)
	self._view._head[id].continue_image.gameObject:SetActive(true)
	self._view._head[id].continue_time.gameObject:SetActive(true)
	
	self._view._head[id].progress_forward.fillAmount = 0
	self._view._head[id].continue_time.text = timer
end

-- 玩家死亡
UIPnlBattleLogic.OnPlayerDead = function(self, id)
	self._view._head[id].play.gameObject:SetActive(false)
	self._view._head[id].dead.gameObject:SetActive(true)
	self._view._head[id].score.gameObject:SetActive(false)
	self._view._head[id].continue_image.gameObject:SetActive(false)
	self._view._head[id].continue_time.gameObject:SetActive(false)
end

-- 更新劫持玩家进度
UIPnlBattleLogic.UpdateHoldUI = function(self, value)
	self._view._holdProgress.fillAmount = value
end

-- 显示劫持进度
UIPnlBattleLogic.ActiveHoldUI = function(self)
	self._view._holdProgress.gameObject:SetActive(true)
end

-- 隐藏劫持进度
UIPnlBattleLogic.DisActiveHoldUI = function(self)
	self._view._holdProgress.fillAmount = 0
	self._view._holdProgress.gameObject:SetActive(false)
end

-- 显示Boss血条
UIPnlBattleLogic.BossBorn = function(self, id)
	--self._view._boss_head.head.sprite = 依据id设置Boss图像
	self._view._boss_head.obj:SetActive(true)
end

-- 隐藏Boss血条
UIPnlBattleLogic.BossDead = function(self)
	self._view._boss_head.obj:SetActive(false)
end

-- 更新Boss血条
UIPnlBattleLogic.UpdateBossHealth = function(self, value)
	if value > 0.6666 then
		self._view._boss_head.num.text = 3
		self._view._boss_head.progress0.fillAmount = 3 * (value - 0.6666)
		self._view._boss_head.progress1.fillAmount = 1
		self._view._boss_head.progress2.fillAmount = 1
	end
	
	if value <= 0.6666 and value > 0.3333 then
		self._view._boss_head.num.text = 2
		self._view._boss_head.progress0.fillAmount = 0
		self._view._boss_head.progress1.fillAmount = 3 * (value - 0.3333)
	end
	
	if value <= 0.3333 then
		self._view._boss_head.num.text = 1
		self._view._boss_head.progress1.fillAmount = 0
		self._view._boss_head.progress2.fillAmount = 3 * value
	end
end

-- 更新射击点信息
UIPnlBattleLogic.UpdateHittingPart = function(self, index, hp)	
	if #self._hpInfo < index + 1 then
		local ret = GameObject.Instantiate(self._view._hit_target[1].gameObject):GetComponent("RectTransform")
		ret.transform:SetParent(self._view._hit_target.obj.transform, false)		
		local image = ret.gameObject:GetComponent("Image")
		local hitInfo = {}
		hitInfo._ret = ret
		hitInfo._image = image
		self._hpInfo[index + 1] = hitInfo	
		local sequence = DG.Tweening.DOTween.Sequence()
		sequence:Append(ret.gameObject.transform:DOLocalRotate(Vector3.New(0,0,13), 0.1,DG.Tweening.RotateMode.Fast))		
		sequence:SetLoops(-1,DG.Tweening.LoopType.Incremental)								   
	end
		
	if #self._hpInfo == 0 then
		return
	end	
	
	local info = self._hpInfo[index + 1]		
	info._image.transform.localScale = Vector3.New(1,1,1) * hp.scale
	
	local percent = hp.curHp / hp.maxHp
	if percent == 0 then
		info._ret.gameObject:SetActive(false)
		return
	else
		info._ret.position = hp.uiPos
		info._ret.gameObject:SetActive(true)
	end
		
	if percent >= 0.67 then
		info._image.sprite = self._view._hit_target[1].sprite
		else if percent < 0.67 and percent > 0.34 then
			info._image.sprite = self._view._hit_target[2].sprite
			else if percent <= 0.34 and percent > 0 then
				info._image.sprite = self._view._hit_target[3].sprite	
			end
		end
	end
end

-- 清除射击点
UIPnlBattleLogic.ClearHittingPart = function(self)
	for i = 1, #self._hpInfo do
		local info = self._hpInfo[i]
		GameObject.Destroy(info._ret.gameObject)		
	end
	self._hpInfo = {}
end

-- 实时更新
UIPnlBattleLogic.UpdateScreenCrack = function(self)
	for i = 1, #self._crackInfo do
		local info = self._crackInfo[i]
		if info.time > 0 then
			info.time = info.time - 0.02	
		end
	end
	
	while(#self._crackInfo > 0)
	do
		local last = #self._crackInfo
		info = self._crackInfo[last]
		if info.time <= 0 then		
			self:PushScreenCrack(info.index, info.obj)
			table.remove(self._crackInfo, last)
		else
			break
		end
	end
end

-- 屏幕破碎
UIPnlBattleLogic.AddScreenCrack = function(self, data)
	local index = math.random(1,3)
	local crack = self:GetScreenCrack(index)	
	if crack == nil then
		crack = GameObject.Instantiate(self._view._screen_crack[index].gameObject):GetComponent("RectTransform")
	end	
	crack.transform:SetParent(self._view._screen_crack.obj.transform, false)	
	crack.transform.localScale = Vector3.New(1,1,1)
	crack.gameObject:SetActive(true)
	crack.transform.position = data
	
	local info = {}
	info.time = 1
	info.index = index
	info.obj = crack.gameObject
	
	local cout = #self._crackInfo
	cout = cout + 1
	self._crackInfo[cout] = info	
end

-- 获取指定碎屏资源
UIPnlBattleLogic.GetScreenCrack = function(self, index)
	if #self._crackCache == 0 then
		return nil
	end
	
	if self._crackCache[index] == nil or #self._crackCache[index] == 0 then
		return nil
	end
	
	local cout = #self._crackCache[index]
	local crack = self._crackCache[index][cout]
	table.remove(self._crackCache[index], cout)
	return crack
end

-- 回收碎屏资源
UIPnlBattleLogic.PushScreenCrack = function(self, index, crack)
	if self._crackCache[index] == nil then
		self._crackCache[index] = {}
	end
	
	local cout = #self._crackCache[index]
	cout = cout + 1
	crack:SetActive(false)
	self._crackCache[index][cout] = crack
end
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
