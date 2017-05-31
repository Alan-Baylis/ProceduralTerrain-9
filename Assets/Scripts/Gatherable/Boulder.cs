using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour, IGatherable
{
    private int health;
    private int xp;
    private AudioSource audio;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        health = (int)UnityEngine.Random.Range(1, 8);
        xp = health;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag.Equals("PickAxe"))
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
        if (collision.gameObject.tag.Equals("PickAxe"))
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
        if (collision.gameObject.tag.Equals("PickAxe"))
        {
            EnvironmentInteraction player = collision.gameObject.transform.parent.parent.parent.GetComponent<EnvironmentInteraction>();
            if (!player.getHarvesting())
            {
                audio.Stop();
            }
        }
    }

    public void harvest()
    {
        health--;
    }
    public int getBark()
    {
        return 0;
    }
    public int getStone()
    {
        if (health <= 0)
            Destroy(gameObject, 0.1f);
        return (int)UnityEngine.Random.Range(0, 5);
    }
    public int getWood()
    {
        return 0;
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
        return "Boulder";
    }
}
