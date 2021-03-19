using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIP1Progress : MonoBehaviour
{
    private Slider _slider;
    public Slider ratioSlider;
    private float slValue = 0;
    private float newval;
    private float interval = 25f;
    public GameObject ObjImage; 
    private Image _image;



    void Start()
    {
        _slider = GetComponent<Slider>();
        _image = ObjImage.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        slValue +=(ratioSlider.value)/50*Time.deltaTime;

        //This is where we'll decide when to draw which bit of the sprite
        switch (slValue)
        {
            case float n when n>= 1:
                //Player1 wins
                UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScreenP1");
                break;
            case float n when n >= 0.9:
                //Show the particles animating and end the game
                newval=1f;
                break;
            case float n when n <= 0.2:
                newval=0.15f;
                break;
            case float n when n <= 0.4:
                    newval=0.26f;
                break;
            case float n when n <= 0.6:
                newval=0.5f;
                break;
            case float n when n <= 0.8:
                newval=0.68f;
                break;
        }
        _slider.value += interval*(newval-_slider.value)*Time.deltaTime;

    }
}
