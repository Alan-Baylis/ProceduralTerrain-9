using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSteps : ICraftable
{
    private int woodRequired = 35;
    private int barkRequired = 15;
    private int stoneRequired = 5;
    private int fiberRequired = 0;
    private float buildTime = 5f;
    private string name = "WoodSteps";
    private GameObject blueprint;

    public GameObject getGameObject()
    {
        blueprint = (GameObject)Resources.Load("WoodStepsBlueprint");
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
