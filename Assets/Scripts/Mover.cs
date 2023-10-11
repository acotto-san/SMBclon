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
    public float friction;
    float currentVelocity = 0;

    public float jumpForce;
    public float maxJumpingTime = 1f;
    public bool isJumping;
    float jumpTimer = 0f;
    float defaultGravity;

    Rigidbody2D rb2D;
    Colisiones colisiones;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        colisiones = GetComponent<Colisiones>();

    }

    private void Start()
    {
        defaultGravity = rb2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping)
        {
            if(rb2D.velocity.y < 0f)
            {
                //como ya está cayendo entonces restablecer gravedad
                rb2D.gravityScale = defaultGravity;
                if (colisiones.Grounded())
                {
                    //como ya está tocando ground entonces dejó de saltar
                    isJumping = false;
                    jumpTimer = 0;
                }   
            }
            else if (rb2D.velocity.y > 0f)
            {
                 if (Input.GetKey(KeyCode.Space))
                 {
                     //ya que está con la barra presionada empezar a contar segundos
                     jumpTimer = Time.deltaTime;
                 }
                 if (Input.GetKeyUp(KeyCode.Space))
                 {
                     // como dejó de saltar evaluemos cuanto tiempo salto
                     if (jumpTimer < maxJumpingTime)
                     {
                         //ya que saltó menos del tiempo esperado aplicar gravedad
                         rb2D.gravityScale = defaultGravity * 3f;
                     }
                 }
            }
        }
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
        //Vector2 forceAcceleration = new Vector2((int)currentDirection * acceleration, 0f);
        //rb2D.AddForce(forceAcceleration);
        //float velocityX = Mathf.Clamp(rb2D.velocity.x, -maxVelocity, maxVelocity);


        //Vector2 velocity = new Vector2(velocityX, rb2D.velocity.y);
        //rb2D.velocity = velocity;

        currentVelocity = rb2D.velocity.x;
        if(currentDirection > 0) // si el input es hacia la der
        {
            if(currentVelocity < 0) //pero la velocidad esta en direccion izq
            {
                //entonces para llevarla al sentido inverso se le agrega puntos de aceleración y fricción
                currentVelocity += (acceleration + friction) * Time.deltaTime; 
            }
            else if(currentVelocity < maxVelocity) // en caso de la velocidad no es hacia izq y menor al limite
            {
                //se agrega solo aceleracion ya que friccion no se quiere 
                currentVelocity += acceleration * Time.deltaTime;
            }

        }
        else if(currentDirection < 0) //si el input es hacia la izq
        {
            if (currentVelocity > 0) 
            { 
                //para llevar la velocidad al lado opueso esta vez es una reducción de velocidad
                currentVelocity -= (acceleration + friction) * Time.deltaTime;  
            }
            else if(currentVelocity > -maxVelocity)
            {
                currentVelocity -= acceleration * Time.deltaTime;
            }
        }
        else //si no hay input de direccion hay que ir frenandolo si se esta moviendo
        {
            if(currentVelocity > 1f) //reduce velocidad si esta yendo a izq
            {
                currentVelocity -= friction * Time.deltaTime;
            }
            else if(currentVelocity < -1f)
            {
                currentVelocity += friction * Time.deltaTime;
            }
            else
            {
                currentVelocity = 0;
            }
        }
        Vector2 velocity = new Vector2(currentVelocity,rb2D.velocity.y);
        rb2D.velocity = velocity;
    }
    void Jump()
    {
        if (colisiones.Grounded() && !isJumping)
        {
            isJumping = true;
            Vector2 fuerza = new Vector2(0, jumpForce);
            rb2D.AddForce(fuerza, ForceMode2D.Impulse);
        }

    }
}