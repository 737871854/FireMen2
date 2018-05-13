--======================================================================
--（c）copyright 2015 175game.com All Rights Reserved
--======================================================================
-- filename: global_enum.lua
-- author: lxt  created: 2015/10/29
-- descrip: 需要在不同模块使用的公共枚举变量存储文件
--======================================================================

local ToStr = function (value)
    if (type(value) == "string") then return "\"" .. value .. "\"" end
    return tostring(value)
end

local SetErrorIndex = function (t)
    setmetatable(t, {
        __index = function (t, k)
            error("cant index not exist key " .. tostring(t) .. "[" .. ToStr(k) .. "]" .. "\n" .. debug.traceback())
        end,
        __newindex = function (t, k, v)
            error("cant newindex not exist key " .. tostring(t) .. "[" .. ToStr(k) .. "]" .. "\n" .. debug.traceback())
        end,
    })
end


-- 玩家属性key
PlayerAttrKey = {
    ["cName"] = "cName",
    ["iGrade"] = "iGrade",
    ["iExp"] = "iExp",
    ["iDiamond"] = "iDiamond",
    ["iGold"] = "iGold",
    ["tCup"] = "tCup",
    ["iMp"] = "iMp",
    ["cGoldItem"] = "g0001",
}
SetErrorIndex(PlayerAttrKey)

--频繁打开的UI的ID
UICustomID = {
    FirstTest = "FirstTest",
    Login = "Login",
}
SetErrorIndex(UICustomID)

LoginState = {
    kUninit = 1,
    kDownloadServerConfig = 2,
    kWaitForSelectServer = 3,
    kWaitForConnectServer = 4,
    kTryReconnect = 5,
    kWaitForInputUserInfo = 6,
    kWaitForCheckUserInfo = 7,
    kWaitForEnterScene = 8,
    kEnterScene = 9,
	kCheckVersion = 10,
    kFinish = 10000,
    kError = 100001,
}
SetErrorIndex(LoginState)



__init__ = function (mod)
    loadglobally(mod)
end