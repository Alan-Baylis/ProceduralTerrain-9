using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {
    public bool snapped = false;
    private bool rotated = false;
    private bool placeable = false;
    public Tile.TileType tileType;
    public GameObject tile;
    public Vector3 cursorPos;

    //added
    private Transform pos;

    private bool wallSlided = false;
    private bool sliding = false;

    // Use this for initialization
    void Start () {
        //if (tileType.Equals(Tile.TileType.WALL) || tileType.Equals(Tile.TileType.FLOOR))
            //placeable = false;
	}
	
	// Update is called once per frame
	void Update () {
        //RaycastHit hit;
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!snapped)           
            this.transform.position = interactionRay.GetPoint(2f);
        
        /*else
        {
            if (tileType.Equals(Tile.TileType.FLOOR))
            {
                this.transform.position = new Vector3(interactionRay.GetPoint(4).x, this.transform.position.y , interactionRay.GetPoint(4).z);              
            }
        }*/
        cursorPos = interactionRay.GetPoint(0.5f);

        /*if ((interactionRay.GetPoint(Camera.main.transform.position.magnitude) - this.transform.position).magnitude > 2f)
        {
            snapped = false;
            if (tileType.Equals(Tile.TileType.FLOOR) || tileType.Equals(Tile.TileType.WALL))
                placeable = false;
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            if (placeable)
            {
                pos = this.transform;
                Destroy(this.gameObject, 0.5f);
                Instantiate(tile, pos.position, pos.rotation);
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            if (snapped)
            {
                snapped = false;
                /*if (tileType.Equals(Tile.TileType.FLOOR) || tileType.Equals(Tile.TileType.WALL) || tileType.Equals(Tile.TileType.FOUNDATION))
                {*/
                    placeable = false;
                    sliding = false;
                //}
            }
        }
        if (Input.GetButtonDown("Rotate"))
        {
            rotate();
        }
    }

    public void snap()
    {
        snapped = true;
        if (tileType.Equals(Tile.TileType.WALL) || tileType.Equals(Tile.TileType.FLOOR) || tileType.Equals(Tile.TileType.FOUNDATION))
            placeable = true;
    }
    public void rotate()
    {
        if (tileType.Equals(Tile.TileType.WALL) || tileType.Equals(Tile.TileType.FLOOR))
        {
            if (!snapped)
            {
                if (!rotated)
                {
                    this.transform.rotation = Quaternion.Euler(0, 90, 0);
                    rotated = true;
                }
                else if (rotated)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    rotated = false;
                }
            }
            else if (tileType.Equals(Tile.TileType.WALL) && sliding)
            {
                if (!rotated)
                {
                    if (wallSlided)
                    {
                        this.transform.position += new Vector3(this.gameObject.GetComponent<BoxCollider>().bounds.size.x, 0, 0);
                        wallSlided = false;
                    }
                    else
                    {
                        this.transform.position -= new Vector3(this.gameObject.GetComponent<BoxCollider>().bounds.size.x, 0, 0);
                        wallSlided = true;
                    }
                    
                }
                else if (rotated)
                {
                    if (wallSlided)
                    {
                        this.transform.position += new Vector3(0, 0, this.gameObject.GetComponent<BoxCollider>().bounds.size.z);
                        wallSlided = false;
                    }
                    else
                    {
                        this.transform.position -= new Vector3(0, 0, this.gameObject.GetComponent<BoxCollider>().bounds.size.z);
                        wallSlided = true;
                    }
                }
            }
        }
    }

    public bool getRotate()
    {
        return rotated;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Tile"))
            if ((!tileType.Equals(Tile.TileType.WALL) && !tileType.Equals(Tile.TileType.FLOOR)))
                placeable = false;
        if (tileType.Equals(Tile.TileType.FOUNDATION))
        {
            if (other.tag.Equals("Terrain"))
            {
                placeable = true;
            }
        }
        if (tileType.Equals(Tile.TileType.MISC))
        {
            if (other.tag.Equals("Terrain") || other.tag.Equals("Tile"))
            {
                placeable = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Tile"))
            if ((!tileType.Equals(Tile.TileType.WALL) && !tileType.Equals(Tile.TileType.FLOOR)))
                placeable = true;
        if (tileType.Equals(Tile.TileType.FOUNDATION))
        {
            if (other.tag.Equals("Terrain"))
            {
                placeable = false;
            }
        }
        if (tileType.Equals(Tile.TileType.MISC))
        {
            if (other.tag.Equals("Terrain") || other.tag.Equals("Tile"))
            {
                placeable = false;
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (tileType.Equals(Tile.TileType.MISC))
        {
            placeable = false;
        }
    }
    public bool isPlaceable()
    {
        return placeable;
    }
    public void enableSliding()
    {
        Debug.Log("Slding enabled");
        sliding = true;
    }
}
