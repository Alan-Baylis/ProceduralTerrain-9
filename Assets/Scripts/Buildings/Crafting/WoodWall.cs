using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodWall : ICraftable  {
    private int woodRequired = 10;
    private int barkRequired = 10;
    private int stoneRequired = 5;
    private int fiberRequired = 8;
    private float buildTime = 5f;
    private string name = "WoodWall";
    private GameObject blueprint;

    public GameObject getGameObject()
    {
        blueprint = (GameObject)Resources.Load("woodWallBlueprint");
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
