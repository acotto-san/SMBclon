using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    enum Direction { Left = -1, None = 0, Right = 1 };

    Direction currentDirection = Direction.None;

    public float speed;
    // Start is called before the first frame update
    Rigidbody2D rb2D;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDirection = Direction.None;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            currentDirection = Direction.Right;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            currentDirection = Direction.Left;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2((int)currentDirection * speed, rb2D.velocity.y);
        rb2D.velocity = velocity;
    }
    void Jump()
    {
        Vector2 fuerza = new Vector2(0, 10f);
        rb2D.AddForce(fuerza, ForceMode2D.Impulse);
    }
}