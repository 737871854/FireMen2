local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 

CMovieScene = class(SceneBase)

CMovieScene.Init = function(self)
	SceneBase.Init(self)
end


CMovieScene.OnLevelWasLoaded = function(self)
end

CMovieScene.OnUpdate = function(self)
end

CMovieScene.OnLateUpdate = function(self)
end

CMovieScene.OnFixedUpdate = function(self)
end


CMovieScene.OnApplicationQuit = function(self)
end

CMovieScene.OnDestroy = function(self)
end