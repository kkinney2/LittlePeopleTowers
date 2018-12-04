using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpace : MonoBehaviour {

    public Text interactText;
    public GameObject[] towers;

    private bool canInteract = false;
    private bool canBuild = false;
    private bool hasBuilt = false;
    private GameController gameController;
    private GameObject tempTower;
    private Tower builtTower;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("e") && canInteract )
        {
            gameController.UpdateTowerListText();
            if (!hasBuilt)
            {
                canBuild = true;
            }
        }

        if (canBuild)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                tempTower = towers[0];
                builtTower = tempTower.GetComponent<Tower>();
                Instantiate(tempTower ,this.transform.position, this.transform.rotation);
                canBuild = false;
                hasBuilt = true;
                gameController.UpdateTowerListText();
            }
        }

        if (canBuild)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                tempTower = towers[1];
                builtTower = tempTower.GetComponent<Tower>();
                Instantiate(tempTower, this.transform.position, this.transform.rotation);
                canBuild = false;
                hasBuilt = true;
                gameController.UpdateTowerListText();
            }
        }

        if (canBuild)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                tempTower = towers[2];
                builtTower = tempTower.GetComponent<Tower>();
                Instantiate(tempTower, this.transform.position, this.transform.rotation);
                canBuild = false;
                hasBuilt = true;
                gameController.UpdateTowerListText();
            }
        }  
    }


    public void ToggleUI()
    {
        canInteract = !canInteract;
        gameController.UpdateInteractText(canInteract);
        if (canInteract)
        {
            if (hasBuilt)
            {
                builtTower.range.gameObject.SetActive(true);
            }
            if (!hasBuilt)
            {
                canBuild = true;
            }
            //tempChild.gameObject.SetActive(true);
        }
        else if (!canInteract)
        {
            //tempChild.gameObject.SetActive(false);
            canBuild = false;
            if (hasBuilt)
            {
                builtTower.range.gameObject.SetActive(false);
            }
        }
    }
}
