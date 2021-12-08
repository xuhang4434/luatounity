using LuaInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XFGame : MonoBehaviour
{
    public static XFGame Instance = null;

    //=============lua启动相关
    protected LuaState luaState = null;
    protected LuaLooper loop = null;
    protected LuaFunction levelLoaded = null;
    //=============lua启动相关

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.runInBackground = true;
        Instance = this;
        GameObject.DontDestroyOnLoad(gameObject);

        //lua启动
        luaStart();

        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (levelLoaded != null)
        {
            levelLoaded.BeginPCall();
            levelLoaded.Push(scene.buildIndex);
            levelLoaded.PCall();
            levelLoaded.EndPCall();
        }

        if (luaState != null)
        {
            luaState.RefreshDelegateMap();
        }
    }

    private void Start()
    {
        //初始化C#中的单例管理类
        initSingletonManager();
        //开始lua的主逻辑main函数
        startMainLogic();
    }

    /// <summary>
    /// lua启动
    /// </summary>
    private void luaStart()
	{
        if(LuaFileUtils.Instance == null)
		{
            return;
		}

        luaState = new LuaState();

        luaState.OpenLibs(LuaDLL.luaopen_pb);
        luaState.OpenLibs(LuaDLL.luaopen_struct);
        luaState.OpenLibs(LuaDLL.luaopen_lpeg);

        if (LuaConst.openLuaSocket)
        {
            luaState.BeginPreLoad();
            luaState.RegFunction("socket.core", LuaOpen_Socket_Core);
            luaState.RegFunction("mime.core", LuaOpen_Mime_Core);
            luaState.EndPreLoad();
        }
   
        luaState.LuaSetTop(0);
        LuaBinder.Bind(luaState);
        DelegateFactory.Init();
        LuaCoroutine.Register(luaState, this);

        luaState.Start();

        loop = gameObject.AddComponent<LuaLooper>();
        loop.luaState = luaState;

        luaState.DoFile("XFEngine/Main.lua");
        levelLoaded = luaState.GetFunction("OnLevelWasLoaded");
        LuaFunction main = luaState.GetFunction("Main");
        main.Call();
        main.Dispose();
        main = null;
    }
    /// <summary>
    /// //初始化C#中的单例管理类
    /// </summary>
    private void initSingletonManager()
	{

	}

    /// <summary>
    /// 开始游戏主逻辑
    /// </summary>
    private void startMainLogic()
	{
        
	}



    //======================================================================
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Socket_Core(IntPtr L)
    {
        return LuaDLL.luaopen_socket_core(L);
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Mime_Core(IntPtr L)
    {
        return LuaDLL.luaopen_mime_core(L);
    }
}
