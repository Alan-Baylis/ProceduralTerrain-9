using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour {
    public const string NAME = "Foundation";


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    public int getWoodRequired()
    {
        return WOOD_REQUIRED;
    }
    public int getStoneRequried()
    {
        return STONE_REQUIRED;
    }
    public float getBuildTime()
    {
        return BUILD_TIME;
    }*/
    public string getName()
    {
        return NAME;
    }
}
