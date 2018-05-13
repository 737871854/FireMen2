--======================================================================
--（c）copyright 2015 175game.com All Rights Reserved
--======================================================================
-- filename: data_manager.lua
-- author: lxt  created: 2015/10/29
-- descrip: 服务器下发的用户数据处理中心，公用数据处理模块全部添加到此处
--======================================================================
-- local CPlayerData = Import("logic/data/player_data").CPlayerData
-- local CItemData = Import("logic/data/item_data").CItemData
-- local CTaskData = Import("logic/data/task_data").CTaskData

--Import各个模块的数据接口--BEGIN
local CLoginData = Import("logic/data/login_data").CLoginData
local CSettingData = Import("logic/data/setting_data").CSettingData
--Import各个模块的数据接口--END

CDataManager = class()

CDataManager.Init = function(self)
    -- 存放所有数据模块
    self._dataDic = {}
	
	--self:GetSettingData()
end

CDataManager.ClearAllData = function(self)
	 
	for key, value in pairs(self._dataDic) do
		value:ClearALL()
	end
	 
    self._dataDic = {}
end

--获取登陆数据模块
CDataManager.GetLoginData = function(self)    
    if not self._dataDic["login"] then
        self._dataDic["login"] = CLoginData:New()
    end
    return self._dataDic["login"]
end


--获取后台数据模块
CDataManager.GetSettingData = function(self)
    if not self._dataDic["setting"] then
        self._dataDic["setting"] = CSettingData:New()
    end
    return self._dataDic["setting"]
end
