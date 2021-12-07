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
    }	

    private void Start()
    {
        
    }
}
