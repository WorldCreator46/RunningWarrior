using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D body;
    public int JumpPower;
    bool Jumping = true;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Jumping)
        {
            Jumping = false;
            jump();
        }
    }
    void jump()
    {
        body.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms"))
        {
            Jumping = true;
        }
    }
}
