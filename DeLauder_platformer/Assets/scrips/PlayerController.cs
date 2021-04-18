using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    public Vector2 velocity;
    public Vector2 respawnPos;
    private Quaternion zero;
    public int health = 3;
    public float speed = 5;
    public float jumpHeight = 6.25f;
    public float doubleClickInterval = 0.2f;
    public float dashVelocity = 30;
    public float dashTime = 0.3f;
    public Sprite normalFace;
    public Sprite invertedFace;
    public float groundDetectDistance = .1f;
    SpriteRenderer mySR;
    Vector2 gCC;
    public float dashCooldown = 1;
    public float dashCooldownTimer;
    float dashTimer;
    float dashCheckTimerD;
    float dashCheckTimerA;
    bool dashCheckD;
    bool dashCheckA;
    bool dash;
    float timer;
    public bool torch;
    public float torchCooldown = 5;
    public float torchCooldownTimer;
    public float torchTimerLength = 3;
    public float torchTimer = 1;
    public bool coolDownActive;
    public string playerTriggerCheck;
    public CircleCollider2D circlee;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        zero = new Quaternion();
    }

    // Update is called once per frame
    void Update()
    {
        gCC = new Vector2(transform.position.x, transform.position.y - .51f);

        if (Input.GetKeyDown(KeyCode.Q) && !coolDownActive)
        {
            torch = !torch;
            GetComponentInChildren<Animator>().SetBool("expand", torch);
            circlee.enabled = torch;
            torchTimer = torchTimerLength;
            

            if (!torch)
            {
                torchCooldownTimer = torchCooldown;
                coolDownActive = true;
            }
        }

        #region ui thing help pleas
        if (torch == true)
        {
            torchTimer -= Time.deltaTime;
            if (torchTimer <= 0)
            {
                torchCooldownTimer = torchCooldown;
                torch = false;
                GetComponentInChildren<Animator>().SetBool("expand", torch);
                coolDownActive = true;
                circlee.enabled = false;
            }
        }
            
        if (coolDownActive)
        {
            torchCooldownTimer -= Time.deltaTime;
            if (torchCooldownTimer <= 0)
            {
                torchTimer = torchTimerLength;
                coolDownActive = false;
            }
        }
        #endregion

        dashTimer -= Time.deltaTime;

        if (health <= 0)
        {
            transform.SetPositionAndRotation(respawnPos, zero);
            health = 3;
        }

        velocity = myRB.velocity;

        if (!dash)
            velocity.x = Input.GetAxisRaw("Horizontal") * speed;

        if (dash && dashTimer <= 0)
        {
            mySR.sprite = normalFace;
            dash = false;
        }

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;


        #region Dash A
        if (dashCheckA)
            dashCheckTimerA += Time.deltaTime;

        else if (Input.GetKeyDown(KeyCode.A))
            dashCheckA = true;

        if (Input.GetKeyDown(KeyCode.A) && dashCheckTimerA <= doubleClickInterval && dashCheckTimerA != 0 && dashCooldownTimer <= 0)
        {
            velocity.x = -dashVelocity;
            dashTimer = dashTime;
            mySR.sprite = invertedFace;
            dash = true;
            dashCheckTimerA = 0;
            dashCheckA = false;
            dashCooldownTimer = dashCooldown;
        }


        if (dashCheckTimerA >= doubleClickInterval)
        {
            dashCheckTimerA = 0;
            dashCheckA = false;
        }
        #endregion

        #region Dash D
        if (dashCheckD)
            dashCheckTimerD += Time.deltaTime;

        else if (Input.GetKeyDown(KeyCode.D))
            dashCheckD = true;

        if (Input.GetKeyDown(KeyCode.D) && dashCheckTimerD <= doubleClickInterval && dashCheckTimerD != 0 && dashCooldownTimer <= 0)
        {
            velocity.x = dashVelocity;
            dashTimer = dashTime;
            mySR.sprite = invertedFace;
            dash = true;
            dashCheckTimerD = 0;
            dashCheckD = false;
            dashCooldownTimer = dashCooldown;
        }


        if (dashCheckTimerD >= doubleClickInterval)
        {
            dashCheckTimerD = 0;
            dashCheckD = false;
        }
        #endregion

        if (Input.GetKey(KeyCode.Space) && Physics2D.Raycast(gCC, Vector2.down, groundDetectDistance))
            velocity.y = jumpHeight;

        myRB.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("enemy") && !dash)
            health--;
        else if (collision.gameObject.tag.Contains("enemy") && dash && collision.gameObject.GetComponent<enemyController>().lit)
        {
            Destroy(collision.gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerTriggerCheck = collision.name;
    }
}
