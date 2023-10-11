using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Colisiones : MonoBehaviour
{
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    BoxCollider2D col2D;

    void Awake()
    {
        col2D = GetComponent<BoxCollider2D>();
    }

    public bool Grounded()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        //center.x para anclarse en el centro y luego le resta la mitad de ancho para ponerse en el borde izq
        Vector2 footLeft = new Vector2(col2D.bounds.center.x - col2D.bounds.extents.x, col2D.bounds.center.y);
        //mismo que anterior pero le suma para posicionarlo en x de su extremo derecho
        Vector2 footRight = new Vector2(col2D.bounds.center.x + col2D.bounds.extents.x, col2D.bounds.center.y);


        if (Physics2D.Raycast(footRight, Vector2.down * col2D.bounds.extents.y * 1.5f, groundLayer)
            || Physics2D.Raycast(footRight, Vector2.down * col2D.bounds.extents.y * 1.5f, groundLayer)
          )
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        return isGrounded;
    }

}
