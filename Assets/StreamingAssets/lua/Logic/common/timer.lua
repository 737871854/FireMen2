--======================================================================
--（c）copyright 2015 175game.com All Rights Reserved
--======================================================================
-- filename: timer.lua
-- author: lxt  created: 2015/12/22
-- descrip: 定时器类，不直接被调用，外部只会看到timer_manager
--======================================================================

CTimer = class()

CTimer.Init = function(self, detalTime, func, id, immediately)
	--定时器的执行频率
	self._detal_time = detalTime
	--定时器的回调函数
	self._call_back_func = func
	--定时器的ID
	self._id = id
	--定时器的下次执行的时间
	self._time_call = detalTime
	if immediately then
		self._time_call = 0
	end
	--定时器当前时间
	self._cur_time = 0
end

CTimer.Call = function(self, detalTime)
	self._cur_time = self._cur_time + detalTime
	if self._cur_time >= self._time_call then
		--计算出下次执行的时间
		self._time_call = self._time_call + self._detal_time
		return self._call_back_func()
	end
end

--重启定时器
CTimer.Restart = function(self)
	self._cur_time = 0
	self._time_call = self._detal_time
end

--设置定时器的频率
CTimer.SetDetalTime = function(self, detalTime)
	self._detal_time = detalTime
end

CTimer.GetCurTime = function(self)
	return self._cur_time
end