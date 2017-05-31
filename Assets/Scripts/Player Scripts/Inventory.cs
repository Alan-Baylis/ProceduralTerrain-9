using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory: MonoBehaviour {
//    private List<GameObject> tools;
    private Materials wood;
    private Materials bark;
    private Materials stone;
    private Materials fiber;
    private Dictionary<string, Stack> buildable;
    //private int position;
    //private int key;

    private GameObject slot;

    private int row = 0;
    private int col = 0;
    private int maxcol = 2;

    private GameObject curSelected;

    private GameObject UICanvas;
    private void Start()
    {
  //      tools = new List<GameObject>();
        wood = new Materials("Wood");
        bark = new Materials("Bark");
        stone = new Materials("Stone");
        fiber = new Materials("Fiber");
        //position = 0;
        //key = 0;
        buildable = new Dictionary<string, Stack>();
        UICanvas = GameObject.Find("UI").transform.Find("Inv-Craft").transform.Find("Inventory").Find("LayoutGroup").gameObject;
        slot = (GameObject)Resources.Load("Slot");
    }

    //Could remove all placement and col,row operations due to implementation of grid layout
    //Instantiate object to allow placement
    public void selectBuildable(GameObject button)
    {
        curSelected = (GameObject) buildable[button.name].Peek();
        GameObject.Instantiate(curSelected);
        removeBuildable(button.name, button);
    }
    //Buildable inventory  by gameobject and string name with stacking
    public void addBuildable(GameObject input, string name)
    {

        if (input.tag.Equals("Blueprint"))
        {
            if (buildable.ContainsKey(name))
            {
                GameObject curSlot;
                Text[] slotDetails;

                if (buildable[name].Count != 0)
                {
                    curSlot = GameObject.Find("UI").transform.Find("Inv-Craft").Find("Inventory").Find("LayoutGroup").Find(name).gameObject;                
                    buildable[name].Push(input);
                    slotDetails = curSlot.GetComponentsInChildren<Text>();
                    slotDetails[1].text = "" + buildable[name].Count;
                }
                else
                {
                    GameObject newSlot;
                    RectTransform newSlotDetails;

                    buildable[name].Push(input);
                    if (col < maxcol)
                    {
                        newSlot = GameObject.Instantiate(slot, slot.transform.position, slot.transform.rotation);
                        newSlotDetails = newSlot.GetComponent<RectTransform>();                  
                        newSlot.transform.position = slot.transform.position + new Vector3(newSlotDetails.rect.width * col, newSlotDetails.rect.height * -row, 0);
                        newSlotDetails.SetParent(UICanvas.transform, false);                    
                        newSlot.name = name;
                        slotDetails = newSlot.GetComponentsInChildren<Text>();
                        slotDetails[0].text = name;
                        slotDetails[1].text = "1";
                        incCol();
                    }
                }
            }
            else
            {
                Stack temp = new Stack();
                GameObject newSlot;
                RectTransform newSlotDetails;
                Text[] slotDetails; 

                temp.Push(input);
                buildable.Add(name, temp);
                if (col < maxcol)
                {
                    newSlot = GameObject.Instantiate(slot, slot.transform.position, slot.transform.rotation);
                    newSlotDetails = newSlot.GetComponent<RectTransform>();
                    newSlot.transform.position = slot.transform.position + new Vector3(newSlotDetails.rect.width * col, newSlotDetails.rect.height * -row, 0);
                    newSlotDetails.SetParent(UICanvas.transform, false);
                    newSlot.name = name;
                    slotDetails = newSlot.GetComponentsInChildren<Text>();
                    slotDetails[0].text = name;
                    slotDetails[1].text = "1";
                    incCol();
                }
            }
            //Debug.Log("col " + col);
        }
    }
    //add buildable only by string
    public void addBuildableString(string name)
    {
        string prefabname = name + "Blueprint";
        GameObject prefab = (GameObject)Resources.Load(prefabname);
        addBuildable(prefab, name);
    }
    //Removes buildable from inventory
    public void removeBuildable(string name, GameObject button)
    {
        Text[] slotDetails;
        if (buildable.ContainsKey(name))
        {
            if (buildable[name].Count>1)
            {
                slotDetails = button.GetComponentsInChildren<Text>();
                slotDetails[1].text = "" + (buildable[name].Count - 1);
                buildable[name].Pop();
            }
            else
            {
                if (col > 0)
                {
                    col--;
                }
                else if(col == 0 && row > 0)
                {
                    col = maxcol-1;
                    row--;
                }
                buildable[name].Pop();
                GameObject.Destroy(button);            
            }         
        }
    }
    //Empties and resets
    public void resetInventory()
    {
        buildable.Clear();
        row = 0;
        col = 0;
    }
    public Dictionary<string, Stack> getBuildable()
    {
        return buildable;
    }
    private void incCol()
    {
        if (col+1 < maxcol)
        {
            col++;

        }
        else
        {
            row++;
            col = 0;
        }
    }
    //Get Basic Inventory
    public Materials getWood()
    {
        return wood;
    }
    public Materials getBark()
    {
        return bark;
    }
    public Materials getStone()
    {
        return stone;
    }   
    public Materials getFiber()
    {
        return fiber;
    }
    //print materials
    public void printMaterials()
    {
        wood.printMat();
        bark.printMat();
        stone.printMat();
    }
}
