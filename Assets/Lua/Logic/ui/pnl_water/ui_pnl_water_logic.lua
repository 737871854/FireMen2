local CUIBaseLogic 	= Import("logic/ui/base/ui_base_logic").CUIBaseLogic
local CUIWaterView 	= Import("logic/ui/pnl_water/ui_pnl_water_view").CUIWaterView
local EventDefine 	= Import("logic/base/event_define").Define

UIPnlWaterLogic = class(CUIBaseLogic)

--构造函数，初始化View，绑定事件等
UIPnlWaterLogic.Init = function(self, id)
    self._prefabPath = "prefabs\\ui\\panel_water"
    self._view = CUIWaterView:New(self._prefabPath)
	
	self._progress = 0
	self._waitTime = 0
	self.Min_HEIGHT = -300
	self.MAX_HEIGHT = 490
	self._pos = Vector3:New(0,0,0)
	
    CUIBaseLogic.Init(self, id)	
end

--绑定UI事件监听
UIPnlWaterLogic.BindUIEvent = function(self)
    --编写格式如下:
		 
	--注册 IO消息
		
	--注册逻辑交互消息
	
end

UIPnlWaterLogic.OnCreate = function(self)
	--注册后台数据更改消息

	self._origin = self._view._fill_water.splash.transform.position
end

UIPnlWaterLogic.OnDestroy = function(self)  
  --移除后台数据更改消息
  
  --移除 IO消息

  
  --移除逻辑监听消息
 
    
  self._view:Destroy() 
end

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 被注册的方法 (Lua-Lua) Begine--------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--TODO:
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 被注册的逻辑方法(C#-Lua) Begine--------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--TODO:
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 内部使用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--TODO:
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------- 场景调用的方法 Begine------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 更新加水面板信息
UIPnlWaterLogic.UpdateWaterInfo = function(self, value0, value1)
	self._view._fill_water.timeUp.text = value0
	self._view._fill_water.progress.fillAmount = value1
	self._pos.x = self._view._fill_water.splash.rectTransform.anchoredPosition.x
	self._pos.y = value1 * self.MAX_HEIGHT + self.Min_HEIGHT
	self._pos.z = self._view._fill_water.splash.rectTransform.anchoredPosition.z
	self._view._fill_water.splash.rectTransform.anchoredPosition = self._pos
	
	if self._progress ~= value1 then		
		self._view._animator_man:SetInteger("Flag", 1)
		self._view._animator_column:SetInteger("Flag", 1)
		local timer0 = self._view._animator_man:GetCurrentAnimatorStateInfo(0).length
		local timer1 = self._view._animator_column:GetCurrentAnimatorStateInfo(0).length
		if timer0 > timer1 then
			self._waitTime = timer0
		else
			self._waitTime = timer1
		end
		self._progress = value1
	else		
		if self._waitTime ~= 0 then
			self._waitTime = self._waitTime - 0.02		
			if self._waitTime <= 0 then
				self._waitTime = 0
				self._view._animator_man:SetInteger("Flag", 2)
				self._view._animator_column:SetInteger("Flag", 0)
			end
		end	
	end	
end
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------  End	-----------------------------------------------------------------------------				
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------