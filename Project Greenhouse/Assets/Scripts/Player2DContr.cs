using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DContr : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
    private Collider2D colliders;
    private Animator anim;
	public float HSpeed = 6;
	public float MaxSpeed = 6;
	public float VSpeed = 6;
    private bool facingRight = true;
    private float jumping;
    private Vector2 movement; 
    private SpriteRenderer sprRender;

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
    	if (GetComponent<Animator>())
    	{
    		anim = GetComponent<Animator>();
    	}
        _rigidbody = GetComponent<Rigidbody2D>();
        sprRender = GetComponent<SpriteRenderer>();

    }

    public void Update()
    {
    	//----Simple Horizontal movement
    	//If the velocity is less than the Max speed then add a force
    	if (Mathf.Abs(_rigidbody.velocity.x) < MaxSpeed) //|| movement.x * _rigidbody.velocity.x < 0) <--- I have no clue why I added this here but just in case there's a bug I refuse to remove it
    	{
    		_rigidbody.AddForce(movement*HSpeed);
    		if (anim && movement.x!=0)
			{
				anim.SetBool("walking", true);
			}
		}

		//----Deceleration to stop it being so floaty
		//If there is no player input then add a force in the opposite direction of movement
		if (movement.x==0 && _rigidbody.velocity.x != 0 && jumping == 0)
		{
			
			_rigidbody.AddForce(new Vector2(_rigidbody.velocity.x * -1,0));
			if (anim)
			{
				anim.SetBool("walking", false);
			}
		}

        //Simple jump
        if ((jumping == 1) && Mathf.Abs(_rigidbody.velocity.y) < 0.001)
        {
        	_rigidbody.AddForce(new Vector2(0f, VSpeed), ForceMode2D.Impulse);
        	jumping=0;
        } 

        //Change the direction of the sprite depending on the velocity
        if (movement.x < 0 && facingRight)
        {
        	facingRight = false;
        	sprRender.flipX=true;
        } 
        else if (movement.x > 0 && !facingRight)
        {
        	facingRight = true;
        	sprRender.flipX=false;

        }

        //Always orient upwards (likely temporary until someone smarter than me makes a better solution)
        transform.localRotation = Quaternion.Euler(0,0,0);

    }
}
