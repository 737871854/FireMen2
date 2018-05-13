CControllerManager = class()

CControllerManager.Init = function(self)
	self._controller = nil
end

CControllerManager.SwitchController = function(self, controller)
	if self._controller ~= nil then
		self._controller:End()
	end
	self._controller = controller
	self._controller:Start()
end

CControllerManager.ClearAll = function(self)
	if self._controller ~= nil then
		self._controller:End()
	end
	self._controller = nil
end

--以下代码在start.lua中被调用（其实是在c#代码中调过来的

CControllerManager.OnFingerDown = function(self, vec2, go)
	if self._controller ~= nil then
		self._controller:OnFingerDown(vec2, go)
	end
end

CControllerManager.OnFingerHover = function(self, vec2, go, phase)
	if self._controller ~= nil then
		self._controller:OnFingerHover(vec2, vec2, go, phase)
	end
end

CControllerManager.OnFingerMove = function(self, vec2, go, phase)
	if self._controller ~= nil then
		self._controller:OnFingerMove(vec2, go, phase)
	end
end

CControllerManager.OnFingerStationary = function(self, vec2, go, phase)
	if self._controller ~= nil then
		self._controller:OnFingerStationary(vec2, go, phase)
	end
end

CControllerManager.OnFingerUp = function(self, vec2, go)
	if self._controller ~= nil then
		self._controller:OnFingerUp(vec2, go)
	end
end

CControllerManager.OnFirstFingerDrag = function(self, vec2, go, deltaMove, phase)
	if self._controller ~= nil then
		self._controller:OnFirstFingerDrag(vec2, go, deltaMove, phase)
	end
end

CControllerManager.OnLongPress = function(self, vec2, go)
	if self._controller ~= nil then
		self._controller:OnLongPress(vec2, go)
	end
end

CControllerManager.OnTap = function(self, vec2, go)
	if self._controller ~= nil then
		self._controller:OnTap(vec2, go)
	end
end

CControllerManager.OnSwipe = function(self, vec2, go, startGO, velocity, move)
	if self._controller ~= nil then
		self._controller:OnSwipe(vec2, go, startGO, velocity, move)
	end
end

CControllerManager.OnPinch = function(self, vec2, go, delta, gap, phase)
	if self._controller ~= nil then
		self._controller:OnPinch(vec2, go, delta, gap, phase)
	end
end

CControllerManager.OnTwist = function(self, vec2, go, deltaRotation, totalRotation)
	if self._controller ~= nil then
		self._controller:OnTwist(vec2, go, deltaRotation, totalRotation)
	end
end

--以下与game.lua同时被调用
CControllerManager.Update = function(self)
	if self._controller ~= nil then
		self._controller:Update()
	end
end

CControllerManager.LateUpdate = function(self)
	if self._controller ~= nil then
		self._controller:LateUpdate()
	end
end


