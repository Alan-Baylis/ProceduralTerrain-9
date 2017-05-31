using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class South : MonoBehaviour {
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
        /*if (other.tag.Equals("Tile"))
        {
            if (tileScript.tileType.Equals(Tile.TileType.WALL) && other.GetComponent<Tile>().tileType.Equals(Tile.TileType.FOUNDATION))
                Destroy(this.gameObject);
            if (tileScript.tileType.Equals(Tile.TileType.FOUNDATION) || tileScript.tileType.Equals(Tile.TileType.FLOOR))
                Destroy(this.gameObject);
        }*/

        if (other.tag.Equals("Blueprint") && this.transform.rotation == other.transform.rotation)
        {
            otherObject = other.gameObject;
            followScript = otherObject.GetComponent<FollowMouse>();

            if (tileScript.tileType.Equals(Tile.TileType.FOUNDATION))
            {
                if (followScript.tileType.Equals(Tile.TileType.FOUNDATION))
                {
                    placementFoundationToFoundation(otherObject, followScript);
                }
                else if (followScript.tileType.Equals(Tile.TileType.WALL) && !followScript.getRotate())
                {
                    placementWallToFoundation(otherObject, followScript, other);
                }
            }
            else if (tileScript.tileType.Equals(Tile.TileType.WALL))
            {
                if (followScript.tileType.Equals(Tile.TileType.FOUNDATION))
                    return;
                else if(followScript.tileType.Equals(Tile.TileType.FLOOR))
                    placementFloorToWall(otherObject, followScript, other);
                else if(followScript.tileType.Equals(Tile.TileType.WALL))
                    placementWallToWall(otherObject, followScript, other);
            }
            else if (tileScript.tileType.Equals(Tile.TileType.FLOOR))
            {
                if (followScript.tileType.Equals(Tile.TileType.FLOOR))
                    placementFoundationToFoundation(otherObject, followScript);
            }
        }
    }
    //Enum and depending on enum snap diffrent OnTriggerEnter
    private void placementWallToWall(GameObject otherObject, FollowMouse followScript, Collider otherCol)
    {
        if (this.transform.rotation == otherCol.transform.rotation)
        {
            otherObject.transform.position = parentObject.transform.position - new Vector3(0, parentObject.transform.GetComponent<BoxCollider>().bounds.size.y, 0);
            followScript.snap();
        }
    }
    private void placementFoundationToFoundation(GameObject otherObject, FollowMouse followScript)
    {
        followScript.snap();
        otherObject.transform.position = parentObject.transform.position - new Vector3(0, 0, parentObject.transform.GetComponent<BoxCollider>().bounds.size.z);
    }
    private void placementWallToFoundation(GameObject otherObject, FollowMouse followScript, Collider otherCol)
    {
        followScript.snap();
        otherObject.transform.position = parentObject.transform.position - new Vector3(0, -(parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y + otherObject.transform.GetComponent<BoxCollider>().bounds.extents.y), parentObject.transform.GetComponent<BoxCollider>().bounds.extents.z - otherObject.transform.GetComponent<BoxCollider>().bounds.extents.z);
    }
    private void placementFloorToWall(GameObject otherObject, FollowMouse followScript, Collider otherCol)
    {
        followScript.snap();
        otherObject.transform.position = parentObject.transform.position - new Vector3( 0, 0, parentObject.transform.GetComponent<BoxCollider>().bounds.size.z);
    }
}
