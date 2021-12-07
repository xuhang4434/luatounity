using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFGame : MonoBehaviour
{
    public static XFGame Instance = null;

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
