using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Text townHealthText;
    public Text interactText;
    public Text[] towerTexts;
    public Text waveText;
    public Text timerText;
    public Text towersLeftText;
    public Text gameOverText;
    public GameObject enemySpawner;
    public GameObject enemy;
    public int spawnWait;
    public int waveWait;

    private int townHealth = 100;
    private int waveCount;
    private int towersLeft;
    private float enemyCount;
    private bool isDispTowerList = false;
    private EnemyController enemyController;

    // Use this for initialization
    void Start () {
        townHealth = 100;
        enemyCount = 5;
        UpdateTownHealth();
        SetWave(1);
        interactText.text = "";
        timerText.text = "";
        gameOverText.text = "";

        for (int i = 0; i < towerTexts.Length; i++)
        {
            Text tempText = towerTexts[i];
            tempText.gameObject.SetActive(false);
        }
        StartCoroutine (SpawnEnemies());
    }
	
	// Update is called once per frame
	void Update () {
        if (townHealth < 0)
        {
            StartCoroutine(EndGame());
        }
	}

    IEnumerator SpawnEnemies()
    {
        StartCoroutine(Timer(waveWait));
        yield return new WaitForSeconds(waveWait);

        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = enemySpawner.transform.position;
                Quaternion spawnRotation = enemySpawner.transform.rotation;
                GameObject tempEnemy = enemy;
                tempEnemy = Instantiate(tempEnemy, spawnPosition, spawnRotation) as GameObject;
                enemyController = tempEnemy.GetComponent<EnemyController>();
                enemyController.SetHealth((waveCount - 1) * 150 + 100);
                enemyController.SetDamage(waveCount * 5);
                //Debug.Log("Enemy Spawned");

                yield return new WaitForSeconds(spawnWait);
            }
            while (true)
            {
                int layerMask = 1 << 11;
                Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 100, layerMask);
                if (hitColliders.Length > 0)
                {
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    break;
                }
            }
            SetWave(waveCount + 1);
            spawnWait = 3 - waveCount/4;
            StartCoroutine(Timer(waveWait));
            yield return new WaitForSeconds(waveWait);
            enemyCount += enemyCount * waveCount / 2;
        }
    }
    IEnumerator Timer(int howLong)
    {
        AddTowersLeft(2);
        for (int i = howLong; i > 1; i--)
        {
            timerText.text = i + " seconds";
            yield return new WaitForSeconds(1);
        }
        timerText.text = 1 + " second";
        yield return new WaitForSeconds(1);
        timerText.text = "";

        if (towersLeft > 0)
        {
            AddTownHealth(25 * waveCount);
            AddTowersLeft(-1);
        }
        if (towersLeft > 0)
        {
            AddTownHealth(25 * waveCount);
            AddTowersLeft(-1);
        }
        towersLeftText.text = "";
    }
    IEnumerator EndGame()
    {
        gameOverText.text = "Game Over!";
        interactText.text = "";
        UpdateInteractText(true);
        for (int i = 3; i > 1; i--)
        {
            interactText.text = i + " seconds";
            yield return new WaitForSeconds(1);
        }
        interactText.text = 1 + " second";
        yield return new WaitForSeconds(1);
        interactText.text = "";
        SceneManager.LoadScene(0);
    }

    public void AddTownHealth(int healthMod)
    {
        townHealth = townHealth + healthMod;
        Debug.Log("Addtownhealth: " + townHealth);
        UpdateTownHealth();
    }

    void UpdateTownHealth()
    {
        townHealthText.text = "Town Health: " + townHealth + " pts";
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

    public int GetWave()
    {
        return waveCount;
    }

    private void SetWave(int newWave)
    {
        waveCount = newWave;
        UpdateWave();
    }
    public void AddTowersLeft(int numLeft)
    {
        towersLeft = towersLeft + numLeft;
        UpdateTowersLeft();
    }

    public int GetTowersLeft()
    {
        return towersLeft;
    }

    private void UpdateTowersLeft()
    {
        towersLeftText.text = "Towers Left: " + towersLeft;
    }

    public void UpdateWave()
    {
        waveText.text = "Wave " + waveCount;
    }
}
