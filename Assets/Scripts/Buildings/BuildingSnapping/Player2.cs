using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public GameObject tile1Blueprint;
    public GameObject tile2Blueprint;
    public GameObject tile3Blueprint;
    public GameObject spawn;
    public GameObject holding;

    private Transform pos;

    bool hold;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hold == true)
            {
                Destroy(holding);
            }
            holding = Instantiate(tile1Blueprint, spawn.transform.position, Quaternion.identity);
            hold = true;
        }
        else if (Input.GetMouseButtonDown(2))
        {
            if (hold == true)
            {
                Destroy(holding);
            }
            holding = Instantiate(tile2Blueprint, spawn.transform.position, Quaternion.identity);
            hold = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            FollowMouse followMouse = holding.GetComponent<FollowMouse>();
            if (followMouse.isPlaceable())
            {
                pos = holding.transform;
                Destroy(holding);
                hold = false;
                holding = null;
                Instantiate(followMouse.tile, pos.position, pos.rotation);
            }
        }
        if (Input.GetButtonDown("Rotate"))
        {
            holding.GetComponent<FollowMouse>().rotate();
        }
        if (Input.GetButtonDown("Floor"))
        {
            if (hold == true)
            {
                Destroy(holding);
            }
            holding = Instantiate(tile3Blueprint, spawn.transform.position, Quaternion.identity);
            hold = true;
        }

    }
}
