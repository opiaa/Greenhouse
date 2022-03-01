﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum PowerUp { Speed, Power }

public class Player2DContr : MonoBehaviour
{
    public int PlayerNumber;
	private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Animator anim;
    public GameObject cam;
	private Vector3 newCamPos;
	public float HSpeed = 6;
    public float HSpeedDefault = 6;
	public float MaxSpeed = 6;
	public float VSpeed = 6;
    private bool facingRight = true;
    private float jumping;
    private Vector2 movement; 
    private SpriteRenderer sprRender;
    public int speedBoostTime = 5;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        sprRender = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        //Depending on what player it is, subscribe to their different power up events (could be cleaned up with an interface)
        BasicPowerUp.ApplyPowerUps+=ApplyPowerup;
        if (PlayerNumber==1)
        {
            CoffeePwrUp.ApplyPowerUps+=ApplyPowerup;
        }
    }

    private void OnDisable()
    {
        BasicPowerUp.ApplyPowerUps-=ApplyPowerup;

        if (PlayerNumber==1) 
        {
            CoffeePwrUp.ApplyPowerUps-=ApplyPowerup;
        }

    }

    public void OnJump(InputValue input)
    {
    	jumping = input.Get<float>();
    }

	public void OnMove(InputValue input)
    	{
    		Vector2 inputVec = input.Get<Vector2>();
            movement = new Vector2(inputVec.x, 0);
    	}

    //This function redirects (+ does any pre-launch needed) to the proper functions based on the enum value passed

    public void ApplyPowerup(GameObject player, PowerUp powerUp, float factor = 1)
    {
        if (player==gameObject)
        {
            switch (powerUp)
            {
                case PowerUp.Speed:
                    StartCoroutine(ApplySpeedBoost(factor));
                    break;
                case PowerUp.Power:
                    StartCoroutine(ApplyPowerBoost(factor));
                    break;
                default:
                    break;
            }
        }
    }

    //It's boostin' time -> change HSpeed, and return it to the default value set when the boost timer is done
    IEnumerator ApplySpeedBoost(float factor)
    {
        HSpeed *= factor;
        yield return new WaitForSeconds(speedBoostTime);
        HSpeed = HSpeedDefault;
    }

    //Stronk time, change the power variable, making the player clean/destroy things faster
    IEnumerator ApplyPowerBoost(float factor)
    {
        GetComponent<PlayerInt>().SetPower((int)factor);
        yield return new WaitForSeconds(speedBoostTime);
        GetComponent<PlayerInt>().SetPower(1);
    }


    public void Update()
    {
        //----Simple Horizontal movement
        //If the velocity is less than the Max speed then add a force
        transform.Translate(Time.deltaTime * HSpeed * movement);
        if (Mathf.Abs(movement.x)>0)
		{
			anim.SetBool("walking", true);

            //Animate the camera a little bit too for more visual feedback
            newCamPos = new Vector3(movement.x/2, cam.transform.localPosition.y, cam.transform.localPosition.z);
            cam.transform.localPosition = cam.transform.localPosition + (newCamPos-cam.transform.localPosition)*0.1f;
            /*Basically the camera has to always be at -20 on the Z axis, so this just makes sure that it stays that way, 
            since *0.1f multiplies the Z axis too*/
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -20);
        } 
        //----Change animation when stopping movement
        else if	(movement.x == 0)
		{
			anim.SetBool("walking", false);

			//Animate the camera back to 0,0,-20
			newCamPos = new Vector3(0, cam.transform.localPosition.y, cam.transform.localPosition.z);
			cam.transform.localPosition = cam.transform.localPosition + (newCamPos-cam.transform.localPosition)*0.1f;
			//Same as earlier 
			cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -20);
		}

        //Simple jump
        if ((jumping == 1) && Mathf.Abs(_rigidbody.velocity.y) < 0.001)
        {
        	_rigidbody.AddForce(new Vector2(0f, VSpeed), ForceMode2D.Impulse);
        	jumping=0;
        }

        //Change the direction of the sprite depending on the velocity
        if (movement.x < 0f && facingRight)
        {
        	facingRight = false;
        	sprRender.flipX=true;
        } 
        else if (movement.x > 0f && !facingRight)
        {
        	facingRight = true;
        	sprRender.flipX=false;

        }

        /*Always orient upwards (likely temporary until someone smarter than me 
        makes a better solution for characters falling over lmao)*/
        transform.localRotation = Quaternion.Euler(0,0,0);
    }
}