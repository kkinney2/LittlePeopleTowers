using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 5;
    public float angleMod = 20;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float invulTimeSec = 3;
    public int damageFromShot = 20;
    public GameObject moveTarget;
    public Text healthText;
    //public Text winText;

    private Rigidbody rb;
    private Vector3 startCoord;
    private int health;
    private Vector3 m_Velocity = Vector3.zero;
    private float m_MovementSmoothing = .05f;
    private bool isInvul = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startCoord = transform.position;
        health = 100;
        UpdateHealth();
        //winText.text = "";
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Move(moveHorizontal * Time.fixedDeltaTime, moveVertical * Time.fixedDeltaTime);

        //Jumping Mechanic
        if (Input.GetButtonDown(buttonName: "Jump"))
        {
            //Jump();
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1);
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1);
        }

        //Fall Death/Respawn
        if (transform.position.y < -10)
        {
            transform.position = startCoord;
        }
    }

    void Move(float moveX, float moveZ)
    {
        Vector3 targetVelocity = new Vector3(moveX, rb.velocity.y, moveZ);
        targetVelocity = moveTarget.transform.TransformVector(targetVelocity) * speed;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        //rb.AddForce(targetVelocity * speed);
    }

    void Jump()
    {
        rb.AddForce((Vector3.up * jumpForce), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //if the player gets shot, take X damage
        if (other.gameObject.CompareTag("Shot"))
        {
            Destroy(other.gameObject);
            if (!isInvul)
            {
                AddHealth(damageFromShot);
                InvulTime(invulTimeSec);
            }
            
        }
    }

    void AddHealth(int healthMod)
    {
        health = health + healthMod;
        InvulTime(invulTimeSec);
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthText.text = "Health: " + health + "%";
    }

    IEnumerator InvulTime(float time)
    {
        isInvul = true;
        yield return new WaitForSeconds(time);
        isInvul = false;
    }
}
