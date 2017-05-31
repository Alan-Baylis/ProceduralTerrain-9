using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class HomeBase : IXmlSerializable {
    public int numOfTiles;
    List<GameObject> homeBaseTiles;
    private GameObject prefab;
    private GameObject current;

    private List<string> homeBaseNames;
    private List<Vector3> homeBasePosition;
    private List<Quaternion> homeBaseRotation;

    ////////////////////////////////
    ///Saving and Loading
    ////////////////////////////////
    public HomeBase()
    {
        homeBaseTiles = new List<GameObject>();
        homeBaseNames = new List<string>();
        homeBasePosition = new List<Vector3>();
        homeBaseRotation = new List<Quaternion>();
    }

    public void reloadBase()
    {
        for (int i = 0; i < homeBaseNames.Count; i++)
        {
            addTile(parseGameObjectString(homeBaseNames[i]), homeBasePosition[i], homeBaseRotation[i].eulerAngles);
        }
    }
    public void storeBase()
    {
        homeBaseTiles = new List<GameObject>();
        foreach (var item in GameObject.FindGameObjectsWithTag("Tile"))
        {
            homeBaseNames.Add(item.name);
            homeBasePosition.Add(item.transform.position);
            homeBaseRotation.Add(item.transform.rotation);
        }
    }
    private void addTile(string prefabName, Vector3 pos, Vector3 rot)
    {
        prefab = (GameObject)Resources.Load(prefabName);
        
        current = GameObject.Instantiate(prefab);
        Debug.Log(current.tag);
        current.transform.position = pos;
        current.transform.rotation = Quaternion.Euler(rot);
        homeBaseTiles.Add(current); 
    }
    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        Vector3 position;
        Vector3 rotation;
        string prefabName;
        PlayerScript playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();

        //Load info here
        reader.ReadToDescendant("Tiles");
        reader.ReadToDescendant("Tile");
        //load all saved tiles
        while (reader.IsStartElement("Tile"))
        {
            prefabName = parseGameObjectString(reader.GetAttribute("Prefab"));
            position = stringToVector(reader.GetAttribute("Position"));
            rotation = stringToVector(reader.GetAttribute("Rotation"));
            addTile(prefabName, position, rotation);
            reader.ReadToNextSibling("Tile");
        }
        //Move to player and set player level, player materials
        reader.ReadToNextSibling("Player");
        playerScript.setLevel(int.Parse(reader.GetAttribute("Level")));
        reader.ReadToDescendant("Inventory");
        playerScript.getInventory().getWood().setAmount(int.Parse(reader.GetAttribute("Wood")));
        playerScript.getInventory().getBark().setAmount(int.Parse(reader.GetAttribute("Bark")));
        playerScript.getInventory().getStone().setAmount(int.Parse(reader.GetAttribute("Stone")));
        playerScript.getInventory().getFiber().setAmount(int.Parse(reader.GetAttribute("Fiber")));
        reader.ReadToDescendant("Buildables");
        reader.ReadToDescendant("Buildable");
        while (reader.IsStartElement("Buildable"))
        {
            for (int i = 0; i < int.Parse(reader.GetAttribute("Amount")); i++)
            {
                playerScript.getInventory().addBuildableString(reader.GetAttribute("Name"));
            }
            reader.ReadToNextSibling("Buildable");
        }
    }

    public void WriteXml(XmlWriter writer)
    {
        //Save info here
        Tile tileScript;
            //Player stuffs
        PlayerScript playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();

        homeBaseTiles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tile"));
        
        //writer.WriteAttributeString("Tiles", numOfTiles.ToString());
        //Write Tiles as tile elements in Tiles
        //Each Tile has its prefab name, position and rotation as attribute
        writer.WriteStartElement("Tiles");
        foreach (GameObject tile in homeBaseTiles)
        {
            tileScript = tile.GetComponent<Tile>();
            writer.WriteStartElement("Tile");
            writer.WriteAttributeString("Prefab", tileScript.gameObject.ToString());
            writer.WriteAttributeString("Position", tileScript.transform.localPosition.ToString());
            writer.WriteAttributeString("Rotation", tileScript.transform.localRotation.eulerAngles.ToString());
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        //Write Player to XML
        //Player has level as attribute
        writer.WriteStartElement("Player");
        writer.WriteAttributeString("Level", playerScript.getLevel().ToString());
        //Write Player Inventory to XML as child of Player
        //Basic Materials as attributes of inventory
        writer.WriteStartElement("Inventory");
        writer.WriteAttributeString("Wood", playerScript.getInventory().getWood().checkAmount().ToString());
        writer.WriteAttributeString("Bark", playerScript.getInventory().getBark().checkAmount().ToString());
        writer.WriteAttributeString("Stone", playerScript.getInventory().getStone().checkAmount().ToString());
        writer.WriteAttributeString("Fiber", playerScript.getInventory().getFiber().checkAmount().ToString());
        //Write a List of buildable objects in inventory
        writer.WriteStartElement("Buildables");
        foreach (KeyValuePair<string, Stack> buildable in playerScript.getInventory().getBuildable())
        {
            writer.WriteStartElement("Buildable");
            writer.WriteAttributeString("Name", buildable.Key);
            writer.WriteAttributeString("Amount", buildable.Value.Count.ToString());
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
        writer.WriteEndElement();
        writer.WriteEndElement();
    }

    //Parsing Vector3 from String
    private Vector3 stringToVector(string sourceString)
    {
        string inString;
        Vector3 outVector;
        string[] splitString;

        inString = sourceString.Substring(1, sourceString.Length-2);

        //split values into an array
        splitString = inString.Split("," [0]);
        //Build new Vector 3
        outVector.x = float.Parse(splitString[0]);
        outVector.y = float.Parse(splitString[1]);
        outVector.z = float.Parse(splitString[2]);

        return outVector;
    }
    //Parse String to Quaternion
    private Quaternion stringToQuaternion(string sourceString)
    {
        string inString;
        Quaternion outQuaternion;
        string[] splitString;

        inString = sourceString.Substring(1, sourceString.Length - 2);

        //split values into an array
        splitString = inString.Split(","[0]);
        //Build new Vector 3
        outQuaternion.w = float.Parse(splitString[0]);
        outQuaternion.x = float.Parse(splitString[1]);
        outQuaternion.y = float.Parse(splitString[2]);
        outQuaternion.z = float.Parse(splitString[3]);
        

        return outQuaternion;
    }
    //Parse XML GameObjectName to prefab name removes .GameObject
    private string parseGameObjectString(string sourceString)
    {
        string outString;

        if (sourceString.Contains("(Clone)"))
        {
            outString = sourceString.Substring(0, sourceString.IndexOf('('));
        }
        else if (sourceString.Contains(" ("))
        {
            outString = sourceString.Substring(0, sourceString.IndexOf(' '));
            if (outString.Contains(" ("))
                outString = outString.Substring(0, outString.IndexOf(' '));
        }
        else
        {
            outString = "none found";
        }

        return outString;
    }
}
