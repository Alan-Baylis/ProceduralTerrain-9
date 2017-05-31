using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFoundation : ICraftable {
    private int woodRequired = 5;
    private int barkRequired = 0;
    private int stoneRequired = 35;
    private int fiberRequired = 10;
    private float buildTime = 8f;
    private string name = "Stone Foundation";
    private GameObject gameObject;


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
    public string getName()
    {
        return name;
    }
    public int getRequiredFiber()
    {
        return fiberRequired;
    }
    public GameObject getGameObject()
    {
        return gameObject;
    }
}
