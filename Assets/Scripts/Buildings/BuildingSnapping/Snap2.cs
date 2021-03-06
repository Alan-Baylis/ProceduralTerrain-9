﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap2 : MonoBehaviour {
    private bool snapped = false;
    private bool placeable = true;
    private bool rotated = false;

    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.GetComponentInParent<Transform>().gameObject;
        Debug.Log(otherObject.tag);
        /*Debug.Log("coliding with:" + other.gameObject.tag);
        Debug.Log("bounds: " + other.bounds);
        Debug.Log("Extends: " + other.bounds.extents);
        Debug.Log("Max: " + other.bounds.max);
        Debug.Log("Min: " + other.bounds.min);*/
        if (!snapped && placeable && !other.gameObject.tag.Equals("Tile"))
        {
            //Vector3 temp = other.transform.position;
            GameObject temp = other.transform.parent.gameObject;
            Vector3 distance = other.transform.position - this.transform.position;
            //Debug.Log("coliding with:" + other.gameObject.tag);
            //Debug.Log("distance: " + distance);

            if (other.tag.Equals("ColiderWest"))
            {
                //Debug.Log("ColiderWest");
                //this.transform.position = other.transform.position;
                //may us vector math for dtsance then norm then * length

                //this.transform.Translate(distance);
                //this.transform.position = Vector3.MoveTowards(this.transform.position ,temp, 10);
                //this.transform.rotation = other.transform.rotation;
                //this.transform.position = new Vector3(temp.x - 0.8f, temp.y, temp.z);
                //Debug.Log("Tag: " + temp.tag);
                //Debug.Log("Size: " + temp.name);
                //this.transform.position = temp.transform.position - new Vector3(temp.GetComponent<BoxCollider>().bounds.size.x, 0, 0);
                otherObject.transform.position = this.transform.position - new Vector3(this.GetComponent<BoxCollider>().bounds.size.x, 0, 0);
                //this.transform.rotation = other.transform.rotation;
                snapped = true;
            }
            else if (other.tag.Equals("ColiderEast"))
            {
                //Debug.Log("ColiderEast");
                //this.transform.position = other.transform.position;

                //this.transform.Translate(distance);
                //this.transform.position = Vector3.MoveTowards(this.transform.position, temp, 100 * Time.deltaTime);
                //this.transform.rotation = other.transform.rotation;
                //this.transform.position = new Vector3(temp.x + 0.8f, temp.y, temp.z);
                //this.transform.position = temp.transform.position + new Vector3(temp.GetComponent<BoxCollider>().bounds.size.x, 0, 0);

                otherObject.transform.position = this.transform.position + new Vector3(this.GetComponent<BoxCollider>().bounds.size.x, 0, 0);
                snapped = true;
            }
            else if (other.tag.Equals("ColiderNorth"))
            {
                //this.transform.position = other.transform.position;
                //Debug.Log("ColiderNorth");
                //this.transform.position = new Vector3(temp.x, temp.y + 0.8f, temp.z);
                //this.transform.rotation = other.transform.rotation;
                snapped = true;
            }
            else if (other.tag.Equals("ColiderSouth"))
            {
                //this.transform.position = other.transform.position;
                //this.transform.position = new Vector3(temp.x, temp.y - 0.8f, temp.z);
                //this.transform.rotation = other.transform.rotation;
                snapped = true;
            }
        }
    }
}
