using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public int damage;
    public int health;
    public GameObject range;
    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate = 0.5f;

    private bool hasTarget;
    private GameObject target;
    private float nextFire = 0.0f;

    // Use this for initialization
    void Start () {
        hasTarget = false;

        if(this.gameObject.name == "Tower_Ranged")
        {
            health = 100;
        }

        if (this.gameObject.name == "")
        {
            health = 100;
        }

        if (this.gameObject.name == "")
        {
            health = 100;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (hasTarget)
        {
            transform.LookAt(target.transform);
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
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
}
