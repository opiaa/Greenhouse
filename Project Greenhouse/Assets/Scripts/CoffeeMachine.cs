using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
	/*This is a script that could be greatly optimised, since most of these calculations are happening 
	every frame, which may not be necessary as there are OnStateUpdate/OnStateEnter/OnStateExist events */
	public GameObject CoffeePrefab;
	private Animator animator;
	private GameObject CoffeeInstance;
	private float timer;
	private int StateNum;

    void Start()
    {
    	timer = Time.fixedTime;
     	animator = GetComponent<Animator>();
    }

    void Update()
    {
    	StateNum = animator.GetInteger("StateNum");
    	//If the coffee machine is Idle and it hasn't made a coffee object yet
    	if (StateNum==0 && !CoffeeInstance)
		{
			//Make the empty coffee object and place it perfectly
			CoffeeInstance = Instantiate(CoffeePrefab, transform.position/*+new Vector3(0.05f,0f,0f)*/, Quaternion.Euler(0,0,0));
			//Set the animation to be Making the coffee
			animator.SetInteger("StateNum", StateNum+1);
		}
		
		//If the current animation clip is CMAchineCompleteIdle
		if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="CMachineCompleteIdle" && CoffeeInstance)
		{
			//Then change the coffee's animation to be ready
			CoffeeInstance.GetComponent<Animator>().SetInteger("StateNum", 1);
		}
    }
}