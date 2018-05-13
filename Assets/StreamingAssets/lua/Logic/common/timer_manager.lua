--======================================================================
--（c）copyright 2015 175game.com All Rights Reserved
--======================================================================
-- filename: global_enum.lua
-- author: lxt  created: 2015/12/22
-- descrip: 定时器管理类，用于开启与执行所有定时器，整个app应该只有一个
--======================================================================
--
local Timer = Import("logic/common/timer")

CTimerManager = class()

CTimerManager.Init = function(self)
	--用于管理定时器的自增id
	self._auto_id = 0
	--定时器列表
	self._timers = {}
	
	self._fixedDeltaTime = nil
end

--添加一个定时器
CTimerManager.AddTimer = function(self, detalTime, func, immediately)
	self._auto_id = self._auto_id + 1
	local timer = Timer.CTimer:New(detalTime, func, self._auto_id, immediately)
	self._timers[timer._id] = timer
	
	return timer._id
end

--根据ID移除一个定时器
CTimerManager.RemoveTimer = function(self, id)
	if self._timers[id] then
		self._timers[id] = nil
	end
end

--清空定时器
CTimerManager.RemoveAll = function(self)
	self._timers = {}
end

--添加一个自动释放的定时器，只执行一次主体，其实就是自动加上"release"的返回
CTimerManager.SetTimeOut = function(self, detalTime, func)
	local call_back = function()
		func()
		return "release"
	end
	
	return self:AddTimer(detalTime, call_back)
end

--根据ID获取定时器
CTimerManager.GetTimer = function(self, id)
	return self._timers[id]
end

--将某个定时器重启
CTimerManager.Restart = function(self, id)
	local timer = self._timers[id]
	if not timer then
		print("[error:] 不存在这个定时器：", id)
	end
	timer:Restart()
end

CTimerManager.SetDetalTime = function(self, id, detalTime)
	local timer = self._timers[id]
	if not timer then
		print("[error:] 不存在这个定时器：", id)
	end
	timer:SetDetalTime(detalTime)
end

--更新所有定时器
CTimerManager.Update = function(self)
	if not self._fixedDeltaTime or self._fixedDeltaTime <= 0 then
		self._fixedDeltaTime = Time.fixedDeltaTime
	end
	for k, v in pairs(self._timers) do
		local res = v:Call(self._fixedDeltaTime)
		if res == "release" then
			self._timers[k] = nil
		end
	end
end