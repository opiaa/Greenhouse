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
		//Get the volume from the PlayerPrefs
		if (PlayerPrefs.HasKey("MusicVol"))
			audioMaster.SetFloat("MusicVol",PlayerPrefs.GetFloat("MusicVol"));

		//Set the volume bar equal to the volume
		float curVol;
        audioMaster.GetFloat("MusicVol", out curVol);
        curVol = Mathf.Pow(10, curVol/20);
        VolSliderObj.value=curVol;	
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
		float newVol = Mathf.Log10(sliderVal) * 20;

		audioMaster.SetFloat("MusicVol", newVol);
		PlayerPrefs.SetFloat("MusicVol", newVol);
	}
	IEnumerator LoadLevel(string lvlName)
    {
        transition.SetTrigger("TransitionStart");   

        yield return new WaitForSeconds(transitionTime);

        UnityEngine.SceneManagement.SceneManager.LoadScene(lvlName);
    }
}
