using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

    private int damage;
    private GameObject tower;
    private Tower towerScript;

    private void Update()
    {
        if(towerScript == null && tower != null)
        {
            towerScript = tower.GetComponent<Tower>();
        }

        if(towerScript != null)
        {
            SetDamage(towerScript.GetDamage());
            Debug.Log(GetDamage());
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
        Debug.Log("Shot Damage Set: " + damage);
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetTower(GameObject newTower)
    {
        tower = newTower;
        towerScript = tower.GetComponent<Tower>();
    }

    public void EnemyHit()
    {
        towerScript.EnemyHit();
    }
}
