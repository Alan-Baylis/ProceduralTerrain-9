using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;



    public void DrawTexture(Texture2D texture)
    {
        //Texture.material is only rendered in game mode/ runtime so thats why shared marterial
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {              
        Mesh shared = meshData.CreateMesh();
        GameObject levelMesh = GameObject.Find("LevelMesh");

        meshFilter = levelMesh.GetComponent<MeshFilter>();
        meshRenderer = levelMesh.GetComponent<MeshRenderer>();
        meshCollider = levelMesh.GetComponent<MeshCollider>();
        //Using shared to allow gernating outside of gamemode
        meshFilter.sharedMesh = shared;
        meshRenderer.sharedMaterial.mainTexture = texture;
        meshCollider.sharedMesh = shared;
    }
    public void DrawMeshWithSubMesh(MeshData meshData/*, int[] water, int[] sand, int[] earth, int[] forest, int[]mountain, int[] snow*/)
    {
        Material[] materials = new Material[]{
            Resources.Load("Water") as Material,
            Resources.Load("Sand") as Material,
            Resources.Load("Rock") as Material,
            Resources.Load("Forest") as Material,
            Resources.Load("Earth") as Material,
            Resources.Load("Snow") as Material
        };

        //textureRender.materials = materials;
        Mesh shared = meshData.CreateMesh();
        //shared.subMeshCount = 6;

        //Using shared to allow gernating outside of gamemode
        meshFilter.sharedMesh = shared;  
        meshRenderer.sharedMaterials = materials;
        meshCollider.sharedMesh = shared;
        /*Debug.Log(water[0] + ", "  + water[1] + "," + water[2]);
            shared.SetTriangles(water, 0);
            shared.SetTriangles(sand, 1);
            shared.SetTriangles(earth, 2);
            shared.SetTriangles(forest, 3);
            shared.SetTriangles(mountain, 4);
            shared.SetTriangles(snow, 5);*/
    }
}
