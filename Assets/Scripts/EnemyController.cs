using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public float speed = 100;
    public GameObject moveTarget;
    public Text enemyHealthText;
    
    private Tower tempTower;
    private int moveTargetCount;
    private int damage;
    private int health;
    private int currentHealth;
    private GameController gameController;

    // Use this for initialization
    void Start () {
        SetHealth(100);
        currentHealth = health;
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
	
	// Update is called once per frame
	void LateUpdate () {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, step);

        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");
        if (other.gameObject.CompareTag("Tower"))
        {
            tempTower = other.GetComponent<Tower>();
            tempTower.ShotTarget(this.gameObject);
            //Debug.Log("Tower Trigger");
        }

        if (other.gameObject.CompareTag("Shot"))
        {
            ShotController shotController = other.GetComponent<ShotController>();
            this.SetHealth(currentHealth - shotController.GetDamage());
            //Debug.Log("EnemyHealth: " + currentHealth);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("MoveTarget"))
        {
            moveTargetCount++;
            moveTarget = GameObject.Find("moveTarget"+moveTargetCount);
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
            gameController.AddTownHealth(-damage);
            Destroy(this.gameObject);
        }
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
        UpdateHealth();
    }

    public float GetHealth()
    {
        float tempHealth = (currentHealth / health) * 100;
        return tempHealth;
    }

    void UpdateHealth()
    {
        enemyHealthText.text = "Health: " + GetHealth() + "%";
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
