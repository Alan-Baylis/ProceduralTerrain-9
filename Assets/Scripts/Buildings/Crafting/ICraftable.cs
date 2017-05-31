using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICraftable {

    int getRequiredWood();
    int getRequiredBark();
    int getRequiredStone();
    int getRequiredFiber();
    float getbuildTime();
    string getName();
    GameObject getGameObject();
}
