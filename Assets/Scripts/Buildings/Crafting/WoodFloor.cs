using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFloor : ICraftable
{
    private int woodRequired = 15;
    private int barkRequired = 5;
    private int stoneRequired = 0;
    private int fiberRequired = 5;
    private float buildTime = 5f;
    private string name = "WoodFloor";
    private GameObject blueprint;

    public GameObject getGameObject()
    {
        blueprint = (GameObject)Resources.Load("WoodFloorBlueprint");
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

