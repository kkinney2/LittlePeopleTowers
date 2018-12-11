using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

    private int damage;

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
        Debug.Log("Shot Damage Set: " + damage);
    }

    public int GetDamage()
    {
        return damage;
    }
}
