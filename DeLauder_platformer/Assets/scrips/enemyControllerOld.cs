using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControllerOld : MonoBehaviour
{
    private Rigidbody2D myRB;
    GameObject playerTarget;
    public float movementSpeed = 5;
    public bool isFollowing = false;
    public bool lit;
    public Sprite normalFace;
    public Sprite invertedFace;
    SpriteRenderer mySR;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        playerTarget = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {


        if (isFollowing)
            mySR.sprite = invertedFace;
        else
            mySR.sprite = normalFace;

        Vector3 lookPos = playerTarget.transform.position - transform.position;
        //float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        //myRB.rotation = angle;
        lookPos.Normalize();

        if (isFollowing)
        {

            myRB.velocity = new Vector2(lookPos.x * movementSpeed, 0);

            // Checking to see if we're moving to the right
            if (myRB.velocity.x > 0)
                GetComponent<SpriteRenderer>().flipX = false;

            // Checking to see if we're moving to the left
            else if (myRB.velocity.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFollowing && (collision.gameObject.name == "Player"))
            isFollowing = true;

        if (collision.gameObject.name == "Circle")
        {
            lit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isFollowing && (collision.gameObject.name == "Player"))
            isFollowing = false;

        if (collision.gameObject.name == "Circle")
        {
            lit = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.rigidbody.AddForce(collision.relativeVelocity * -5);
    }
}
