EventSource = class()

EventSource.id = 0

EventSource.Init = function(self)
	-- 存放所有回调函数的弱表
	self._listener_list = new_weak_table("v")

	-- 按顺序记录所有回调函数id
	self._callback_list = {}
end

EventSource.AddListener = function(self, event_name, handle_func)
	self._callback_list[event_name] = self._callback_list[event_name] or {}

	EventSource.id = EventSource.id + 1
	self._listener_list[EventSource.id] = handle_func

	table.insert(self._callback_list[event_name], EventSource.id)

	return handle_func
end

EventSource.ClearListener = function(self, event_name)
	if not event_name then
		self._united_callback = nil
		return
	end
	
	if not self._callback_list[event_name] then return end
	for i = #self._callback_list[event_name], 1, -1 do
		local id = self._callback_list[event_name][i]
		self._listener_list[id] = nil
	end
end

EventSource.ClearAllListener = function(self)
	self._united_callback = nil
	
	local event_list = {}

	for event_name, _ in pairs(self._callback_list) do
		table.insert(event_list, event_name)
	end

	for _, event_name in ipairs(event_list) do
		self:ClearListener(event_name)
	end
	
	self._listener_list = new_weak_table("v")
	self._callback_list = {}
end

EventSource.SetListener = function(self, event_name, handle_func)
	if not event_name then
		self._united_callback = handle_func
		return
	end
	
	self:ClearListener(event_name)
	return self:AddListener(event_name, handle_func)
end

EventSource.DelListener = function(self, event_name, handle_func)
	if not event_name then
		self._united_callback = nil
		return
	end
	
	if not self._callback_list[event_name] then return end
	
	for i = #self._callback_list[event_name], 1, -1 do
		local id = self._callback_list[event_name][i]

		if self._listener_list[id] == handle_func then
			self._listener_list[id] = nil
			break
		end
	end
end

EventSource.test_listener = function(self, event_name)
	if not event_name then
		return (self._united_callback ~= nil)
	end
	return self._callback_list[event_name] and #self._callback_list[event_name] > 0
end

EventSource._call_func = function(self, event_name, func, ...)
	local success
	local result
	local arg = {...}
	success, result = xpcall(
		function() return func(unpack(arg)) end,
		function(e)
			local culprit = string.format("---FireEvent回调失败,事件[%s]---stack: %s", event_name, debug.traceback())
			warn(culprit .. " " ..  type(func) .. " " .. e)
		end
	)
	if result == "Release" then
		self._listener_list[id] = nil
	end
end

EventSource.FireEvent = function(self, event_name, ...)
	if not event_name then
		local func = self._united_callback
		if func then
			self:_call_func(event_name, func, ...)
		end
		return
	end
	
	if not self._callback_list[event_name] then return end

	for i = #self._callback_list[event_name], 1, -1 do
		local id = self._callback_list[event_name][i]
		
		local func = self._listener_list[id]
		if func then
			self:_call_func(event_name, func, ...)
		else
			table.remove(self._callback_list[event_name], i)
		end
	end
end

EventSource.Release = function (self)
	self:ClearAllListener()
end

-- function FireGlobalEvent(ctx, name, ...)
-- 	if not ctx.__global_EventSource__ then
-- 		ctx.__global_EventSource__ = EventSource:new()
-- 	end

-- 	ctx.__global_EventSource__:FireEvent(name, ...)
-- end

-- function DelGlobalEvent(ctx, name, handler)
-- 	if not ctx.__global_EventSource__ then return end
-- 	ctx.__global_EventSource__:DelListener(name, handler)
-- end

-- function HandleGlobalEvent(ctx, name, func)
-- 	if not ctx.__global_EventSource__ then
-- 		ctx.__global_EventSource__ = EventSource:new()
-- 	end

-- 	local handler = ctx.__global_EventSource__:AddListener(name, func)
-- 	return handler
-- end