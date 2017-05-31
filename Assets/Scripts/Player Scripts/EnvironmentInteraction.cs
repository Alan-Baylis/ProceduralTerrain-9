using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EnvironmentInteraction : MonoBehaviour {
    public float interactionDistance;
    public PlayerScript playerScript;
    public LevelMap levelMap;
    public BuildingScript buildingScript;
    public GameObject levelMapLocation;
    public bool buildingMode = false;
    public bool inInvCraftWindow = false;
    private bool inMenu = false;

    private GameObject UI;
    private GameObject invCraftWindow;
    private GameObject menu;

    //Tools
    public GameObject axe;
    public GameObject pickaxe;
    public GameObject shovel;
    public GameObject sickle;

    private float timeHeld = 0;
    //Mouse hold time for gathering
    private float holdTime = 2f;
    //For mouse testing //Animation purpose
    public CharacterController characterController;
    public Animator animator;
    //For Restart
    private WorldController worldController;

    private bool harvesting = false;



    //public GameObject craftTestObject;
    //public CraftingScript craftTest;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        worldController = WorldController.Instance;
        UI = GameObject.Find("UI");
        invCraftWindow = UI.transform.Find("Inv-Craft").gameObject;
        menu = UI.transform.Find("Menu").gameObject;
    }

    // Update is called once per frame
    void Update() {
        //Cursor.visible = true;
        RaycastHit hit;
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!buildingMode)
        {
            if (!inInvCraftWindow && !inMenu)
            {
                if (Input.GetMouseButton(0))
                {
                    //animator.SetBool("Swing", true);
                    if (playerScript.equipted != null)
                        playerScript.startToolAnimation();
                    if (Physics.Raycast(interactionRay, out hit, interactionDistance))
                    {
                        //Interact with enviorment
                        if (!(hit.collider.tag == "Terrain"))
                        {
                            harvesting = true;
                            timeHeld += Time.deltaTime;
                            //Debug.Log("hit: " + hit.transform.gameObject.tag);
                            if (timeHeld >= holdTime)
                            {
                                timeHeld = 0;
                                harvest(hit.transform.gameObject);
                            }
                        }
                        //else if ((hit.collider.tag == "Terrain"))
                        //used methos to level map
                        //levelMap.LevelOutTerrain(levelMapLocation.transform.position);
                        //buildingScript.build();
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (playerScript.equipted != null)
                    playerScript.stopToolAnimation();
                //animator.SetBool("Swing", false);
                harvesting = false;
                timeHeld = 0;
            }
            if (Input.GetMouseButtonDown(2))
            {
                //buildingScript.cycleBuildable();
                //craftTest.getRequirements(0);
            }
            if (Input.GetKeyDown("1"))
            {
                playerScript.equipt(axe);
            }
            if (Input.GetKeyDown("2"))
            {
                playerScript.equipt(pickaxe);
            }
            if (Input.GetKeyDown("3"))
            {
                playerScript.equipt(shovel);
            }
            if (Input.GetKeyDown("4"))
            {
                playerScript.equipt(sickle);
            }
            if (Input.GetButtonDown("Inventory"))
            {
                if (!inMenu)
                {
                    inInvCraftWindow = !inInvCraftWindow;
                    invCraftWindow.SetActive(inInvCraftWindow);
                    if (inInvCraftWindow)
                    {
                        characterController.enabled = false;
                        characterController.GetComponent<FirstPersonController>().enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                    }
                    else
                    {
                        characterController.enabled = true;
                        characterController.GetComponent<FirstPersonController>().enabled = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
            }
        }
        if (Input.GetButtonDown("switchMode"))
        {
            buildingMode = !buildingMode;
            Debug.Log(buildingMode);
        }
        /*if (Input.GetButtonDown("Crafting"))
        {
            inCraftingUI = !inCraftingUI;
            craftingWindow.SetActive(inCraftingUI);
            if (inCraftingUI)
            {
                characterController.enabled = false;
                characterController.GetComponent<FirstPersonController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                characterController.enabled = true;
                characterController.GetComponent<FirstPersonController>().enabled = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }*/
        //Equipting tools
        
        if (Input.GetButtonDown("Menu"))
        {
            inMenu = !inMenu;
            menu.SetActive(inMenu);
            if (inMenu)
            {
                characterController.enabled = false;
                characterController.GetComponent<FirstPersonController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else if(!inInvCraftWindow)
            {
                characterController.enabled = true;
                characterController.GetComponent<FirstPersonController>().enabled = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        if (Input.GetButtonDown("Restart"))
        {
            worldController.newGame();
        }
        if (Input.GetButtonDown("NewGame"))
        {
            worldController.GenMap();
        }
    }

    // shouldnt do any mod just key register so just call harvet in player script which does all the work
    void harvest(GameObject interactObject)
    {
        if (playerScript.equipted == null && interactObject.tag.Equals("Stone"))
        {
            playerScript.addMaterial(interactObject);
        }
        else if(playerScript.equipted != null)
        {
            switch (interactObject.tag)
            {
                case "Tree":
                    if (playerScript.equipted.tag.Equals("Axe"))
                        playerScript.addMaterial(interactObject);
                    break;
                case "Boulder":
                    if (playerScript.equipted.tag.Equals("PickAxe"))
                        playerScript.addMaterial(interactObject);
                    break;
                case "Bush":
                    if (playerScript.equipted.tag.Equals("Sickle"))
                        playerScript.addMaterial(interactObject);
                    break;
                default:
                    break;
            }
        }
        /*
        if (interactObject.tag.Equals("Tree"))
        {
            playerScript.getInventory().getMaterials().addWood(2);
            playerScript.increaseXp(2);
        }*/
    }
    public bool getHarvesting()
    {
        return harvesting;
    }
}
