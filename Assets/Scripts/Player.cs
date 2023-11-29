using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public ObjectStats stats;

    public int playerSpeed = 4;

    private Rigidbody2D rb;

    private Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * playerSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Strength") && stats.strength > 10 && stats.strength < 20)
        {
            Debug.Log("Strength");
            Destroy(collision.gameObject);
        }
    }
}
