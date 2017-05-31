using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {
    public List<GameObject> buildable;
    public GameObject spawnLocation;
    private int position;

	// Use this for initialization
	void Start () {
        buildable = new List<GameObject>();
        position = 0;
	}
	
    //Add new buildable gameobject
    public void addBuildable(GameObject build)
    {
        buildable.Add(build);
        Debug.Log("Added to list size: " + buildable.Count);
    }

    //Cycle through list of buildable gameobjects
    public void cycleBuildable()
    {
        if (!(buildable.Count == 0))
        {
            if (position < buildable.Count)
                position++;
            else
                position = 0;
            Debug.Log("selected: " + position);
        }
        else
            Debug.Log("Empty");
    }

    //Method to build
    public void build()
    {
        if(buildable.Count != 0)
        Instantiate(buildable[position], spawnLocation.transform.position, Quaternion.identity , spawnLocation.transform);
    }
}
