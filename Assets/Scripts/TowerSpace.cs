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
        if (Input.GetKeyDown("e") && canInteract && gameController.GetTowersLeft() > 0)
        {
            gameController.UpdateTowerListText();
            if (!hasBuilt)
            {
                canBuild = true;
            }
        }

        if (canBuild && gameController.GetTowersLeft() > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                tempTower = towers[0];
                GameObject tempTower2 = Instantiate(tempTower ,this.transform.position, this.transform.rotation) as GameObject;
                builtTower = tempTower2.GetComponent<Tower>();
                builtTower.SetDamage(30);
                canBuild = false;
                hasBuilt = true;
                gameController.UpdateTowerListText();
                gameController.AddTowersLeft(-1);
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
                //builtTower.Range(true);
            }
            if (!hasBuilt)
            {
                //canBuild = true;
            }
            //tempChild.gameObject.SetActive(true);
        }
        else if (!canInteract)
        {
            //tempChild.gameObject.SetActive(false);
            canBuild = false;
            if (hasBuilt)
            {
                //builtTower.Range(false);
            }
        }
    }
}
