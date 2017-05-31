using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGen{

    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMulitplier, AnimationCurve heightCurve)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;



        MeshData meshData = new MeshData(heightMap, width, height);
        int vertexIndex = 0;


        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(Mathf.InverseLerp(0, 20, heightMap[x, y])) * heightMulitplier, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float) width,y / (float) height);

                if(x<width-1 && y < height - 1)
                {
                    meshData.addTriangle(vertexIndex, vertexIndex+width+1, vertexIndex+width);
                    meshData.addTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    //Two triangles per square
    public int[] triangles;
    //UV give the position of each vertices in relation to the rest of the map in % 0-1
    public Vector2[] uvs;

    //Multi Texture
    public List<int> water = new List<int>();
    public List<int> sand = new List<int>();
    public List<int> earth = new List<int>();
    public List<int> Grass = new List<int>();
    public List<int> mountain = new List<int>();
    public List<int> snow = new List<int>();
    private float[,] heightMap;


    int triangleIndex;


    public MeshData(float[,] heightMap, int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
        this.heightMap = heightMap;
     }

    public void addTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();       
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        //Use textures for procedural generated mesh requires additional small change to
        /*mesh.subMeshCount = 6;
        setTextureTriangles(this);
        mesh.SetTriangles(water, 0);
        mesh.SetTriangles(sand, 1);
        mesh.SetTriangles(earth, 2);
        mesh.SetTriangles(Grass, 3);
        mesh.SetTriangles(mountain, 4);
        mesh.SetTriangles(snow, 5);*/
        mesh.uv = uvs;
        //For Shadows to work recalculate normals
        mesh.RecalculateNormals();
        return mesh;
    }

    public void setTextureTriangles(MeshData meshData)
    {
        Vector3[] verticies = meshData.vertices;
        int waterPos = 0;
        int sandPos = 0;
        int earthPos = 0;
        int forestPos = 0;
        int mountainPos = 0;
        int snowPos = 0;
        int vertexIndex = 0;
        int height = 129;
        int width = 129;
        int subMeshCount = 0;
        TerrainType[] regions = GameObject.Find("WorldController").GetComponent<MapGen>().regions;


        ///////
        //Another try
        ///////
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {              
                if (x < width - 1 && y < height - 1)
                {
                    float currentHeight = Mathf.InverseLerp(0, 20, (heightMap[x, y] + heightMap[x, y + 1] + heightMap[x + 1, y + 1]) / 3);
                    for (int i = 0; i < regions.Length; i++)
                    {
                        if (currentHeight <= regions[i].height)
                        {
                            switch (regions[i].name)
                            {
                                case "Water":
                                    water.Add(vertexIndex);
                                    water.Add(vertexIndex + width + 1);
                                    water.Add(vertexIndex + width);
                                    break;
                                case "Sand":
                                    sand.Add(vertexIndex);
                                    sand.Add(vertexIndex + width + 1);
                                    sand.Add(vertexIndex + width);
                                    break;
                                case "Earth":
                                    earth.Add(vertexIndex);
                                    earth.Add(vertexIndex + width + 1);
                                    earth.Add(vertexIndex + width);
                                    break;
                                case "Grass":
                                    Grass.Add(vertexIndex);
                                    Grass.Add(vertexIndex  + width + 1);
                                    Grass.Add(vertexIndex + width);
                                    break;
                                case "Mountain":
                                    mountain.Add(vertexIndex);
                                    mountain.Add(vertexIndex + width + 1);
                                    mountain.Add(vertexIndex + width);
                                    break;
                                case "Snow":
                                    snow.Add(vertexIndex);
                                    snow.Add(vertexIndex + width + 1);
                                    snow.Add(vertexIndex + width);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    }
                    currentHeight = Mathf.InverseLerp(0, 20, (heightMap[x+1, y+1] + heightMap[x, y] + heightMap[x + 1, y]) / 3);
                    for (int i = 0; i < regions.Length; i++)
                    {
                        if (currentHeight <= regions[i].height)
                        {
                            switch (regions[i].name)
                            {
                                case "Water":
                                    water.Add(vertexIndex + width + 1);
                                    water.Add(vertexIndex);
                                    water.Add(vertexIndex + 1);
                                    break;
                                case "Sand":
                                    sand.Add(vertexIndex + width + 1);
                                    sand.Add(vertexIndex );
                                    sand.Add(vertexIndex + 1);
                                    break;
                                case "Earth":
                                    earth.Add(vertexIndex + width + 1);
                                    earth.Add(vertexIndex );
                                    earth.Add(vertexIndex + 1);
                                    break;
                                case "Grass":
                                    Grass.Add(vertexIndex + width + 1);
                                    Grass.Add(vertexIndex);
                                    Grass.Add(vertexIndex + 1);
                                    break;
                                case "Mountain":
                                    mountain.Add(vertexIndex + width + 1);
                                    mountain.Add(vertexIndex);
                                    mountain.Add(vertexIndex + 1);
                                    break;
                                case "Snow":
                                    snow.Add(vertexIndex + width + 1);
                                    snow.Add(vertexIndex);
                                    snow.Add(vertexIndex + 1);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    }
                }
                vertexIndex++;  
            }
        }
      
        if (water.Count>0)
        {
            Debug.Log("water" + water.Count);
            subMeshCount++;
        }
        if (sand.Count > 0)
        {
            Debug.Log("sand" + sand.Count);
            subMeshCount++;
        }
        if (earth.Count > 0)
        {
            Debug.Log("earth" + earth.Count);
            subMeshCount++;
        }
        if (Grass.Count > 0)
        {
            Debug.Log("Grass" + Grass.Count);
            subMeshCount++;
        }
        if (mountain.Count > 0)
        {
            Debug.Log("mountina" + mountain.Count);
            subMeshCount++;
        }
        if (snow.Count > 0)
        {
            Debug.Log("snow" + snow.Count);
            subMeshCount++;
        }
    }
}
