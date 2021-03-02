using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DContr : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
	public float HSpeed = 6;
	public float MaxSpeed = 6;
	public float VSpeed = 6;
    private bool facingRight = true;
    private float jumping;
    private Vector2 movement; 

    public void OnJump(InputValue input)
    {
    	jumping = input.Get<float>();
    }

	public void OnMove(InputValue input)
    	{
    		Vector2 inputVec = input.Get<Vector2>();
    		movement = new Vector3(inputVec.x, 0, inputVec.y);
    	}

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    public void Update()
    {
    	//Simple Horizontal movement
    	if (_rigidbody.velocity.x < MaxSpeed && _rigidbody.velocity.x > -MaxSpeed || movement.x * _rigidbody.velocity.x < 0)
    	{
    		_rigidbody.AddForce(movement*HSpeed);
		}

		//Extra drag to stop it being so floaty
		if (movement.x==0 && _rigidbody.velocity.x != 0 && jumping == 0)
		{
			
			_rigidbody.AddForce(new Vector2(_rigidbody.velocity.x * -1,0));
		}

        //Simple jump
        if ((jumping == 1) && Mathf.Abs(_rigidbody.velocity.y) < 0.001)
        {
        	_rigidbody.AddForce(new Vector2(0f, VSpeed), ForceMode2D.Impulse);
        	jumping=0;
        } 


        //Change the direction of the sprite depending on the velocity
        Vector3 sprScale = transform.localScale;
        if (movement.x < 0 && facingRight)
        {
        	facingRight = false;
        	sprScale.x *= -1;
        	transform.localScale = sprScale;
        } 
        else if (movement.x > 0 && !facingRight)
        {
        	facingRight = true;
        	sprScale.x *=-1;
        	transform.localScale = sprScale;
        }

        //Always orient upwards (likely temporary until someone smarter than me makes a better solution)
        transform.localRotation = Quaternion.Euler(0,0,0);
    }
}
