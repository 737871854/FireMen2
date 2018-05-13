CTool = class()
-- 传入GameObject,返回child GameObject
CTool.GetChildRecursive = function(targetObj, name)
    if targetObj.name == name then
        return targetObj
    else
        local childCount = targetObj.transform.childCount
        for i = 0, childCount - 1 do
                local go = CTool.GetChildRecursive(targetObj.transform:GetChild(i), name)
                if go ~= nil then
                    return go.gameObject
                end 
        end
        return nil
    end
end

CTool.InstantiateToParent = function(prefabPath, parentTran)
    local asset = ioo.resourceManager:LoadAsset(prefabPath, GameObject.GetClassType())
    local prefab = asset:GetAsset()
    local obj = GameObject.Instantiate(prefab)
    obj.transform:SetParent(parentTran, false)
    return obj, asset
end

CTool.ReleaseAsset = function(asset)
    ioo.resourceManager:ReleaseAsset(asset)
end


CTool.Table2Json = function(t)
	return json.encode(t)
end

CTool.Json2Table = function(str)
	return json.decode(str)
end


CTool.GetXZProj = function(vec3)
    return Vector3.New(vec3.x, 0, vec3.z)
end

CTool.GetCloneObj = function(clone)
    local obj = GameObject.Instantiate(clone)
    obj.transform.localPosition = clone.transform.localPosition
    obj.transform.localScale = clone.transform.localScale
    local parent = clone.transform.parent
    if parent then
        obj.transform:SetParent(parent, false)
    end
    return obj
end

CTool.Sec2String = function(iTime)
    local cRet = ""; 
 
    local iSec  = math.mod(iTime, 60); iTime = math.floor(iTime / 60); 
    local iMin  = math.mod(iTime, 60); iTime = math.floor(iTime / 60); 
    local iHour = math.mod(iTime, 24); iTime = math.floor(iTime / 24); 
    local iDay  = iTime;

    if iDay ~= 0 then  cRet = cRet..iDay..TEXT("天 ") end 
    if iHour ~= 0 then cRet = cRet..iHour..TEXT('时') end 
    if iMin ~= 0 then cRet = cRet..iMin..TEXT('分') end 
    return cRet..iSec..TEXT('秒');
end

CTool.DiffToCurTime = function(iTime)
    local difftime = math.floor(os.difftime(os.time(), iTime) / 86400)
    local cTime = ""
    if difftime <= 0 then
        difftime = 1 --策划要求最少显示1天 lorry 2015-11-26
        cTime = cTime .. difftime .. TEXT("天")
        return cTime
    end
    local iDay = math.mod(difftime, 30); difftime = math.floor(difftime / 30);
    local iMon = math.mod(difftime, 12); difftime = math.floor(difftime / 12);
    local iYear = difftime

    if iYear ~= 0 then
        cTime = cTime .. iYear .. TEXT("年")
        if iMon ~= 0 then
            cTime = cTime .. iMon .. TEXT("个月")
        end
    else
        if iMon ~= 0 then
            cTime = cTime .. iMon .. TEXT("个月")
            if iDay ~= 0 then
                cTime = cTime .. iDay .. TEXT("天")
            end
        else
            cTime = cTime .. iDay .. TEXT("天")
        end
    end
    return cTime
end

CTool.GetVersion = function()
	return "aaa"
end

CTool.ChangeLayersRecursively = function(transform, layer)
    -- transform.layer = LayerMask.NameToLayer("UI")
    transform.gameObject.layer = layer
    if transform.childCount > 0 then
        for i = 0, transform.childCount-1 do
            local childTrans = transform:GetChild(i)
            CTool.ChangeLayersRecursively(childTrans, layer)
        end
    else
        return
    end
end

CTool.UIButtonGray = function(image, flag)
	if flag then
		local kGrayMatPath = "Assets/Common/Materials/UI/EffectUIGray.mat"
		local grayAsset = ioo.resourceManager:LoadAsset(kGrayMatPath, Material.GetClassType()):GetAsset()
		local grayMaterial = GameObject.Instantiate(grayAsset)
		image.material = grayMaterial
	else
		image.material = nil
	end
end

CTool.SwtichGrayedEffect = function(node, isShow)
	local kGrayMatPath = "Assets/Common/Materials/UI/EffectUIGray.mat"
	local grayAsset = ioo.resourceManager:LoadAsset(kGrayMatPath, Material.GetClassType()):GetAsset()
	assert(grayAsset ~= nil)
    local imageComs = node.gameObject:GetComponentsInChildren(UnityEngine.UI.Image.GetClassType(), true)
	for i = 0, imageComs.Length - 1 do
        local maskCom = imageComs[i]:GetComponent("Mask")
        if not maskCom then
            if isShow then
		        imageComs[i].material = grayAsset
            else
                imageComs[i].material = nil
            end
        end
	end
    --因为gray对outline的text颜色处理有点问题，所以添加判断，剔除了对此种txt的处理 lorry
    local textComs = node.gameObject:GetComponentsInChildren(UnityEngine.UI.Text.GetClassType(), true)
    for i = 0, textComs.Length - 1 do
        local maskCom = textComs[i]:GetComponent("Mask")
        if not maskCom then
            if isShow then
                local outLine = textComs[i]:GetComponent("Outline")
                if not outLine then
                    textComs[i].material = grayAsset
                end
            else
                textComs[i].material = nil
            end
        end
	end
    
end
