using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

    public GameObject range;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5f;
    public Text damageText;

    private bool hasTarget;
    private GameObject target;
    private float nextFire = 0.0f;
    private float scaleUpCap;
    private float shotScale = 1;
    private int damage;
    private int health = -1;
    private int currentHealth;
    private int numShotsHit;
    private int totalShotsHit;
    private int level;
    private int searchRadius;
    private ShotController shotController;

    // Use this for initialization
    void Start () {
        hasTarget = false;
        SetHealth(100);
        numShotsHit = 0;
        totalShotsHit = 0;
        scaleUpCap = 5;
        level = 1;
        searchRadius = 10;
    }

    // Update is called once per frame
    void Update() {
        int layerMask = 1 << 11;

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, searchRadius, layerMask);
        if (hitColliders.Length > 0)
        {
            var tempCollider = hitColliders[0];
            target = tempCollider.gameObject;
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
        }

        if (hasTarget || target != null)
        {
            var tempTrans = target.transform.position - Vector3.up * 0.5f; 
            this.transform.LookAt(tempTrans);
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject tempShot = shot;
                GameObject tempShot2 = Instantiate(tempShot, this.shotSpawn.position, this.shotSpawn.rotation);

                if (tempShot != null)
                {
                    shotController = tempShot2.GetComponent<ShotController>();
                }
                if (shotController == null)
                {
                    Debug.Log("Cannot find 'ShotController' script");
                }
                shotController.SetTower(this.gameObject);
                shotController.SetSize(shotScale);
                //shotController.SetDamage(this.GetDamage());
                //Debug.Log("Damage: " + damage);
            }
        }

        if (numShotsHit >= scaleUpCap)
        {
            level++;
            numShotsHit = 0;
            totalShotsHit++;
            scaleUpCap = scaleUpCap * (level / 2);
            StartCoroutine(ScaleUp());
        }
    }

    IEnumerator ScaleUp()
    {
        Vector3 currentScale = this.transform.localScale;
        Vector3 tempScale = new Vector3(0.1f, 0.1f, 0.1f);
        this.transform.localScale += (tempScale * level);
        yield return new WaitForSeconds(0.25f);
        this.transform.localScale = currentScale;
        yield return new WaitForSeconds(0.25f);
        this.transform.localScale += (tempScale * level);
        shotScale = 1 + 0.09f * level;
    }
    
    public void ShotTarget(GameObject tempTarget)
    {
        if(tempTarget == null)
        {
            hasTarget = false;
            target = null;
        }
        if (!hasTarget && tempTarget != null)
        {
            target = tempTarget;
            hasTarget = true;
            //Debug.Log("Target Acquired");
        }
    }

    public void Range(bool dispRange)
    {
        //range.gameObject.SetActive(dispRange);
    }

    public void SetHealth(int newHealth)
    {
        if (health == -1)
        {
            health = newHealth;
            currentHealth = newHealth;
        }
        else
        {
            currentHealth = currentHealth + newHealth;
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
        UpdateDamage();
        //Debug.Log("tower damage set: " + damage);
    }

    public int GetDamage()
    {
        return damage;
    }

    void UpdateDamage()
    {
        damageText.text = "Damage: " + GetDamage() + "";
    }

    public void EnemyHit()
    {
        numShotsHit++;
    }
}
