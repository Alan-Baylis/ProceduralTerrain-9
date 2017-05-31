using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public TileType tileType;
    public enum TileType { WALL, FOUNDATION, FLOOR, MISC }
    public Vector3 position;
    public Quaternion rotation;
    public string tileName;
    public GameObject prefab;

    public Tile()
    {

    }
    private void Start()
    {
        position = this.transform.position;
        rotation = this.transform.rotation;
        tileName = this.name;
        prefab = this.gameObject;
    }
}
