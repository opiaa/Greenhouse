using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISlider : MonoBehaviour
{
	private GameObject[] destroyables;
	public float interp = 25f;
	private float destroyed = 0f;
	private float numOfObjects = 0f;
	private float newVal;
	private Slider curSlider;

	private void Start()
	{
		//Get the slider component on the current object
		curSlider = GetComponent<Slider>();
		//Count how many destroyable objects exist in the world
		destroyables = GameObject.FindGameObjectsWithTag("Destroyables");

		Destroyables.onDestroyed+=updateNumDestroyed;
	}

	private void OnDisable()
	{
		Destroyables.onDestroyed-=updateNumDestroyed;
	}

	public void Update()
	{
		numOfObjects=0;
		foreach (GameObject i in destroyables)
		{
			numOfObjects++;
		}
		newVal = 1-(destroyed/numOfObjects);

		//---Simple interpolation
		if (curSlider.value != newVal)
		{
			curSlider.value += interp*(newVal-curSlider.value)*Time.deltaTime;
		}
	}

	public void updateNumDestroyed(GameObject obj, bool Destroyed)
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
