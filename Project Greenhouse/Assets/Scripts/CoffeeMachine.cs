using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
	/*This is a script that could be optimised, since most of these calculations are happening 
	every frame, which may not be necessary as there are OnStateUpdate/OnStateEnter/OnStateExist events
	but they have to be attached to the Animation Controller itself or something weird */
	public GameObject CoffeePrefab;
	private Animator animator;
	private GameObject CoffeeInstance;
    private Collider2D thisCol;
    public int timeTillNextCoffee = 2;
    private bool timerUp = true;
    private int StateNum;
	public PowerUp powerType;

    void Start()
    {
     	animator = GetComponent<Animator>();
        thisCol = GetComponent<Collider2D>();

    }

    void Update()
    {
    	StateNum = animator.GetInteger("StateNum");

		
		//If the current animation clip is CMAchineCompleteIdle and the CoffeeInstance exists then do stuff
        //--Needs to be before we create the coffee cup otherwise every new coffee cup will be ready instantly because the animator takes a few frames to catch up
        if (CoffeeInstance)
        {
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="CMachineCompleteIdle")
            {
                CoffeeInstance.GetComponent<Animator>().SetInteger("StateNum", 1);
            }
        }

    	//If the coffee machine is Idle and it hasn't made a coffee object yet
    	if (timerUp && StateNum==0 && !CoffeeInstance)
		{
			//Make the empty coffee object and place it perfectly
            animator.Play("CMachineIdle");
            timerUp=false;
			CoffeeInstance = Instantiate(CoffeePrefab, transform.position+new Vector3(0.01f,-0.01f,0f), Quaternion.identity);
			animator.SetInteger("StateNum", StateNum+1);
		}

    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        //if the coffe is not set to be destroyed and is spinning then do the powerup
        if (CoffeeInstance && !CoffeeInstance.GetComponent<Animator>().GetBool("Destroyed") && CoffeeInstance.GetComponent<Animator>().GetInteger("StateNum")==1)
        {
            if (col.gameObject.name == "PlayerHumanoid")
            {
                Destroy(CoffeeInstance); //Destroy the object and set the animator state BEFORE we set the power-up
                animator.SetInteger("StateNum", 0);
                col.gameObject.GetComponent<Player2DContr>().ApplyPowerup(powerType);
                col.gameObject.GetComponent<Player2DContr>().ApplyPowerup(PowerUp.Power);
                StartCoroutine(waitTime(timeTillNextCoffee));
            }
        }
    }

    //Wait a few seconds before being able to make another Coffee cup
    IEnumerator waitTime(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        timerUp=true;
    }
}