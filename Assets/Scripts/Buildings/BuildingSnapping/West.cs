using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class West : MonoBehaviour {
    GameObject parentObject;
    GameObject otherObject;
    Tile tileScript;
    FollowMouse followScript;

    void Start()
    {
        parentObject = this.transform.parent.gameObject;
        tileScript = parentObject.GetComponent<Tile>();
    }
    void OnTriggerEnter(Collider other)
    {
        /*Debug.Log("coliding with:" + other.gameObject.tag);
        Debug.Log("bounds: " + other.bounds);
        Debug.Log("Extends: " + other.bounds.extents);
        Debug.Log("Max: " + other.bounds.max);
        Debug.Log("Min: " + other.bounds.min);*/
        //Vector3 temp = other.transform.position;

        if (other.tag.Equals("Blueprint"))
        {

            otherObject = other.gameObject;
            followScript = otherObject.GetComponent<FollowMouse>();
            if (tileScript.tileType.Equals(Tile.TileType.FOUNDATION))
            {
                if (followScript.tileType.Equals(Tile.TileType.FOUNDATION))
                {
                    placementFoundationToFoundation(otherObject, followScript);
                }
                else if (followScript.tileType.Equals(Tile.TileType.WALL) && followScript.getRotate())
                {
                    placementWallToFoundation(otherObject, followScript, other);
                }
            }
            else if (tileScript.tileType.Equals(Tile.TileType.WALL))
            {
                if (followScript.tileType.Equals(Tile.TileType.WALL))
                    placementWalltoWall(otherObject, followScript, other);
            }
            else if (tileScript.tileType.Equals(Tile.TileType.FLOOR))
            {
                if (followScript.tileType.Equals(Tile.TileType.FLOOR))
                    placementFoundationToFoundation(otherObject, followScript);
            }
        }
    }
    private void placementWalltoWall(GameObject otherObject, FollowMouse followScript, Collider otherCol)
    {
        if (this.transform.rotation == otherCol.transform.rotation)
        {
            if (!followScript.getRotate())
            {
                followScript.snap();
                otherObject.transform.position = parentObject.transform.position - new Vector3(parentObject.transform.GetComponent<BoxCollider>().bounds.size.x, 0, 0);
            }
            else if (followScript.getRotate())
            {
                followScript.snap();
                otherObject.transform.position = parentObject.transform.position + new Vector3(0, 0, parentObject.transform.GetComponent<BoxCollider>().bounds.size.z);
            }
        }
        else
        {
            followScript.enableSliding();
            if (!followScript.getRotate())
            {
                followScript.snap();
                otherObject.transform.position = parentObject.transform.position + new Vector3(otherCol.transform.GetComponent<BoxCollider>().bounds.extents.x - otherCol.transform.GetComponent<BoxCollider>().bounds.extents.z, 0, parentObject.transform.GetComponent<BoxCollider>().bounds.extents.z); ;
            }
            else if (followScript.getRotate())
            {
                followScript.snap();
                otherObject.transform.position = parentObject.transform.position - new Vector3(parentObject.transform.GetComponent<BoxCollider>().bounds.extents.x, 0, -(otherCol.transform.GetComponent<BoxCollider>().bounds.extents.z - otherCol.transform.GetComponent<BoxCollider>().bounds.extents.x));
            }
        }
    }
    private void placementFoundationToFoundation(GameObject otherObject, FollowMouse followScript)
    {
        followScript.snap();
        otherObject.transform.position = parentObject.transform.position - new Vector3(parentObject.transform.GetComponent<BoxCollider>().bounds.size.x, 0, 0);
    }
    private void placementWallToFoundation(GameObject otherObject, FollowMouse followScript, Collider otherCol)
    {
        followScript.snap();
        otherObject.transform.position = parentObject.transform.position - new Vector3(parentObject.transform.GetComponent<BoxCollider>().bounds.extents.x - otherObject.transform.GetComponent<BoxCollider>().bounds.extents.x, -(parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y + otherObject.transform.GetComponent<BoxCollider>().bounds.extents.y), 0);
    }
}
