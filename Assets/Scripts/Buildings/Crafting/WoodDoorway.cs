using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDoorway : ICraftable
{
    private int woodRequired = 10;
    private int barkRequired = 10;
    private int stoneRequired = 2;
    private int fiberRequired = 10;
    private float buildTime = 5f;
    private string name = "WoodDoorway";
    private GameObject blueprint;

    public GameObject getGameObject()
    {
        blueprint = (GameObject)Resources.Load("WoodDoorwayBlueprint");
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
    public float getbuildTime()
    {
        return buildTime;
    }
    public int getRequiredFiber()
    {
        return fiberRequired;
    }
    public string getName()
    {
        return name;
    }
}
