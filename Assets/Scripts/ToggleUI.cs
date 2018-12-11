using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour {

    private TowerSpace tempTowerSpace;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TowerSpace"))
        {
            tempTowerSpace = other.GetComponent<TowerSpace>();
            tempTowerSpace.ToggleUI();
            //Debug.Log("ToggleUI Enter");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TowerSpace"))
        {
            tempTowerSpace.ToggleUI();
            //Debug.Log("ToggleUI Exit");
        }
    }
}
