using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public GameObject range;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5f;

    private bool hasTarget;
    private GameObject target;
    private float nextFire = 0.0f;
    private int damage;
    private int health;
    private int currentHealth;
    private ShotController shotController;

    // Use this for initialization
    void Start () {
        hasTarget = false;

        health = 100;
        currentHealth = health;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasTarget)
        {
            this.transform.LookAt(target.transform);
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject tempShot = shot;
                Instantiate(tempShot, this.shotSpawn.position, this.shotSpawn.rotation);

                GameObject shotControllerObject = GameObject.FindWithTag("GameController");
                if (tempShot != null)
                {
                    shotController = tempShot.GetComponent<ShotController>();
                }
                if (shotController == null)
                {
                    Debug.Log("Cannot find 'ShotController' script");
                }

                shotController.SetDamage(this.GetDamage());
                Debug.Log("Damage: " + damage);
            }
        }
	}
    
    public void ShotTarget(GameObject tempTarget)
    {
        if (!hasTarget)
        {
            target = tempTarget;
            hasTarget = true;
            //Debug.Log("Target Acquired");
        }
        if(tempTarget == null)
        {
            hasTarget = false;
        }
    }

    public void Range(bool dispRange)
    {
        range.gameObject.SetActive(dispRange);
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public float GetHealth()
    {
        float tempHealth = currentHealth / health;
        return tempHealth;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
        Debug.Log("tower damage set: " + damage);
    }

    public int GetDamage()
    {
        return damage;
    }
}
