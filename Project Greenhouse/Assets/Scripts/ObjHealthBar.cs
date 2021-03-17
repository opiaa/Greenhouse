using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHealthBar : MonoBehaviour
{
    private SpriteRenderer sprRender;
    public int timeToFade = 3;
    public List<Sprite> sprites;

    public void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
    }

    public void UpdateObjHealth(float health, float maxHealth)
    {
        //This calculates which sprite in the array should be shown, formatted weirdly for my tiny smooth brain
        sprRender.sprite=sprites
            [sprites.Count-1
            -((int)Mathf.Round(
                (health/maxHealth)*(sprites.Count-1)
                ))
            ];
        
        if (health>0 && health<maxHealth)
        {
            StopAllCoroutines();
        }
        else
        {
            StartCoroutine(fadeOut(timeToFade));
        }
    }

    IEnumerator fadeOut(int timeF)
    {
        //Destroy itself if it's just been sitting there with no information to show
        yield return new WaitForSeconds(timeF);
        Destroy(gameObject);
    }
}