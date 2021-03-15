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

    private int StateNum;
	public PowerUp powerType;
	public enum PlayerLimit { None, Player1, Player2 };
	public PlayerLimit playerLimit = PlayerLimit.None;

    void Start()
    {
     	animator = GetComponent<Animator>();
        thisCol = GetComponent<Collider2D>();

    }

    void Update()
    {
    	StateNum = animator.GetInteger("StateNum");
    	//If the coffee machine is Idle and it hasn't made a coffee object yet
    	if (StateNum==0 && !CoffeeInstance)
		{
			//Make the empty coffee object and place it perfectly
			CoffeeInstance = Instantiate(CoffeePrefab, transform.position+new Vector3(0.01f,-0.01f,0f), Quaternion.Euler(0,0,0));
			//Set the Coffee Machine's animation to be making the coffee
			animator.SetInteger("StateNum", StateNum+1);
		}
		
		//If the current animation clip is CMAchineCompleteIdle
		if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="CMachineCompleteIdle" && CoffeeInstance)
		{
			//Then change the coffee's animation to be ready
			CoffeeInstance.GetComponent<Animator>().SetInteger("StateNum", 1);
		}
		//If there's no coffee, then make another one
		else if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="CMachineCompleteIdle" && !CoffeeInstance)
		{
			animator.SetInteger("StateNum", 0);
		}
    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "CMachineCompleteIdle" && CoffeeInstance)
        {
            switch (playerLimit)
            {
                case PlayerLimit.Player1:
                    if (col.gameObject.name == "PlayerHumanoid")
                    {
                        col.gameObject.GetComponent<Player2DContr>().ApplyPowerup(powerType);
                        Destroy(CoffeeInstance);
                    }
                    break;
                case PlayerLimit.Player2:
                    if (col.gameObject.name == "PlayerPet")
                    {
                        col.gameObject.GetComponent<Player2DContr>().ApplyPowerup(powerType);
                        Destroy(CoffeeInstance);
                    }
                    break;
                case PlayerLimit.None:
                    col.gameObject.GetComponent<Player2DContr>().ApplyPowerup(powerType);
                    Destroy(CoffeeInstance);
                    break;
            }
        }
    }
}