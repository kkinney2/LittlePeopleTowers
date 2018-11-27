using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpace : MonoBehaviour {

    public Text interactText;
    public GameObject[] towers;

    private bool canInteract = false;
    private bool canBuild = false;
    private Transform tempChild;
    private GameController gameController;

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

        tempChild = this.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("e") && canInteract )
        {
            gameController.UpdateTowerListText();
            canBuild = true;
        }
        if (canBuild)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Instantiate(towers[0],this.transform.position, this.transform.rotation);
                canBuild = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Instantiate(towers[1], this.transform.position, this.transform.rotation);
                canBuild = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Instantiate(towers[2], this.transform.position, this.transform.rotation);
                canBuild = false;
            }
        }
    }


    public void ToggleUI()
    {
        canInteract = !canInteract;
        gameController.UpdateInteractText(canInteract);
        if (canInteract)
        {
            tempChild.gameObject.SetActive(true);
        }
        else if (!canInteract)
        {
            tempChild.gameObject.SetActive(false);
            canBuild = false;
        }
    }
}
