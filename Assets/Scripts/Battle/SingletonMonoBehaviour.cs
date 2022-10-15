using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    //唯一のインスタンス
    private static T instance;

    //private SingletonMonoBehaviour(){ }

    //インスタンスを参照するプロパティ（読み取り専用）
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    Debug.LogError(typeof(T) + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    virtual protected void Awake ()
    {
        // 他のGameObjectにアタッチされているか調べる.
        // アタッチされている場合は破棄する.
        if (this != Instance)
        {
            Destroy(this.gameObject);
            //Destroy(this.gameObject);
            Debug.LogError(
                typeof(T) +
                " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
            return;
        }

        // なんとかManager的なSceneを跨いでこのGameObjectを有効にしたい場合は
        // ↓コメントアウト外してください.
        //DontDestroyOnLoad(this.gameObject);
    }
}
