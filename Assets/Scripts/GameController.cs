using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text townHealthText;
    public Text interactText;
    public Text[] towerTexts;
    public GameObject enemySpawner;
    public GameObject enemy;
    public int spawnWait;
    public int enemyCount;
    public int waveWait;

    private int townHealth = 100;
    private bool isDispTowerList = false;
    private bool gameOver = false;

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
        StartCoroutine (SpawnEnemies());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = enemySpawner.transform.position;
                Quaternion spawnRotation = enemySpawner.transform.rotation;
                Instantiate(enemy, spawnPosition, spawnRotation);
                //Debug.Log("Enemy Spawned");
                yield return new WaitForSeconds(spawnWait);
            }

            if (gameOver)
            {
                //restartText.text = "Press 'R' for Restart";
                //restart = true;
                break;
            }

            yield return new WaitForSeconds(waveWait);
        }
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
