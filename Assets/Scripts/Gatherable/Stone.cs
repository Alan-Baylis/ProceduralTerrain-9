using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IGatherable {
    private int health;
    private int xp;


    // Use this for initialization
    void Start()
    {
        health = 1;
        xp = health;
    }


    public void harvest()
    {
        Destroy(this.gameObject, 0.1f);
    }
    public int getBark()
    {
        return 0;
    }
    public int getStone()
    {        
        return 1;
    }
    public int getWood()
    {      
        return 0;
    }
    public int getFiber()
    {
        return 0;
    }
    public int getXp()
    {
        return xp;
    }

    public string toString()
    {
        return "Stone";
    } 
}
