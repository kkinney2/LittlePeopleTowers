using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text townHealthText;
    public Text interactText;
    public Text[] towerTexts;
    public Text waveText;
    public Text timerText;
    public GameObject enemySpawner;
    public GameObject enemy;
    public int spawnWait;
    public int waveWait;

    private int townHealth = 100;
    private int waveCount;
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

        for (int i = 0; i < towerTexts.Length; i++)
        {
            Text tempText = towerTexts[i];
            tempText.gameObject.SetActive(false);
        }
        StartCoroutine (SpawnEnemies());
    }
	
	// Update is called once per frame
	void Update () {
        enemyCount = enemyCount * waveCount / 2;
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
                enemyController = enemy.GetComponent<EnemyController>();
                enemyController.SetHealth(waveCount * 2 + 100);
                enemyController.SetDamage(waveCount * 2 + 2);
                Instantiate(enemy, spawnPosition, spawnRotation);
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
            
            StartCoroutine(Timer(waveWait));
            yield return new WaitForSeconds(waveWait);
            SetWave(waveCount++);
        }
    }
    IEnumerator Timer(int howLong)
    {
        for (int i = howLong; i > 1; i--)
        {
            timerText.text = i + " seconds";
            yield return new WaitForSeconds(1);
        }
        timerText.text = 1 + " second";
        yield return new WaitForSeconds(1);
        timerText.text = "";
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

    public void UpdateWave()
    {
        waveText.text = "Wave " + waveCount;
    }
}
