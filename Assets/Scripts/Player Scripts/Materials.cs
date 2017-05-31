using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Materials {
    private int amount;
    private string name;
    private Text guiText;

    public Materials(string name)
    {
        amount = 0;
        this.name = name;

    }
    public void addMaterial(int inputMat)
    {
        guiText = GameObject.Find(name + "Amount").GetComponent<Text>();
        amount += inputMat;
        guiText.text = amount.ToString();
    }
    public int checkAmount()
    {
        return amount;
    }
    public void removeMat(int inputMat)
    {
        guiText = GameObject.Find(name + "Amount").GetComponent<Text>();
        amount -= inputMat;
        guiText.text = amount.ToString();
    }
    public void printMat()
    {
        Debug.Log(name + ", Total: " + amount);
    }
    public void setAmount(int inputMat)
    {
        guiText = GameObject.Find(name + "Amount").GetComponent<Text>();
        amount = inputMat;
        guiText.text = amount.ToString();
    }
}