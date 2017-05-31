using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class LevelMap : MonoBehaviour{
    public GameObject player;
    public MeshCollider meshCollider;
    public MeshFilter meshFilter;


    public void LevelOutTerrain(Vector3 position)
    {
        
        Vector3[] vertixArray; 
        Vector3 playerPosition;
        Vector3 targetPosition;
        Mesh mesh;

        mesh = meshFilter.mesh;
        vertixArray = mesh.vertices;
        playerPosition = player.transform.position;
        targetPosition = position;

        int indexX;
        int indexY;

        int finalIndex;

        // Debug.Log(player.transform.position);
        /* Vector3 check = new Vector3(-64f, 120.5f,64f);
         Debug.Log("Index of player position in vertix array?::: " + ArrayUtility.IndexOf<Vector3>(vertixArray, check));
         Debug.Log(vertixArray[0]);*/

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                indexX = ((int)targetPosition.x/5) + x;// dividing by 5 due to mesh being scaled by 5
                //indexX = (indexX + 65) * 5;
                int xDiff = (indexX + 64);
                //Debug.Log("Index of changed X: " + indexX);

                indexY = ((int)targetPosition.z/5) + y;
                int zValue = (int)64 - indexY;//Basically which row if one row has 128 units then rests and y decreases
                //Debug.Log("zValue: " + zValue);
                int yDiff = indexY - 64; //Not needed I think
                //Debug.Log("Index of changed Y: " + indexY);


                finalIndex = (zValue * 128) + xDiff + zValue;
                vertixArray[finalIndex].y = playerPosition.y;
                Debug.Log("Index of changed vertix: " + finalIndex);
                Debug.Log(vertixArray[finalIndex]);
            }
        }


        mesh.vertices = vertixArray;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
}


/*
 * for (int x = 0; x < 5; x++)
        {           
            indexX = (int)playerPosition.x+x;
            //indexX = (indexX + 65) * 5;
            int xDiff = (indexX + 64);
            //Debug.Log("Index of changed X: " + indexX);

            indexY = (int)playerPosition.z;
            int zValue = (int)64-indexY;//Basically which row if one row has 128 units then rests and y decreases
            //Debug.Log("zValue: " + zValue);
            int yDiff = indexY - 64; //Not needed I think
            //Debug.Log("Index of changed Y: " + indexY);


            finalIndex = (zValue * 128)+xDiff+ zValue;
            vertixArray[finalIndex].y = 0;
            Debug.Log("Index of changed vertix: " + finalIndex);
            Debug.Log(vertixArray[finalIndex]);
        }
 */