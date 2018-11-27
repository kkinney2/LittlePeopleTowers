using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text townHealthText;
    public Text interactText;
    public Text towerListText;

    private int townHealth = 100;
    private string towerTextList;
    private bool isDispTowerList = false;

    // Use this for initialization
    void Start () {
        interactText.text = "";
        townHealth = 100;
        UpdateTownHealth();
        towerListText.text = "";
        towerTextList = "1 - Tower1 /r/n 2 - Tower2 /r/n 3 - Tower3";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddTownHealth(int healthMod)
    {
        townHealth = townHealth + healthMod;
        UpdateTownHealth();
    }

    void UpdateTownHealth()
    {
        townHealthText.text = "Town Health: " + townHealth + "%";
    }

    public void UpdateInteractText(bool canInteract)
    {
        if (canInteract)
        {
            interactText.text = "Interact: \"e\"";
        }
        else
        {
            interactText.text = "";
        }
    }

    public void UpdateTowerListText()
    {
        isDispTowerList = !isDispTowerList;
        if (isDispTowerList)
        {
            towerListText.text = towerTextList;
        }
        else towerListText.text = "";

    }
}
