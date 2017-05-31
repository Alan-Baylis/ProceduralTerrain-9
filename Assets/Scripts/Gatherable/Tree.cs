using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tree : MonoBehaviour, IGatherable {
    private int health;
    private int xp;
    public AudioClip harvestingClip;
    private AudioSource audio;


    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        health = (int) UnityEngine.Random.Range(1,5);
        xp = health;	
	}

    public void harvest()
    {
       // audio.PlayOneShot(harvestingClip);
        health--;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision entered");

        //Debug.Log(collision.gameObject.transform.parent.parent.parent);
        //Debug.Log(collision.gameObject.transform.parent.parent.parent.GetComponent<EnvironmentInteraction>());
        if (collision.gameObject.tag.Equals("Axe"))
        {
            EnvironmentInteraction player = collision.gameObject.transform.parent.parent.parent.GetComponent<EnvironmentInteraction>();
            if (player.getHarvesting() && !audio.isPlaying)
            {
                //audio.PlayOneShot(harvestingClip);
                audio.Play();
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Axe"))
        {
            EnvironmentInteraction player = collision.gameObject.transform.parent.parent.parent.GetComponent<EnvironmentInteraction>();
            if (!player.getHarvesting() && audio.isPlaying)
            {
                audio.Stop();
            }
            else if (player.getHarvesting() && !audio.isPlaying)
            {
                audio.Play();
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Axe"))
        {
            EnvironmentInteraction player = collision.gameObject.transform.parent.parent.parent.GetComponent<EnvironmentInteraction>();
            if (!player.getHarvesting())
            {
                audio.Stop();
            }
        }
    }
    public int getBark()
    {
        if (health <= 0)
            Destroy(gameObject, 0.1f);
        return (int)UnityEngine.Random.Range(0, 5);
    }
    public int getStone()
    {
        return 0;
    }
    public int getWood()
    {
        if (health <= 0)
            Destroy(gameObject, 0.1f);
        return (int)UnityEngine.Random.Range(0, 5);
    }
    public int getFiber()
    {
        return 0;
    }
    public int getXp()
    {
        return xp;
    }

    public string toString()
    {
        return "Tree";
    }
}
