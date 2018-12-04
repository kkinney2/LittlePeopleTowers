using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

    private float dist;

	// Use this for initialization
	void Start () {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 0.1f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Collider temp = hitColliders[i];
            float tempDist = Vector3.Distance(temp.transform.position, transform.position);
            if( i == 0 )
            {
                dist = tempDist;
            }
            if (tempDist < dist)
            {
                dist = tempDist;
                temp.GetComponent<Tower>();
                Debug.Log("Shot/Tower Acquired");
            }
            i++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
