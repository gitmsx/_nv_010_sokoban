using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovementGrid001 : MonoBehaviour
{

    public float speed = 10; 
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontalf = Input.GetAxis("Horizontal");
        float Verticalf = Input.GetAxis("Vertical");
        Vector3 mov = new Vector3(Horizontalf, 0f, Verticalf) * speed; 
        mov = Vector3.ClampMagnitude(mov, speed);
        
        if (mov != Vector3.zero)
        {
            rb.MoveRotation(Quaternion.LookRotation(mov));
            rb.MovePosition(transform.position + mov * Time.deltaTime);
         
        }
    }

}


