using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	private GameObject[] destroyables;
	public float interp = 0.5f;
	private float destroyed = 0f;
	private float numOfObjects = 0f;
	private float newVal;
	private Slider slider;

	private void Start()
	{
		//Get the slider component on the current object
		slider = GetComponent<Slider>();
		//Count how many destroyable objects exist in the world
		destroyables = GameObject.FindGameObjectsWithTag("Destroyables");
		foreach (GameObject i in destroyables)
		{
			numOfObjects++;
		}
	}

	public void Update()
	{
		newVal = 1-destroyed/numOfObjects;

		//---Simple linear interpolation
		//If the new value is lower than the current slider value then add a little bit to it 
		if (newVal < slider.value)
		{
			slider.value = slider.value - (interp* Time.deltaTime);
		}
		//Otherwise if the new value is higher than the current slider value then subtract a little bit from it
		else if (newVal > slider.value)
		{
			slider.value = slider.value + (interp * Time.deltaTime);
		}

		//If it's close enough (within 0.1) then just set it to be equal to the new value
		//to stop it getting stuck in a loop if the values don't match perfectly
		if (newVal - slider.value < 0.01 && newVal - slider.value > 0 )
		{
			slider.value = newVal;
		} 
		else if (slider.value - newVal < 0.01 && slider.value - newVal > 0 )
		{
			slider.value = newVal;
		}
	}

	public void updateNumDestroyed(bool Destroyed)
	{
		//This is called when a "Destroyable" object is destroyed by the animal player
		if (Destroyed)
		{
			destroyed++;;
		}
		else
		{
			destroyed--;
		}
	}
}
