using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Singleton design to enoforce only one instance of worldcontroller
public class MeshDoNotDestroy : MonoBehaviour
{
    private static MeshDoNotDestroy _instance;

    public static MeshDoNotDestroy Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        GameObject.DontDestroyOnLoad(this);
    }
}
