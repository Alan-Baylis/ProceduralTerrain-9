using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGen : MonoBehaviour {
    public int mapWidth;
    public int mapHeight;

    public TerrainType[] regions;

    public float meshHeightMulitplier;

    /*public float a;
    public float b;
    public float c;
    public float d*/

    public AnimationCurve heightCurve;

    //Gameobjects to spawn and their parent gameobject
    public Transform parentObject;
    private GameObject currentInstance;
    public GameObject deadGrass;
    public GameObject[] grasses;
    public GameObject[] trees;
    public GameObject[] treesMountain;
    public GameObject[] beachTrees;
    public GameObject[] stones;
    public GameObject[] bushes;
    public GameObject[] boulders;
    public GameObject[] bouldersMountain;


    //Multi Texture
    public List<int> water;
    public List<int> sand;
    public List<int> earth;
    public List<int> forest;
    public List<int> mountain;
    public List<int> snow;
    public int[] triangles;
    public Material[] materials;



    //Test to enforce condition on map like num of trees
    private int minNumOfTree = 200;
    private int numOfTrees = 0;
    private int minNumOfStones = 50;
    private int numOfStones;

    public void GenerateMap()
    {
        numOfTrees = 0;
        numOfStones = 0;
        parentObject = GameObject.FindGameObjectWithTag("Objects").transform;

        //SceneManager.LoadScene("LevelGenScrene");
        //EditorSceneManager.LoadScene("LevelGenScrene");
        GameObject cleanup;
        while (cleanup = GameObject.FindWithTag("Tree"))
        {
            Object.DestroyImmediate(cleanup);
        }
        while (cleanup = GameObject.FindWithTag("Stone"))
        {
            Object.DestroyImmediate(cleanup);
        }
        while (cleanup = GameObject.FindWithTag("Bush"))
        {
            Object.DestroyImmediate(cleanup);
        }
        while (cleanup = GameObject.FindWithTag("Boulder"))
        {
            Object.DestroyImmediate(cleanup);
        }
        while (cleanup = GameObject.FindWithTag("Grass"))
        {
            Object.DestroyImmediate(cleanup);
        }

        float a = Random.Range(5f, 20f);
        float b = Random.Range(0f, 20f);
        float c = Random.Range(5f, 20f);
        float d = Random.Range(0f, 20f);

        float[,] noiseMap = NoiseMapGen.GenerateNoiseMap(mapWidth, mapHeight, a, b, c, d);
        MeshData meshData = MeshGen.GenerateTerrainMesh(noiseMap, meshHeightMulitplier, heightCurve);
        triangles = meshData.triangles;
        
        Color[] colorMap = new Color[mapWidth * mapHeight];

        for(int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = Mathf.InverseLerp(0, 20, noiseMap[x, y]);
                for(int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        //New code to try and get lerp from one zone to another
                        //Skipping earth into grass transition
                        if (regions[i].name.Equals("Earth"))
                        {
                            float tempMaxHeightOfRegion = regions[i].height;
                            float tempMinHeightOfRegion = regions[i - 1].height;
                            float tempMidHeightOfRegion = regions[i - 1].height + ((regions[i].height - regions[i - 1].height) / 2);

                            if (currentHeight > tempMidHeightOfRegion)
                            {
                                colorMap[y * mapWidth + x] = Color.Lerp(regions[i].color, regions[i + 1].color, Mathf.InverseLerp(tempMidHeightOfRegion, tempMaxHeightOfRegion, currentHeight));
                            }
                            else
                            {
                                colorMap[y * mapWidth + x] = regions[i].color;
                            }


                        }
                        else if (i > 0 && i < regions.Length - 1)
                        {
                            float tempMaxHeightOfRegion = regions[i].height;
                            float tempMinHeightOfRegion = regions[i - 1].height;
                            float tempMidHeightOfRegion = regions[i - 1].height + ((regions[i].height - regions[i - 1].height) / 2);
                            /*if (currentHeight >= tempMinHeightOfRegion)
                                colorMap[y * mapWidth + x] = Color.Lerp(regions[i].color, regions[i + 1].color, /*(float) noiseMap[x, y]/10 Mathf.InverseLerp(tempMidHeightOfRegion, tempMaxHeightOfRegion, currentHeight));
                            else if (currentHeight < tempMinHeightOfRegion)
                            {
                                colorMap[y * mapWidth + x] = Color.Lerp(regions[i].color, regions[i - 1].color, /*(float) noiseMap[x, y]/10 Mathf.InverseLerp(tempMinHeightOfRegion, tempMidHeightOfRegion, currentHeight));
                            }*/
                            //colorMap[y * mapWidth + x] = Color.Lerp(regions[i].color, regions[i + 1].color, /*(float) noiseMap[x, y]/10*/ Mathf.InverseLerp(tempMinHeightOfRegion, tempMaxHeightOfRegion, currentHeight) - 0.1f);
                            if (currentHeight > tempMidHeightOfRegion)
                            {
                                colorMap[y * mapWidth + x] = Color.Lerp(regions[i].color, regions[i + 1].color, Mathf.InverseLerp(tempMidHeightOfRegion, tempMaxHeightOfRegion, currentHeight));
                            }
                            else
                            {
                                colorMap[y * mapWidth + x] = regions[i].color;
                            }
                        }
                        else if (i == 0)
                        {
                            /*float tempMaxHeightOfRegion = regions[i].height;
                            float tempMinHeightOfRegion = 0;
                            float tempMidHeightOfRegion = 0 + ((regions[i].height - 0) / 2);
                            if(currentHeight > tempMidHeightOfRegion)
                                colorMap[y * mapWidth + x] = Color.Lerp(regions[i].color, regions[i + 1].color, /*(float) noiseMap[x, y]/10 Mathf.InverseLerp(tempMidHeightOfRegion, tempMaxHeightOfRegion, currentHeight));
                            else*/
                            colorMap[y * mapWidth + x] = regions[i].color;

                        }
                        else
                        {
                            /*float tempMaxHeightOfRegion = regions[i].height;
                            float tempMinHeightOfRegion = regions[i - 1].height;
                            float tempMidHeightOfRegion = regions[i - 1].height + ((regions[i].height - regions[i - 1].height) / 2);
                            colorMap[y * mapWidth + x] = Color.Lerp(regions[i].color, regions[i - 1].color, /*(float) noiseMap[x, y]/10 Mathf.InverseLerp(tempMinHeightOfRegion, tempMaxHeightOfRegion, currentHeight));
                            */
                            colorMap[y * mapWidth + x] = regions[i].color;
                        }
                        //OG code for coolour depending on height
                        //colorMap[y * mapWidth + x] = regions[i].color;
                        //place Gameobjects
                        //trees/bushes on grassy area
                        if (regions[i].name.Equals("Grass"))
                        {
                            if (Random.Range(0f, 1f) > 0.9f)
                            {
                                Vector3 treePos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(trees[(int)Random.Range(0, trees.Length)], treePos, Quaternion.identity);
                                numOfTrees++;
                                currentInstance.transform.parent = parentObject;
                            }
                            else if (Random.Range(0f, 1f) > 0.8f)
                            {
                                Vector3 bushPos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(bushes[(int)Random.Range(0, bushes.Length)], bushPos, Quaternion.identity);
                                currentInstance.transform.parent = parentObject;
                            }
                            if (Random.Range(0f, 1f) > 0.75f)
                            {
                                Vector3 grassPos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(grasses[(int)Random.Range(0, grasses.Length)], grassPos, Quaternion.identity);
                                currentInstance.transform.parent = parentObject;
                            }
                        }
                        //Stones on Sand
                        else if (regions[i].name.Equals("Sand"))
                        {
                            if (Random.Range(0f, 1f) > 0.9f)
                            {
                                Vector3 stonePos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(stones[(int)Random.Range(0, stones.Length)], stonePos, Quaternion.identity);
                                numOfStones++;
                                currentInstance.transform.parent = parentObject;
                            }
                        }
                        else if (regions[i].name.Equals("Earth"))
                        {
                            if (Random.Range(0f, 1f) > 0.96f)
                            {
                                Vector3 treePos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(beachTrees[(int)Random.Range(0, beachTrees.Length)], treePos + new Vector3(0, 3.5f, 0), Quaternion.Euler(-90, 0, 0));
                                currentInstance.transform.parent = parentObject;
                            }
                            else if (Random.Range(0f, 1f) > 0.9f)
                            {
                                Vector3 stonePos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(stones[(int)Random.Range(0, stones.Length)], stonePos, Quaternion.identity);
                                currentInstance.transform.parent = parentObject;
                            }
                            else if (Random.Range(0f, 1f) > 0.90f)
                            {
                                Vector3 grassPos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(deadGrass, grassPos + new Vector3(0,0.3f,0), Quaternion.identity);
                                currentInstance.transform.parent = parentObject;
                            }
                        }
                        else if (regions[i].name.Equals("Mountain"))
                        {
                            if (Random.Range(0f, 1f) > 0.95f)
                            {
                                Vector3 boulderPos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(bouldersMountain[(int)Random.Range(0, bouldersMountain.Length)], boulderPos, Quaternion.identity);
                                currentInstance.transform.localScale = new Vector3(Random.Range(8, 10), Random.Range(8, 10), Random.Range(8, 10));
                                currentInstance.transform.parent = parentObject;
                            }
                            else if (Random.Range(0f, 1f) > 0.99f)
                            {
                                float temp = Random.Range(5, 10);
                                Vector3 boulderPos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(treesMountain[(int)Random.Range(0, treesMountain.Length)], boulderPos, Quaternion.identity);
                                currentInstance.transform.localScale = new Vector3(temp, temp, temp);
                                currentInstance.transform.parent = parentObject;
                            }

                        }
                        else if (regions[i].name.Equals("Snow"))
                        {
                            if (Random.Range(0f, 1f) > 0.9f)
                            {
                                Vector3 boulderPos = Vector3.Scale(meshData.vertices[y * mapWidth + x], new Vector3(5, 1, 5));
                                currentInstance = Instantiate(boulders[(int)Random.Range(0, boulders.Length)], boulderPos, Quaternion.identity);
                                currentInstance.transform.parent = parentObject;
                            }
                        }
                        break;
                    }
                }
            }
        }
        //Force conditions
        if (numOfTrees < minNumOfTree || numOfStones < minNumOfStones)
        {
            Debug.Log("Not Enough");
            GenerateMap();
        }
        else
        {
            Debug.Log("Enough");
            MapDisplay display = FindObjectOfType<MapDisplay>();
            //display.DrawTexture(TextureGen.TextureFromColorMap(colorMap, mapWidth, mapHeight));

            //Select render type just 2d colour or 3d mesh        
            display.DrawMesh(meshData, TextureGen.TextureFromColorMap(colorMap, mapWidth, mapHeight));

            //Texture try
            //setTextureTriangles(meshData);
            //display.DrawMeshWithSubMesh(meshData/*, water.ToArray(), sand.ToArray(), earth.ToArray(), forest.ToArray(), mountain.ToArray(), snow.ToArray()*/);
        }
    }
    //For texture sub
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

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x < width - 1 && y < height - 1)
                {
                    for (int j = 0; j < regions.Length; j++)
                    {
                        if ((Mathf.InverseLerp(0, 20, (verticies[vertexIndex].y + verticies[vertexIndex + width + 1].y + verticies[vertexIndex + width].y) / 3)) > regions[j].height)
                        {
                            switch (regions[j].name)
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
                                case "Forest":
                                    forest.Add(vertexIndex);
                                    forest.Add(vertexIndex + width + 1);
                                    forest.Add(vertexIndex + width);
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
                        }
                        if ((Mathf.InverseLerp(0, 20, (verticies[vertexIndex + width + 1].y + verticies[vertexIndex].y + verticies[vertexIndex + 1].y) / 3)) > regions[j].height)
                        {
                            switch (regions[j].name)
                            {
                                case "Water":
                                    water.Add(vertexIndex + width + 1);
                                    water.Add(vertexIndex);
                                    water.Add(vertexIndex + 1);
                                    break;
                                case "Sand":
                                    sand.Add(vertexIndex + width + 1);
                                    sand.Add(vertexIndex);
                                    sand.Add(vertexIndex + 1);
                                    break;
                                case "Earth":
                                    earth.Add(vertexIndex + width + 1);
                                    earth.Add(vertexIndex);
                                    earth.Add(vertexIndex + 1);
                                    break;
                                case "Forest":
                                    forest.Add(vertexIndex + width + 1);
                                    forest.Add(vertexIndex);
                                    forest.Add(vertexIndex + 1);
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
                        }                        
                    }                    
                }
                vertexIndex++;
            }
        }
    }
}


[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
