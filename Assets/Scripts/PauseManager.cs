using System.Collections.Generic;
using UnityEngine;

public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    void Awake()
    {
        base.Awake();
    }

    //ゲームの一時停止機能
    public void Pause(bool active)
    {
        if(!active)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
