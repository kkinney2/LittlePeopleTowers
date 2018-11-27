using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text townHealthText;
    public Text interactText;
    public Text[] towerTexts;

    private int townHealth = 100;
    private bool isDispTowerList = false;

    // Use this for initialization
    void Start () {
        townHealth = 100;
        UpdateTownHealth();
        interactText.text = "";

        for (int i = 0; i < towerTexts.Length; i++)
        {
            Text tempText = towerTexts[i];
            tempText.gameObject.SetActive(false);
        }
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
            for(int i = 0; i < towerTexts.Length; i++)
            {
                Text tempText = towerTexts[i];
                tempText.gameObject.SetActive(true);
            }
        }

        if (!isDispTowerList)
        {
            for (int i = 0; i < towerTexts.Length; i++)
            {
                Text tempText = towerTexts[i];
                tempText.gameObject.SetActive(false);
            }
        }
    }
}
