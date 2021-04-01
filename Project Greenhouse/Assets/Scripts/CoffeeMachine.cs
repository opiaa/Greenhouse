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
    public Material powerUpMaterial;
    public Material outlineMaterial;
    public Material regularMaterial;
    private bool coffeeReady = false;
    void Start()
    {
     	animator = GetComponent<Animator>();
        thisCol = GetComponent<Collider2D>();
        PlayerInt.onInteraction += SetPowerUp;
    }

    void OnDisable()
    {
        PlayerInt.onInteraction -= SetPowerUp;
    }

    void Update()
    {

        //This is horrible, save me from this mess of GetComponent<>
        if (CoffeeInstance && CoffeeInstance.GetComponent<Animator>().GetInteger("StateNum")==1)
        {
            CoffeeInstance.GetComponent<Destroyables>().unitMat = powerUpMaterial;
            CoffeeInstance.GetComponent<Destroyables>().outlineMat = powerUpMaterial;

            if (CoffeeInstance.GetComponent<Animator>().GetBool("Destroyed"))
            {
                CoffeeInstance.GetComponent<Destroyables>().unitMat = regularMaterial;
                CoffeeInstance.GetComponent<Destroyables>().outlineMat = outlineMaterial;
            }
        }
        else if (CoffeeInstance && CoffeeInstance.GetComponent<Animator>().GetBool("Destroyed"))
        {
            CoffeeInstance.GetComponent<Destroyables>().unitMat = regularMaterial;
            CoffeeInstance.GetComponent<Destroyables>().outlineMat = outlineMaterial;
        }


    	StateNum = animator.GetInteger("StateNum");

		
		//If the current animation clip is CMAchineCompleteIdle and the CoffeeInstance exists then do stuff
        //--Needs to be before we create the coffee cup otherwise every new coffee cup will be ready instantly because the animator takes a few frames to catch up
        if (CoffeeInstance)
        {
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="CMachineCompleteIdle")
            {
                CoffeeInstance.GetComponent<Animator>().SetInteger("StateNum", 1);
                coffeeReady=true;
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

    //Wait a few seconds before being able to make another Coffee cup
    IEnumerator waitTime(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        timerUp=true;
    }

    public delegate void ApplyPlayerPowerUps(GameObject player, PowerUp powerType, float factor);
    public static event ApplyPlayerPowerUps ApplyPowerUps;
    //This is called whenever a player presses the "interact" button on this object
    private void SetPowerUp(GameObject player, GameObject interactionObj)
    {
        if (interactionObj==gameObject)
        {
            if (coffeeReady && 
            !CoffeeInstance.GetComponent<Animator>().GetBool("Destroyed") && 
            player.GetComponent<PlayerInt>().PlayerNumber==1) 
            {
                coffeeReady=false;
                Destroy(CoffeeInstance); //Destroy the object and set the animator state BEFORE we set the power-up
                animator.SetInteger("StateNum", 0);
                if (ApplyPowerUps !=null)
                {
                    ApplyPowerUps(player, PowerUp.Power, 2);
                    ApplyPowerUps(player, PowerUp.Speed, 2);
                }
                StartCoroutine(waitTime(timeTillNextCoffee));
            }
        }
    }
}