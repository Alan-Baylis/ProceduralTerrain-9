using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFoundation : ICraftable {
    private int woodRequired = 10;
    private int barkRequired = 5;
    private int stoneRequired = 20;
    private int fiberRequired = 5;
    private float buildTime = 5f;
    private string name = "WoodFoundation";
    private GameObject blueprint;

    public GameObject getGameObject()
    {
        blueprint = (GameObject)Resources.Load("WoodFoundationBlueprint");
        return blueprint;
    }
    public int getRequiredWood()
    {
        return woodRequired;
    }
    public int getRequiredBark()
    {
        return barkRequired;
    }
    public int getRequiredStone()
    {
        return stoneRequired;
    }
    public int getRequiredFiber()
    {
        return fiberRequired;
    }
    public float getbuildTime()
    {
        return buildTime;
    }
    public string getName()
    {
        return name;
    }
}
