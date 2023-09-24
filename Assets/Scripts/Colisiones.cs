using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Pipe"))
    //        Debug.Log("Collision ENTER: " + collision.gameObject.name);   
    //}   

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    //Debug.Log("Collision Stay: " + collision.gameObject.name);
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Pipe"))
    //        Debug.Log("Collision EXIT: " + collision.gameObject.name);
    //}   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            Debug.Log("Collision ENTER: " + collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            Debug.Log("Collision EXIT: " + collision.gameObject.name);
    }
}
