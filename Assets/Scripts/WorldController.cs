using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

//Singleton design to enoforce only one instance of worldcontroller
public class WorldController : MonoBehaviour {
    private MapGen mapGen;
    public GameObject player;
    public GameObject buttonManager;
    public static HomeBase homeBase;

    //Own instance
    private static WorldController _instance;
    //Button manager instance and player manager instance
    private static bool buttonManagerInst = false;
    public static GameObject buttonManagerObjInst = null;
    private static bool playerInst = false;
    public static GameObject playerObjInst = null;
    //Allow other scripts to easy get world controller instance
    public static WorldController Instance { get { return _instance; } }

    //singleton design and create player, button manager if not in scene
    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            homeBase = new HomeBase();
        }
        mapGen = GetComponent<MapGen>();
        if (!playerInst)
        {
            playerInst = true;
            playerObjInst = Instantiate(player, new Vector3(0, 10, 0), Quaternion.identity);
        }
        if (!buttonManagerInst)
        {
            buttonManagerInst = true;
            buttonManagerObjInst = Instantiate(buttonManager);
        }
    }
    //Reloads scene in base
    public void newGame()
    {
        SceneManager.LoadScene("HomeBase");
        homeBase = new HomeBase();
    }
    //Generates new map
    public void GenMap()
    {      
        mapGen.GenerateMap();
    }
    //XML serializer to allow saving/loads, saves to user prefs
    public void saveWorld()
    {
        XmlSerializer serialzer = new XmlSerializer(typeof(HomeBase));
        TextWriter writer = new StringWriter();
        serialzer.Serialize(writer, homeBase);
        writer.Close();

        Debug.Log(writer.ToString());

        //Saving XML to Playerprefs could save to file instead but due to small amount of data
        //player prefs is fine
        PlayerPrefs.SetString("SaveGame0", writer.ToString());

    }
    public void loadWorld()
    {
        SceneManager.LoadScene("HomeBase");
        playerObjInst.GetComponent<PlayerScript>().getInventory().resetInventory();
        buttonManagerObjInst.GetComponent<ButtonManager>().cleanUpUIforReload();       
        Invoke("load", 0.2f);       
    }
    public void load()
    {
        XmlSerializer serialzer = new XmlSerializer(typeof(HomeBase));
        TextReader reader = new StringReader(PlayerPrefs.GetString("SaveGame0"));
        homeBase = (HomeBase)serialzer.Deserialize(reader);
        reader.Close();
    }
    public GameObject getplayerScriptInst()
    {
        return playerObjInst;
    }
}
