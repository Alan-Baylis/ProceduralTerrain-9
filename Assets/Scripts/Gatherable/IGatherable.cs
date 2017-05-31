using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGatherable
{
    int getWood();
    int getBark();
    int getStone();
    int getFiber();
    int getXp();
    void harvest();
}
