using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAnimation : MonoBehaviour {
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }
    public void startToolAnimation()
    {
        Debug.Log("Starting");
        Debug.Log(animator);
        this.animator.SetBool("Swinging", true);
    }
    public void stopToolAnimation()
    {
        Debug.Log("Stoping");
        Debug.Log(animator);
        this.animator.SetBool("Swinging", false);
    }
}
