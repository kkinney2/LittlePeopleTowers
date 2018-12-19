using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public float speed = 4;
    public GameObject moveTarget;
    public Text enemyHealthText;
    
    private Tower tempTower;
    private float speedStart = 4;
    private int moveTargetCount;
    private int damage = 5;
    private int health = -1;
    private int currentHealth;
    private GameController gameController;

    // Use this for initialization
    void Start () {
        UpdateHealth();

        moveTarget = GameObject.Find("moveTarget1");
        moveTargetCount = 1;
        if(moveTarget == null)
        {
            Debug.Log("Could not find moveTarget");
        }

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

    private void Update()
    {
        speed = 2 + speedStart * ((float)gameController.GetWave() / 4);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed Self(Enemy)");
        }
    }

    // Update is called once per frame
    void LateUpdate () {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, step);
    }

    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Shot"))
        {
            ShotController shotController = other.GetComponent<ShotController>();
            Debug.Log("EnemyHealth: " + currentHealth + " - " + shotController.GetDamage());
            this.SetHealth(-shotController.GetDamage());
            shotController.EnemyHit();
            tempTower = shotController.GetTower();
            tempTower.ShotTarget(null);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("MoveTarget"))
        {
            moveTargetCount++;
            moveTarget = GameObject.Find("moveTarget"+moveTargetCount);
            if(moveTarget == null)
            {
                gameController.AddTownHealth(-damage);
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            tempTower = other.GetComponent<Tower>();
            tempTower.ShotTarget(null);
        }
        if (other.gameObject.CompareTag("PlayArea"))
        {
            //gameController.AddTownHealth(-damage);
            //Destroy(this.gameObject);
        }
    }

    public void SetHealth(int newHealth)
    {
        if(health == -1)
        {
            health = newHealth;
            currentHealth = newHealth;
        }
        else
        {
            currentHealth = currentHealth + newHealth;
        }
        UpdateHealth();
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    void UpdateHealth()
    {
        enemyHealthText.text = "Health: " + GetHealth() + "pts";
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }
}
