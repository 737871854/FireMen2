local CUIBaseLogic 	= Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUICoinView 	= Import("logic/ui/pnl_coin/ui_pnl_coin_view").CUICoinView
local EventDefine 	= Import("logic/base/event_define").Define


CUICoinLogic = class(CUIBaseLogic)

--构造函数，初始化View，绑定事件等
CUICoinLogic.Init = function(self, id)
    self._prefabPath = "prefabs\\ui\\panel_coin"
    self._view = CUICoinView:New(self._prefabPath)

	self._coinNum = 0				 -- 币数
	
	self._hasStart = false			 -- 是否进入游戏
	
	self._isPlaying = false			 -- 正在进行播放待机视频

	self._idleTime = 30			 -- 无操作进入待机视频时间	
	
    CUIBaseLogic.Init(self, id)	
end

--绑定UI事件监听
CUICoinLogic.BindUIEvent = function(self)
    --编写格式如下:
		 
	--注册 IO消息
	IOLuaHelper.Instance:RegesterListener(2, LuaHelper.OnIOEventHandle(function(id, flag) self:HandlerEnterSetting(id, flag) end), "pnl_coin_setting")
	
	--注册逻辑交互消息
	ioo.RegesterListener("Coin_Event_End_Idle_Movie", LuaHelper.EventHandle(function() self:EventMovieEnd()  end), "pnl_coin_start")
	
end

CUICoinLogic.OnCreate = function(self)
	--注册后台数据更改消息
	RegTrigger(EventDefine.COIN_DATA_COIN_CHANGE, function(id) self:RefreshCoinNum(id) end, "pnl_coin")
	RegTrigger(EventDefine.COIN_DATA_PLAYER_SURE, function() self:PlayerUseCoin() end, "pnl_coin")

    --后台数据
	self._settingData 			= gGame:GetDataMgr():GetSettingData()	
	
	--打开界面更新界面数据
	for i = 0, 2 do
		self:RefreshCoinNum(i)
	end
	
	-- 待机倒计时
	self:UpdateIdleTime()
end

CUICoinLogic.OnDestroy = function(self)  
  --移除后台数据更改消息
  UnRegTrigger(EventDefine.COIN_DATA_COIN_CHANGE, "pnl_coin")
  UnRegTrigger(EventDefine.COIN_DATA_PLAYER_SURE, "pnl_coin")
  
  --移除 IO消息
  IOLuaHelper.Instance:RemoveListener(2, "pnl_coin_setting")
  
  --移除逻辑监听消息
  ioo.RemoveListener("Coin_Event_End_Idle_Movie", "pnl_coin_start")
    
  self._view:Destroy() 
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 被注册的方法 (Lua-Lua) Begine--------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--CUICoinLogic.OnButtonMoveEnd = function(self)
--	print("MoveEnd")
--end


-- 刷新币数据
CUICoinLogic.RefreshCoinNum = function(self,id)
	if self._isPlaying == true then
		self:EndIdleMovie()	
		self:UpdateIdleTime()
	end
	
	self._idleTime = 30
	
	self._view._coinNumbers[id].text = self._settingData._hasCoin[id].."/"..self._settingData._rate
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 被注册的逻辑方法(C#-Lua) Begine--------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 结束视频播放
CUICoinLogic.EventMovieEnd = function(self)
	self:EndIdleMovie()
	
	self:UpdateIdleTime()
end


-- 切换到后台按钮
CUICoinLogic.HandlerEnterSetting = function(self, id, flag)
	self:EndIdleMovie()
	
	--print("----------------------------------Regester Coin Time Up")
	gGame:GetTimeMgr():RemoveTimer(self._timerId)
	
	gGame:ChangeSceneDirect("setting","settingScene")
end

-- 玩家拍确认按钮
CUICoinLogic.PlayerUseCoin = function(self,id)
	if self._isPlaying == true then
		self:EndIdleMovie()	
	end
	
	if not self._hasStart then
		self._hasStart = true
		--print("----------------------------------Regester Coin Time Up")
		gGame:GetTimeMgr():RemoveTimer(self._timerId)
		coroutine.start(ToSelect)
	end
	
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
----------------------------------------------------------------------------------------- 内部使用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 进入地图选择
function ToSelect()
	coroutine.wait(2)

	gGame:ChangeScene("select", "selectScene")
end

-- 无人操作倒计时
CUICoinLogic.UpdateIdleTime = function(self)
	--print("++++++++++++++++++++++++++++Regester Coin Time Up")
	self._idleTime = 30
	self._timerId = gGame:GetTimeMgr():AddTimer(1, function() 
		self._idleTime = self._idleTime - 1		
		--print(self._idleTime)
		if self._idleTime <= -1 then
			self:PlayIdleMovie()
			--print("----------------------------------Regester Coin Time Up")
			gGame:GetTimeMgr():RemoveTimer(self._timerId)
		end			
	end, true)
end

-- 进入待机视频
CUICoinLogic.PlayIdleMovie = function(self)
	self._isPlaying = true	
	ioo.audioManager:StopBackMusic("music_standby_0"..gGame:GetSceneMgr():GetCurrentScene()._index)	
	ioo.TriggerListener("Coin_Event_Play_Idle_Movie")
end

-- 结束待机视频播放
CUICoinLogic.EndIdleMovie = function(self)
	if self._isPlaying == false then
		return
	end
	
	self._isPlaying = false
	ioo.audioManager:PlayBackMusic("music_standby_0"..gGame:GetSceneMgr():GetCurrentScene()._index, true)
	ioo.TriggerListener("Coin_Event_Stop_Idle_Movie")
end
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 场景调用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
