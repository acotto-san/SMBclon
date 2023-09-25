using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    enum Direction { Left = -1, None = 0, Right = 1 };
    Direction currentDirection = Direction.None;

    public float speed;
    public float acceleration;
    public float maxVelocity;

    Rigidbody2D rb2D;
    Colisiones colisiones;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        colisiones = GetComponent<Colisiones>();
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
        Vector2 forceAcceleration = new Vector2((int)currentDirection * acceleration, 0f);
        rb2D.AddForce(forceAcceleration);
        float velocityX = Mathf.Clamp(rb2D.velocity.x, -maxVelocity, maxVelocity);


        Vector2 velocity = new Vector2(velocityX, rb2D.velocity.y);
        rb2D.velocity = velocity;
    }
    void Jump()
    {
        if (colisiones.Grounded())
        {
            Vector2 fuerza = new Vector2(0, 10f);
            rb2D.AddForce(fuerza, ForceMode2D.Impulse);
        }

    }
}