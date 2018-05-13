--临时的协议配置
-- type:
-- 0:int, 1:string, 2:struct, 3:double

local ENDIAN_LITTLE = 1
local ENDIAN_BIG = 2
local ENDIAN = ENDIAN_LITTLE
local HEAD_LENGTH = 4
local ARRAY_VAR = 0x00010000
local kPtoFilePath = "logic/net/rpc_lua_table"

Crpc = class()

Crpc.Init = function(self, protocolMgr)
	self._protocolMgr = protocolMgr
	self._client_protocols = {}		--下行协议列表
	self._server_protocols = {}		--上行协议列表
	self._structs = {}				--结构信息列表
	self._output = {}				--输出的包数据
	self._input = {}				--输入的包数据
	self:LoadConfig()
end

--加载协议配置
Crpc.LoadConfig = function(self, str)
	if str then
		local truck = loadstring(str)
		if type(truck) == "function" then
			self._rpc_data = truck()
		else
			print("update rpc error!", debug.traceback())
		end
	else
		self._rpc_data = safe_dofile(kPtoFilePath)
	end
	self:ParseStruct()				--解析结构信息
	self:ParseProtocol()			--解析协议信息
	
	self:GenServerProxy()
end

Crpc.GetPtoMd5 = function(self)
	local ptoPath = Util.LuaPath(kPtoFilePath..".lua")
	local content = Util.ReadAllTextFromFile(ptoPath);
	local md5 = Util.HashToMD5Hex(content);
	return md5
end

Crpc.UpdatePto = function(self, pto)
	--重新生成协议
	self:LoadConfig(pto)
end

Crpc.ParseStruct = function(self)
	self._structs = self._rpc_data.class_cfg
end

Crpc.ParseProtocol = function(self)
	self._client_protocols = {}
	self._server_protocols = {}
	for k, v in pairs(self._rpc_data.function_cfg) do
		local func_info = 
		{
			id	= tonumber(k),
			name = v.function_name,
			args = v.args,
		}
		
		if v.function_name:find("rpc_client") then
			self._client_protocols[func_info.id] = func_info
		else
			self._server_protocols[func_info.id] = func_info
		end
	end
	local NET_WORK_EVENT = 
	{
		[101] = "OnConnect",
		[102] = "OnException",
		[103] = "OnDisconnect",
	}
	
	for i = 101, 103 do
		local func_info = 
		{
			id  = tonumber(i),
			name = NET_WORK_EVENT[i],
			args = {},
		}
		
		self._client_protocols[func_info.id] = func_info
	end
end

Crpc.GenServerProxy = function(self)
	for _,func_info in pairs(self._server_protocols) do
		local proxy_func = function(...)
			local args = {...}
			self._output = {}
			self:PackInt(func_info.id)
			for i = 1, #func_info.args do
				local arg_info = func_info.args[i]
				local arg = args[i]
				
				if arg_info.class_index ~= -1 then
					self:PackStruct(arg, arg_info.class_index, arg_info.type)
				else
					self:PackBaseType(arg, arg_info.type)
				end
			end
			
			print(func_info.name, ":", func_info.id, ":", table.get_table_str(args))
			
			--写网络包，发送网络包
			local buff = ByteBuffer.New()
			buff:InitWithBytes(self._output)
			ioo.networkManager:SendMessage(buff)
		end
		
		--rawset(_G, func_info.name, proxy_func)
		self._protocolMgr:GetProtocol()[func_info.name] = proxy_func
	end
end

Crpc.IntToBytes = function(self, num, endian, signed)
	if num < 0 and not signed then
		num = -num
		print("warning, dropping sign from number converting to unsigned")
	end
	
	local res = {}
	local n = 4
    if signed and num < 0 then
        num = num + 2^(n*8)
    end
    
    for k = n, 1, -1 do 	-- 256 = 2^8 bits per char.
        local mul = 2^(8*(k-1))
        res[k] = math.floor(num/mul)
        num = num - res[k] * mul
    end
    
    assert(num == 0)
    
    if endian == ENDIAN_BIG then
        local t = {}
        for k=1, n do
            t[k] = res[n-k+1]
        end
        res = t
    end
    
    return res
end

Crpc.BytesToInt = function(self, str, endian, signed)
	local t = str
	if endian == ENDIAN_BIG then --reverse bytes
        local tt = {}
        for k=1, #t do
            tt[#t-k+1] = t[k]
        end
        t = tt
    end
    
    local n=0
    for k=1, #t do
        n = n + t[k]*2^((k-1)*8)
    end
    
    if signed then
		n = (t[4] > 2^7 -1) and (n - 2^((#t*8))) or n -- if last bit set, negative.
    end
    
    return n
end

Crpc.PackInt = function(self, data)
	local bytes = self:IntToBytes(data, ENDIAN, true)
	for k, v in ipairs(bytes) do
		table.insert(self._output, v)
	end
end

Crpc.UnPackInt = function(self)
	local buf = {}
	for i = 1, 4 do
		buf[i] = table.remove(self._input, 1)
	end
	
	return self:BytesToInt(buf, ENDIAN, true)
end

Crpc.PackDouble = function(self, data)
	local res = Util.NumberToByte(data)
	for i = 0, res.Length - 1 do
		table.insert(self._output, res[i])
	end
end

Crpc.UnPackDouble = function(self)
	local buff = {}
	for i = 1, 8 do
		buff[i] = table.remove(self._input, 1)
	end
	
	return Util.ByteToNumber(buff)
end

Crpc.PackString = function(self, str)
	self:PackInt(string.len(str))
	for i=1, #str do
		table.insert(self._output, string.byte(str, i))
	end
end

Crpc.UnPackString = function(self)
	local len = self:UnPackInt()
	local buf = {}
	local str = ""
	if len > 100000 then
		print("Crpc.UnPackString too len", len)
		return
	end
	for i = 1, len do
		str = str..string.char(table.remove(self._input, 1))
	end
	 
	return str
end

Crpc.PackBuffer = function(self, buff)
	return self:PackString(buff)
end

Crpc.UnPackBuffer = function(self)
	return self:UnPackString()
end

--打包基础类型
Crpc.PackBaseType = function(self, value, btype)	
	--计算最终类型，因为这个与数组混合在一变量中定义的
	local _type = btype < ARRAY_VAR and btype or btype - ARRAY_VAR
	local isarray = btype >= ARRAY_VAR and 1 or 0
	
	if isarray ~= 0 then
		--将数组的长度压入数据包
		self:PackInt(#value)
	else
		value = { value }
	end
	
	for i=1, #value do
		if _type == 0 then
			self:PackInt(value[i])
		elseif _type == 1 then
			self:PackString(value[i])
		elseif _type == 3 then
			self:PackDouble(value[i])
		elseif _type == 5 then
			self:PackBuffer(value[i])
		end
	end
end

--解包基础类型
Crpc.UnPackBaseType = function(self, btype)
	local _type = btype < ARRAY_VAR and btype or btype - ARRAY_VAR
	local isarray = btype >= ARRAY_VAR and 1 or 0
	local array_count
	if isarray ~= 0 then
		array_count = self:UnPackInt()
	else
		array_count = 1
	end
	
	local args = {}
	for i = 1, array_count do
		if _type == 0 then
			table.insert(args, self:UnPackInt())
		elseif _type == 1 then
			table.insert(args, self:UnPackString())
		elseif _type == 3 then
			table.insert(args, self:UnPackDouble())
		end
	end
	
	if isarray ~= 0 then
		return args
	else
		return args[1]
	end
end

--打包结构体
Crpc.PackStruct = function(self, value, index, btype)
	--计算最终类型，因为这个与数组混合在一变量中定义的
	local _type = btype < ARRAY_VAR and btype or btype - ARRAY_VAR
	local isarray = btype >= ARRAY_VAR and 1 or 0
	
	if isarray ~= 0 then
		--将数组的长度压入数据包
		self:PackInt(#value)
	else
		value = { value }
	end
	
	local st_info = self._structs[index+1]
	for k = 1, #value do
		for i = 1, #st_info.field do
			local field_info = st_info.field[i]
			local field_name = field_info.field_name
			local field_value = value[k][i] or value[k][field_name]
			if field_info.class_index ~= -1 then
				self:PackStruct(field_value, field_info.class_index, field_info.type)
			else
				self:PackBaseType(field_value, field_info.type)
			end
		end
	end
end

--解包结构体
Crpc.UnPackStruct = function(self, index, btype)
	--计算最终类型，因为这个与数组混合在一变量中定义的
	local _type = btype < ARRAY_VAR and btype or btype - ARRAY_VAR
	local isarray = btype >= ARRAY_VAR and 1 or 0
	local array_count
	
	if isarray ~= 0 then
		array_count = self:UnPackInt()
	else
		array_count = 1
	end
	
	local args = {}
	local st_info = self._structs[index+1]
	for j = 1, array_count do
		local arg = {}
		for i = 1, #st_info.field do
			local field_info = st_info.field[i]
			if field_info.class_index ~= -1 then
				arg[field_info.field_name] = self:UnPackStruct(field_info.class_index, field_info.type)
			else
				arg[field_info.field_name] = self:UnPackBaseType(field_info.type)
			end
		end
		
		args[j] = arg
	end
	
	if isarray ~= 0 then
		return args
	else
		return args[1]
	end
end

--解析与执行下行协议
--参数为网络包
Crpc.OnPacket = function(self, key, pack)
	self._input = pack
	local pid = key
	local testid = self:UnPackInt()
	assert(pid == testid)
	local func_info = self._client_protocols[pid]
	if not func_info then
		print("receive unknown packet:", pid)
		return
	end
	
	local func_name = func_info.name
	local stub_func = self._protocolMgr:GetProtocol()[func_name]
	if not stub_func then
		print("stub func not implemented:", func_name)
		return
	end
	
	--print("正在解释协议：", pid, "   name：", func_name)
	local args = {}
	for i = 1, #func_info.args do
		local arg_info = func_info.args[i]
		if arg_info.class_index ~= -1 then
			table.insert(args, self:UnPackStruct(arg_info.class_index, arg_info.type))
		else
			table.insert(args, self:UnPackBaseType(arg_info.type))
		end
	end
	
	print(func_name..":"..pid, table.get_table_str(args))
	
	xpcall(
		function()
			self._protocolMgr:receiveRpc(func_name, args)
		end,
		
		function(e)
			print("---- call stub func failed["..func_name.."]!------\n"..e..debug.traceback())
		end
	)
end
