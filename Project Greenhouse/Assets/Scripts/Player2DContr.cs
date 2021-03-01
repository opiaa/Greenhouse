using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DContr : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
	public float HSpeed = 2;
	public float VSpeed = 2;
    private bool facingRight = true;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    	//Simple Left & Right movement
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0 , 0) * Time.deltaTime * HSpeed;

        //Simple jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.01 )
        {
        	_rigidbody.AddForce(new Vector2 (0, VSpeed), ForceMode2D.Impulse);
        }

        //Change the direction of the sprite depending on the velocity
        Vector3 sprScale = transform.localScale;
        if (movement < 0 && facingRight)
        {
        	facingRight = false;
        	sprScale.x *= -1;
        	transform.localScale = sprScale;
        } 
        else if (movement > 0 && !facingRight)
        {
        	facingRight = true;
        	sprScale.x *=-1;
        	transform.localScale = sprScale;
        }

        //Always orient upwards (likely temporary until someone smarter than me makes a better solution)
        transform.localRotation = Quaternion.Euler(0,0,0);


    }
}
