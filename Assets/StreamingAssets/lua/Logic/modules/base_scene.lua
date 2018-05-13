local Tool = Import("logic/common/tool").CTool

CSceneBase = class()

CSceneBase.Init = function(self)
end

--设置预加载资源的函数，各个子类都要重写此函数，以便在Loading界面进行资源预加载
CSceneBase.SetPreLoad = function(self, level)
    -- LoadSceneMgr.Instance:AddPreLoadPrefab("Assets/Prefabs/common/b50001/b50001.prefab" ,5)
    -- LoadSceneMgr.Instance:AddPreLoadPrefab("Assets/Prefabs/common/b50003/b50003.prefab",10)
    -- LoadSceneMgr.Instance:AddPreLoadPrefab("Assets/Prefabs/common/b50501/b50501.prefab",15)
end

--场景和预加载对象全部加载完毕之后调用，用于打开界面调整摄像头,摆放主角，切换控制器
CSceneBase.OnLevelWasLoaded = function(self)
end

CSceneBase.OnUpdate = function(self)
end

CSceneBase.OnLateUpdate = function(self)
end

CSceneBase.OnFixedUpdate = function(self)
end


CSceneBase.OnApplicationQuit = function(self)
end

CSceneBase.OnDestroy = function(self)
end

