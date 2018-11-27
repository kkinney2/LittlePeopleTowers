using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpace : MonoBehaviour {

    public Text interactText;

    private bool canInteract = false;
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
        else
        {
            tempChild.gameObject.SetActive(false);
        }
    }
}
