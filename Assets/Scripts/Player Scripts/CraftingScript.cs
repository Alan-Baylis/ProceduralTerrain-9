using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingScript : MonoBehaviour {
    public List<ICraftable> craftable;
    private int index = 0;
    public Text itemName;
    public Text woodReq;
    public Text barkReq;
    public Text stoneReq;
    public Text fiberReq;
    public PlayerScript player;
    private Inventory playerInventory;
    private GameObject craftingWindow;
    //public BuildingScript buildingScript;


    // Use this for initialization
    void Awake () {
        craftable = new List<ICraftable>();
        Text[] requirements;       
        playerInventory = GetComponent<Inventory>();
        craftingWindow = GameObject.Find("UI").transform.Find("Inv-Craft").transform.Find("CraftingWindow").gameObject;
        itemName = craftingWindow.transform.Find("CurrentItem").gameObject.GetComponent<Text>();
        requirements = craftingWindow.transform.Find("Requirements").gameObject.GetComponentsInChildren<Text>();       
        woodReq = requirements[0];
        barkReq = requirements[1];
        stoneReq = requirements[2];
        fiberReq = requirements[3];
    }

    // Update is called once per frame
    void Update () {
        
	}

    public void craftItem()
    {
        if (checkReq())
        {
            playerInventory.getWood().removeMat(getWoodRequired());
            playerInventory.getBark().removeMat(getBarkRequired());
            playerInventory.getStone().removeMat(getStoneRequired());
            playerInventory.addBuildable(craftable[index].getGameObject(), craftable[index].getName());
        }
    }
    private bool checkReq()
    {
        bool check = false;
        if (playerInventory.getWood().checkAmount() >= getWoodRequired())
        {
            check = true;
        }
        if (check && playerInventory.getBark().checkAmount() >= getBarkRequired())
        {
            check = true;
        }
        if (check && playerInventory.getStone().checkAmount() >= getStoneRequired())
        {
            check = true;
        }
        if (check && playerInventory.getFiber().checkAmount() >= getFiberRequired())
        {
            check = true;
        }
        return check;
    }
    public void addCraftable(ICraftable newElement)
    {
        craftable.Add(newElement);
    }
    public void nextItem()
    {
        if (craftable != null)
        {
            if (index < craftable.Count-1)
                index++;
            else
                index = 0;
            refreshReq();
        }
    }
    public void prevItem()
    {
        Debug.Log("craftable previtem start");
        if (craftable != null)
        {
            Debug.Log("index:" + index);
            if (index < craftable.Count - 1)
                index++;
            else
                index = 0;
            Debug.Log("new index:" + index);
            refreshReq();
        }
    }

    private void refreshReq()
    {
        itemName.text = getName();
        woodReq.text = "wood: " + getWoodRequired().ToString();
        barkReq.text = "bark: " + getBarkRequired().ToString();
        stoneReq.text = "stone: " + getStoneRequired().ToString();
        fiberReq.text = "fiber: " + getFiberRequired().ToString();
    }
    private void getRequirements()
    {
        Debug.Log(craftable[index].getName());
        Debug.Log("Wood:" + craftable[index].getRequiredWood());
        Debug.Log("Stone: " + craftable[index].getRequiredStone());
    }
    private int getWoodRequired()
    {       
        return craftable[index].getRequiredWood();
    }
    private int getBarkRequired()
    {
        return craftable[index].getRequiredBark();
    }
    private int getStoneRequired()
    {
        return craftable[index].getRequiredStone();
    }
    private int getFiberRequired()
    {
        return craftable[index].getRequiredFiber();
    }   
    private string getName()
    {
        return craftable[index].getName();
    }
    private float getBuildTime()
    {
        return craftable[index].getbuildTime();
    }


}
