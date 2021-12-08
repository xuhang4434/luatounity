UILayer =
{
    Base = 1,
    Normal = 2,
    Guide = 3,
    Pop = 4,
    Click = 5,
}

local Layer = {
    'Base',
    'Normal',
    'Guide',
    'Pop',
    'Click'
}

PanelAttribute = { }

--- 不用检查 PanelId是否唯一 类名是唯一的
---@param panelClass BasePage
---@Params = {
    -- mResident = 是否缓存 隐藏不消毁
    -- uiLayer = 父节层级
--}
function RegisterUIAttribute(panelClass, Params)
    --- checkPram
    if Params.assetpath == nil then
        Debugger.LogError(PanelId + "must set assetpath  if use RegisterUIAttribute")
        return
    end
    PanelAttribute[panelClass.__cname] = Params
end


XFUIManager = class("XFUIManager")
singleton_class(XFUIManager)


function XFUIManager:Init(prefab_path, block_path)
    self._block_path = block_path
    self.canvas = CreateProxyObj(UIManager.canvasId, "GameObject")
    self.cva_tf = self.canvas:GetComponent(xf.GloabalUnityClass.RectTransform)
    local canvas_size = self.cva_tf:GetsizeDelta()
    self.layers = {}
    local asset_id = ResMgr.LoadAsset(prefab_path)
    local tf = nil
    local width = UnityEngine.Screen.width
    local height = UnityEngine.Screen.height
    
    local ratio = height / width
    local delta_x = 0
    local delta_y = 0
    if ratio > 1.86 then
        delta_y = -60
    elseif ratio < 1.77 then
        delta_x = 720 - canvas_size.x
    end

    if asset_id ~= '' then
        for v, k in ipairs(Layer) do
            -- local type = {}
            -- table.insert(type, "UnityEngine.RectTransform,UnityEngine")
            -- if addComponent must use this
            self.layers[v] = self.canvas:AddChildFromPrefab(asset_id)
            tf = self.layers[v]:GetComponent(xf.GloabalUnityClass.RectTransform)
            tf:Setname(k)
            tf:SetsizeDelta(Vector2.New(delta_x, delta_y))
            tf:SetlocalPosition(Vector3.New(0, delta_y / 2, 0))
        end
    end
    
    self.pages = {}
    self.pageDestroyCb = nil
end

function XFViewMgr:OpenView(panelClass, user_data, complete_cb)
    self:_DoOpenView(panelClass, user_data, complete_cb)
end

function XFViewMgr:GetParent(panelClassName)
    -- return self.canvas
    --- UI配置
    local param  = PanelAttribute[panelClassName]
    if param.uiLayer then
        return self.layers[param.uiLayer]
    end
    return self.layers[UILayer.Normal]
end

---@param panelClass BasePage
function XFViewMgr:_DoOpenView(panelClass, user_data, complete_cb)
    local panelClassName
    local clz
    if type(panelClass) == 'string' then
        panelClassName = panelClass
        clz = _G[panelClassName]
    else
        panelClassName = panelClass.__cname
        clz = panelClass
    end
    xf.ClientEvent:Emit(xf.Const.Event.OnViewOpen, panelClassName)
    
    user_data = user_data or {}
    local parent = user_data.parent or clz:GetParent() or self:GetParent(panelClassName)
    --- UI配置
    local param  = PanelAttribute[panelClassName]
    ---@type BasePage
    local curPage = self.pages[panelClassName]
    if  curPage == nil then
        curPage = clz.new(parent, param, complete_cb)
        self.pages[panelClassName] = curPage
    else
        if curPage.isVisable then return end
        curPage:SetCompleteCb(complete_cb)
    end
    curPage:SetData(user_data)
    curPage:Show(self._block_path)
end

---@param panelClass BasePage
function XFViewMgr:HideView(panelClass)
    local panelClassName
    if type(panelClass) == 'string' then
        panelClassName = panelClass
    else
        panelClassName = panelClass.__cname
    end
    local curPage = self.pages[panelClassName]
    if not curPage then
        return
    end
    curPage:Hide()
end

--@param 
function XFViewMgr:CloseView(panelClass)
    local panelClassName = panelClass.__cname
    local curPage = self.pages[panelClassName]
    if not curPage then
        return
    end
    
    self.pages[panelClassName] = nil
    curPage:Destroy()
end

function XFViewMgr:CloseAllViewByModule(module_name)
    local param
    local path
    local idx
    local view
    local module_struct = '/' .. module_name .. '/'
    
    for k, v in pairs(self.pages) do
        param = PanelAttribute[k]
        path = param.assetpath
        idx = string.find(path, module_struct)
        if idx then
            view = self.pages[k]
            view:Destroy()
            self.pages[k] = nil
        end
    end
end

function XFViewMgr:HideAllView(arr_except)
    arr_except = arr_except or {}
    for k, v in pairs(self.pages) do
        if not xf.Table:Include(arr_except, k) then
            self.pages[k] = nil
            v:Hide()
        end
    end
end

function XFViewMgr:CloseAllView(arr_except)
    arr_except = arr_except or {}
    local param
    for k, v in pairs(self.pages) do
        param  = PanelAttribute[k]
        --顶层不关
        if param.uiLayer ~= UILayer.Pop then
            if not xf.Table:Include(arr_except, k) then
                self.pages[k] = nil
                v:Destroy()
            end
        end
    end
end

function XFViewMgr:CloseViewByLayer(layer)
    local param
    for k, v in pairs(self.pages) do
        param  = PanelAttribute[k]
        if param.uiLayer == layer then
            self.pages[k] = nil
            v:Destroy()
        end
    end
end

-- TODO:
-- 场景切换 强行移除所有UI 
-- 对应的asset 该如何释放
function XFViewMgr:RemoveAllView()
    for key, value in pairs(self.pages) do
        local curPage = value
        curPage:Destroy()
    end
    self.pages = {}
end

function XFViewMgr:Update(deltaTime, unscaledDeltaTime)
    for key, value in pairs(self.pages) do
        local curPage = value
        if curPage.isVisable == true then
            curPage:OnUpdate(deltaTime, unscaledDeltaTime)
        end
    end
end

function XFViewMgr:GetCanvasTransform()
    return self.cva_tf
end

function XFViewMgr:GetCanvas()
    return self.canvas
end

function XFViewMgr:GetParentByName(name)
    return self.layers[name]
end


--- 派发UI事件（click, drag, animationEvent ?）
-- function XFViewMgr.DiapathEvent(objId, eventType, param)
--     -- params{
--         -- eventName:"click,drag?"
--     -- }
--     -- must XFUIBehaviour sub 
--     ---@type XFUIBehaviour
--     local uibehaviour = xf.objMap[objId]
--     if uibehaviour then
--         ---@type XFUnityEvent
--         local event = uibehaviour.uiEvents[eventType]
--         event:Call(param)
--     else
--         error("not find ui event to fire" .. tostring(objId))
--     end
-- end


singleton_class(XFViewMgr)
---@type XFViewMgr
XFEngine.ViewMgr = XFViewMgr.Instance;