using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	private GameObject[] destroyables;
	public float interp = 25f;
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
		if (slider.value != newVal)
		{
			slider.value = slider.value + interp*(newVal-slider.value)*Time.deltaTime;
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
