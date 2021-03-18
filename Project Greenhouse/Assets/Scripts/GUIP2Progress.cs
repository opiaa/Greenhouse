using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIP2Progress : MonoBehaviour
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
        slValue +=(1-ratioSlider.value)/50*Time.deltaTime;


        //This is where we'll decide when to draw which bit of the sprite
        switch (slValue)
        {
            case 1:
                //Player1 wins
                break;
            case float n when n >= 0.9:
                //Show the sprite animating and end the game
                newval=1f;
                break;
            case float n when n <= 0.2:
                newval=0.16f;
                break;
            case float n when n <= 0.4:
                    newval=0.28f;
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
