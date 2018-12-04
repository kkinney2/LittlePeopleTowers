using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int damage;
    public int health;
    public float speed = 100;
    public GameObject moveTarget;
    
    private Tower tempTower;
    private int moveTargetCount;

    // Use this for initialization
    void Start () {
        health = 100;
        moveTarget = GameObject.Find("moveTarget1");
        moveTargetCount = 1;
        if(moveTarget == null)
        {
            Debug.Log("Could not find moveTarget");
        }
    }
	
	// Update is called once per frame
	void Update () {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, step);
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
            Destroy(this.gameObject);
        }
    }
}
