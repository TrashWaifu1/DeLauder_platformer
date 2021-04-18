using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy" && !collision.isTrigger)
            Destroy(collision.transform.gameObject);
        else
        {
            if (!collision.GetComponent<PlayerController>().torch)
            collision.GetComponent<PlayerController>().health = 0;
        }

        collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400);
    }
}
