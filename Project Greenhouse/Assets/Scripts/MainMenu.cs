using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{	
	public AudioMixer audioMaster;
	private float audioLevel;
	public Animator transition;
	public GameObject HowToStarDuster;
	private float transitionTime=1f;
	public Slider VolSliderObj;

	private void Start() {
		//Set the volume bar equal to the volume
		float audioLevel;
        audioMaster.GetFloat("MusicVol", out audioLevel);
        audioLevel = Mathf.Pow(10, audioLevel/20);
        VolSliderObj.value=audioLevel;	
	}


	public void SetHowTo(bool setHowTo)
	{
		HowToStarDuster.SetActive(setHowTo);
	}

	public void PlayGame()
	{
		//UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
		StartCoroutine(LoadLevel("MainScene"));
	}

	public void GoMainMenu() 
	{
		//UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
		StartCoroutine(LoadLevel("MenuScene"));
	}

	public void QuitGame()
	{
		print("*quits game*");
		Application.Quit();
	}

	public void VolSlider(float sliderVal) 
	{
        audioMaster.SetFloat("MusicVol", Mathf.Log10(sliderVal)*20);
	}
	IEnumerator LoadLevel(string lvlName)
    {
        transition.SetTrigger("TransitionStart");   

        yield return new WaitForSeconds(transitionTime);

        UnityEngine.SceneManagement.SceneManager.LoadScene(lvlName);
    }
}
