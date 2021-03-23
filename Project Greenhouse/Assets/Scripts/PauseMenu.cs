using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer audioMaster;
    public Slider VolSliderObj;
    public Animator transition;
    private float transitionTime = 1f;


    void Start() 
    {
        //Set the volume bar equal to the volume
        float audioLevel;
        audioMaster.GetFloat("MusicVol", out audioLevel);
        audioLevel = Mathf.Pow(10, audioLevel/20);
        VolSliderObj.value=audioLevel;
    }

    void Update()
    {

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }  
    }
    
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale=1f;
        GameIsPaused=false;
    }

    public void Pause() 
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale=0f;
        GameIsPaused=true;
    }

    public void MainMenu()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
        Time.timeScale=1f;
        StartCoroutine(LoadLevel("MenuScene"));
    }

    public void VolSlider(float sliderVal)
    {
        audioMaster.SetFloat("MusicVol", Mathf.Log10(sliderVal)*20);
    }

    public void FSToggle(bool isFullscreen) 
    {
        Screen.fullScreen = isFullscreen;
    }

    IEnumerator LoadLevel(string lvlName)
    {
        transition.SetTrigger("TransitionStart");   

        yield return new WaitForSeconds(transitionTime);

        UnityEngine.SceneManagement.SceneManager.LoadScene(lvlName);
    }
}
