using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class North : MonoBehaviour {

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

       /* if (other.tag.Equals("Tile"))
        {
            if (tileScript.tileType.Equals(Tile.TileType.FOUNDATION))
                Destroy(this.gameObject);
        }*/

        if (other.tag.Equals("Blueprint"))
        {
            otherObject = other.gameObject;
            followScript = otherObject.GetComponent<FollowMouse>();
            if (!followScript.snapped)
            {
                if (tileScript.tileType.Equals(Tile.TileType.FOUNDATION))
                {
                    if (followScript.tileType.Equals(Tile.TileType.FOUNDATION))
                    {
                        placementFoundationToFoundation(otherObject, followScript);
                    }
                    else if (followScript.tileType.Equals(Tile.TileType.WALL) && !followScript.getRotate())
                    {
                        Debug.Log("Wall on foundation");
                        placementWallToFoundation(otherObject, followScript, other);
                    }
                }
                else if (tileScript.tileType.Equals(Tile.TileType.WALL))
                {
                    if (followScript.tileType.Equals(Tile.TileType.FOUNDATION))
                        return;
                    else if (followScript.tileType.Equals(Tile.TileType.FLOOR))
                        placementFloorToWall(otherObject, followScript, other);
                    else if (followScript.tileType.Equals(Tile.TileType.WALL))
                        placementWallToWall(otherObject, followScript, other);
                }
                else if (tileScript.tileType.Equals(Tile.TileType.FLOOR))
                {
                    if (followScript.tileType.Equals(Tile.TileType.FLOOR))
                        //otherObject.transform.position = parentObject.transform.position + new Vector3(parentObject.transform.GetComponent<BoxCollider>().bounds.size.x, 0, 0);
                        placementFoundationToFoundation(otherObject, followScript);
                }
            }
        }
    }
    private void placementWallToWall(GameObject otherObject, FollowMouse followScript, Collider otherCol)
    {
        if (this.transform.rotation == otherCol.transform.rotation)
        {
            followScript.snap();
            otherObject.transform.position = parentObject.transform.position + new Vector3(0, parentObject.transform.GetComponent<BoxCollider>().bounds.size.y, 0);
        }
    }
    private void placementFoundationToFoundation(GameObject otherObject, FollowMouse followScript)
    {
        followScript.snap();
        otherObject.transform.position = parentObject.transform.position + new Vector3(0, 0, parentObject.transform.GetComponent<BoxCollider>().bounds.size.z);
    }
    private void placementWallToFoundation(GameObject otherObject, FollowMouse followScript, Collider otherCol)
    {
        followScript.snap();
        otherObject.transform.position = parentObject.transform.position + new Vector3(0, parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y + otherObject.transform.GetComponent<BoxCollider>().bounds.extents.y, parentObject.transform.GetComponent<BoxCollider>().bounds.extents.z - otherObject.transform.GetComponent<BoxCollider>().bounds.extents.z);
    }
    private void placementFloorToWall(GameObject otherObject, FollowMouse followScript, Collider otherCol)
    {
        followScript.snap();
        //otherObject.transform.position = parentObject.transform.position + new Vector3(0, parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y, otherObject.transform.GetComponent<BoxCollider>().bounds.extents.z - parentObject.transform.GetComponent<BoxCollider>().bounds.extents.z);
        //Debug.Log(followScript.isPlaceable());

        float xPos = followScript.cursorPos.x;
        float zPos = followScript.cursorPos.z;
        Vector3 cursorPos = followScript.cursorPos;
        Vector3 temp = this.transform.position - cursorPos;
        Vector3 temp2 = this.GetComponent<BoxCollider>().bounds.center;
        //Debug.Log("X: " + xPos);
        //Debug.Log("Z: " + zPos);
        //Debug.Log("X: " + Mathf.Abs(xPos));
        //Debug.Log(this.transform.rotation == new Quaternion(0.0f, 0.7f, 0.0f, 0.7f));
        float angle = Quaternion.Angle(this.transform.rotation, new Quaternion(0.0f, 0.7f, 0.0f, 0.7f));
        //Debug.Log(this.transform.rotation.Equals(new Quaternion(0.0f, 0.7f, 0.0f, 0.7f)) || this.transform.rotation == new Quaternion(0.0f, 0.7f, 0.0f, 0.7f));
        if (angle < 90f) { 
            if (xPos > temp2.x + 0.5f)
            {
                otherObject.transform.position = parentObject.transform.position + new Vector3(otherObject.transform.GetComponent<BoxCollider>().bounds.extents.x - parentObject.transform.GetComponent<BoxCollider>().bounds.extents.x, parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y - otherObject.transform.GetComponent<BoxCollider>().bounds.extents.y, 0);
            }
            else if (xPos < temp2.x - 0.5f)
            {
                otherObject.transform.position = parentObject.transform.position + new Vector3(-(otherObject.transform.GetComponent<BoxCollider>().bounds.extents.x - parentObject.transform.GetComponent<BoxCollider>().bounds.extents.x), parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y - otherObject.transform.GetComponent<BoxCollider>().bounds.extents.y, 0);
            }
            else
            {
                otherObject.transform.position = parentObject.transform.position + new Vector3(0, parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y - otherObject.transform.GetComponent<BoxCollider>().bounds.extents.y, 0);
            }
        }
        else
        {
            if (zPos > temp2.z + 0.5f)
            {
                otherObject.transform.position = parentObject.transform.position + new Vector3(0, parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y - otherObject.transform.GetComponent<BoxCollider>().bounds.extents.y, (otherObject.transform.GetComponent<BoxCollider>().bounds.extents.z - parentObject.transform.GetComponent<BoxCollider>().bounds.extents.z));
            }
            else if (zPos < temp2.z - 0.5f)
            {
                otherObject.transform.position = parentObject.transform.position + new Vector3(0, parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y - otherObject.transform.GetComponent<BoxCollider>().bounds.extents.y, -(otherObject.transform.GetComponent<BoxCollider>().bounds.extents.z  - parentObject.transform.GetComponent<BoxCollider>().bounds.extents.z));
            }
            else
            {
                otherObject.transform.position = parentObject.transform.position + new Vector3(0, parentObject.transform.GetComponent<BoxCollider>().bounds.extents.y - otherObject.transform.GetComponent<BoxCollider>().bounds.extents.y, 0);
            }
        }
    }
}
