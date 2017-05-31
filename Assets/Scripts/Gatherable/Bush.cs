using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour, IGatherable
{
    private int health;
    private int xp;
    public AudioClip harvestingClip;
    private AudioSource audio;


    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        health = (int)UnityEngine.Random.Range(1, 5);
        xp = health;
    }

    public void harvest()
    {
        health--;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Sickle"))
        {
            EnvironmentInteraction player = collision.gameObject.transform.parent.parent.parent.GetComponent<EnvironmentInteraction>();
            if (player.getHarvesting() && !audio.isPlaying)
            {
                audio.Play();
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Sickle"))
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
        if (collision.gameObject.tag.Equals("Sickle"))
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
        return 0;
    }
    public int getStone()
    {
        return 0;
    }
    public int getWood()
    {
        return 0;
    }
    public int getFiber()
    {
        if (health <= 0)
            Destroy(gameObject, 0.1f);
        return (int)UnityEngine.Random.Range(0, 5);
    }
    public int getXp()
    {
        return xp;
    }

    public string toString()
    {
        return "Bush";
    }
}
