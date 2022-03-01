using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIP2Progress : MonoBehaviour
{
    public List<Sprite> petFaceSprites;
    public Slider ratioSlider;
    private float slValue = 0;
    private Image _image;



    void Start()
    {
        _image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        slValue +=(1-ratioSlider.value)/75*Time.deltaTime;


        //This is where we'll decide when to draw which sprite
        switch (slValue)
        {
            case float n when n >=1:
                //Player2 wins
                UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScreenP2");
                break;
            case float n when n >= 0.9:
                //Show the sprite animating and end the game
                _image.sprite=petFaceSprites[4];
                break;
            case float n when n <= 0.2:
                _image.sprite=petFaceSprites[0];
                break;
            case float n when n <= 0.4:
                _image.sprite=petFaceSprites[1];
                break;
            case float n when n <= 0.6:
                _image.sprite=petFaceSprites[2];
                break;
            case float n when n <= 0.8:
                _image.sprite=petFaceSprites[3];
                break;
        }

        //_slider.value += interval*(newval-_slider.value)*Time.deltaTime;

    }
}
