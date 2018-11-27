using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int damage;
    public int health;

    private Tower tempTower;

    // Use this for initialization
    void Start () {
        health = 100;
    }
	
	// Update is called once per frame
	void Update () {
		
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
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            tempTower = other.GetComponent<Tower>();
            tempTower.ShotTarget(null);
        }
    }
}
