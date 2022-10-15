using System.Collections.Generic;
using UnityEngine;

public class AlbumData : SingletonMonoBehaviour<AlbumData>
{
    public int[] albumArray = new int[10];

    void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        albumArray[0] = 0; 
    }


    //get a character
    public void SetAlbum(int num)
    {
        albumArray[num] = num;
        
        
    }
}
