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

        //This is where we'll decide when to end the game
            if (slValue >= 1)
            {
                //Player1 wins
                UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScreenP1");
            }
        _slider.value += interval*(slValue-_slider.value)*Time.deltaTime;

    }
}
