using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFGame : MonoBehaviour
{
    public static XFGame Instance = null;

    //=============lua启动相关
    protected LuaState luaState = null;
    protected LuaLooper loop = null;
    protected LuaFunction levelLoaded = null;

    protected bool openLuaSocket = false;
    protected bool beZbStart = false;
    //=============lua启动相关

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.runInBackground = true;
        Instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }	

    private void Start()
    {
        initSingletonManager();
        startMainLogic();
    }

    /// <summary>
    /// 初始化单例管理类
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
}
