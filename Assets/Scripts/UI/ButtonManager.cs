using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour {
    public CraftingScript craftingScript;
    public PlayerScript player;

    private void Awake()
    {
        /*player = GameObject.Find("Player").GetComponent<PlayerScript>();
        craftingScript = GameObject.Find("Player").GetComponent<CraftingScript>();*/
        player = WorldController.playerObjInst.GetComponent<PlayerScript>();
        craftingScript = WorldController.playerObjInst.GetComponent<CraftingScript>();
        DontDestroyOnLoad(this);
        assignButtons();
    }

    /// <summary>
    /// Script for Crafting Window UI Buttons
    /// </summary>

    /*private void OnLevelWasLoaded(int level)
    {
        //craftingScript = GameObject.Find("Player").GetComponent<CraftingScript>();
        cleanUpUIforReload();
    }*/
    public void nextItem()
    {
        //Cycle to next item      
        craftingScript = WorldController.playerObjInst.GetComponent<CraftingScript>();
        craftingScript.nextItem();
    }
    public void prevItem()
    {
        //Cycle to next item      
        craftingScript = WorldController.playerObjInst.GetComponent<CraftingScript>();
        Debug.Log("previtem start");
        craftingScript.prevItem();
    }

    public void printRequirements()
    {
        //Temp function just to test functionablity
        //Debug.Log(craftingScript.getName());
        //Debug.Log(craftingScript.getBuildTime());
    }
    public void craftItem()
    {
        craftingScript = WorldController.playerObjInst.GetComponent<CraftingScript>();
        craftingScript.craftItem();
    }

    /// <summary>
    /// Script for Inventory Window UI Buttons
    /// </summary>
    
    public void selectItem(GameObject button)
    {
        player = WorldController.playerObjInst.GetComponent<PlayerScript>();
        player.getInventory().printMaterials();
        player.getInventory().selectBuildable(button);
    }
    
    public void cleanUpUIforReload()
    {
        GameObject UI = GameObject.Find("UI");
        GameObject inventory = UI.transform.Find("Inv-Craft").transform.Find("Inventory").transform.Find("LayoutGroup").gameObject;
        foreach(Transform child in inventory.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Script for Menu Window UI Buttons
    /// </summary>
    /// 
    public void goToMap()
    {
        WorldController.homeBase.storeBase();
        SceneManager.LoadScene("LevelGenScrene");
        Invoke("genMap", 0.1f);
        player = WorldController.Instance.getplayerScriptInst().GetComponent<PlayerScript>();
        player.transform.position = new Vector3(0, 100, 0);       
    }
    public void goToBase()
    {
        SceneManager.LoadScene("HomeBase");
        Invoke("reloadBase", 0.1f);
        player = WorldController.Instance.getplayerScriptInst().GetComponent<PlayerScript>();
        player.transform.position = new Vector3(0, 2, 0);
    }
    public void reloadBase()
    {
        WorldController.homeBase.reloadBase();
    }
    public void genMap()
    {
        WorldController.Instance.GenMap();
    }
    public void quit()
    {
        Application.Quit();
    }


    /////////////
    /////test assing button per script
    //////////////
    Button[] buttons;

    void assignButtons()
    {
        //gadgetBase = GameObject.FindGameObjectWithTag("gadgetBase");
        buttons = GameObject.Find("UI").transform.Find("Inv-Craft").Find("CraftingWindow").GetComponentsInChildren<Button>();
        GameObject mainScript = this.gameObject;
        ButtonManager buttonManagerScript = this;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name.Equals("RightArrow"))
            {
                buttons[i].onClick.AddListener(delegate () { buttonManagerScript.nextItem(); });
            }
            else if (buttons[i].name.Equals("LeftArrow"))
            {
                buttons[i].onClick.AddListener(delegate () { buttonManagerScript.prevItem(); });
            }
        }
    }
}
