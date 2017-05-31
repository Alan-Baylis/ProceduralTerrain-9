using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    public GameObject equipted;
    public GameObject toolPos;
    private ToolAnimation toolAnimation;
    public Inventory inventory;
    //base Stats
    private int xp = 0;
    private int xpNeeded = 4;
    private int level;
    //Crafting
    private CraftingScript craftingScript;

    public GameObject levelUpUI;

    // Use this for initialization
    void Awake () {
        inventory = GetComponent<Inventory>();
        craftingScript = GetComponent<CraftingScript>();
        levelUpUI = GameObject.Find("UI").transform.Find("LevelUp").gameObject;
        equipted = null;
    }

    /*public PlayerScript()
    {
    }*/

    /*public void resetInv()
    {
        
    }*/
    public void equipt(GameObject input)
    {
        if (equipted == null)
        {
            equipted = Instantiate(input, toolPos.transform.position, toolPos.transform.rotation, toolPos.transform);
            //equipted.transform.parent = 
            //equipted.transform.position = Vector3.zero;
            // equipted.transform.position = Vector3.zero;
            toolAnimation = equipted.GetComponent<ToolAnimation>();
        }
        else if (!equipted.tag.Equals(input.tag))
        {
            Destroy(equipted);
            equipted = Instantiate(input, toolPos.transform.position, toolPos.transform.rotation, toolPos.transform);
            //equipted.transform.parent = 
            //equipted.transform.position = Vector3.zero;
           // equipted.transform.position = Vector3.zero;
            toolAnimation = equipted.GetComponent<ToolAnimation>();
        }
        else
            Destroy(equipted);
    }
    public void startToolAnimation()
    {
        toolAnimation.startToolAnimation();
    }
    public void stopToolAnimation()
    {
        toolAnimation.stopToolAnimation();
    }
    public void addMaterial(GameObject input)
    {
        IGatherable other = input.GetComponent<IGatherable>();
        Debug.Log("Harvesting: " + input.tag);
        switch (input.tag)
        {
            case "Tree":
                other.harvest();
                inventory.getWood().addMaterial(other.getWood());
                inventory.getBark().addMaterial(other.getBark());
                increaseXp(other.getXp());
                break;
            case "Stone":
                other.harvest();
                inventory.getStone().addMaterial(other.getStone());
                increaseXp(other.getXp());
                break;
            case "Boulder":
                other.harvest();
                inventory.getStone().addMaterial(other.getStone());
                increaseXp(other.getXp());
                break;
            case "Bush":
                other.harvest();
                inventory.getFiber().addMaterial(other.getFiber());
                increaseXp(other.getXp());
                break;
            default:
                break;
        }
        //inventory.printMaterials();
    }
    public Inventory getInventory()
    {
        return inventory;
    }
    public void increaseXp(int x)
    {
        xp += x;
        //Debug.Log("Gained Xp: " + xp);
        if(xp >= xpNeeded)
        {
            //Debug.Log("Leveled up calling levelup function");
            xp = xp % xpNeeded;
            level++;//increases level
            xpNeeded += (xpNeeded / 100) * 20; //increases xp need for next level by 20%
            levelUp(level);
        }
        //Debug.Log("XP needed: " + xpNeeded);
    }
    //level up and add new craftables to list
    public void levelUp(int lvl)
    {
        Text craftableText = levelUpUI.transform.Find("craftableAdded").GetComponent<Text>();
        string start = "Added ";
        string end = " to list of craftables.";
        //Debug.Log("Level up called now level: " + lvl);
        StartCoroutine("levelUpUIroutine");
        switch (lvl)
        {
            case 1:
                craftableText.text = start + "Wood Foundation" + end;
                craftingScript.addCraftable(new WoodFoundation());
                break;
            case 2:
                craftableText.text = start + "Wood Wall" + end;
                craftingScript.addCraftable(new WoodWall());
                break;
            case 3:
                craftableText.text = start + "Wood Wood Floor" + end;
                craftingScript.addCraftable(new WoodFloor());
                break;
            case 4:
                craftableText.text = start + "Wood Doorway" + end;
                craftingScript.addCraftable(new WoodDoorway());
                break;
            case 5:
                craftableText.text = start + "Wood Steps" + end;
                craftingScript.addCraftable(new WoodSteps());
                break;
            case 6:
                craftableText.text = start + "Bed" + end;
                craftingScript.addCraftable(new Bed());
                break;
            default:
                break;
        }
    }
    public int getLevel()
    {
        return level;
    }
    public void setLevel(int inputLevel)
    {
        for (int i = 1; i <= inputLevel; i++)
        {
            levelUp(i);
        }
    }
    IEnumerator levelUpUIroutine()
    {
        Debug.Log("Starting coroutine");
        //Turn My game object that is set to false(off) to True(on).
        levelUpUI.SetActive(true);

        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(3);

        //Game object will turn off
        levelUpUI.SetActive(false);
    }
}
