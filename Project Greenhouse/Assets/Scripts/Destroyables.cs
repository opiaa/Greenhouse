using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyables : MonoBehaviour
{
	private Animator animator;
	private SpriteRenderer sprRender;
    private GameObject scoreboard;
    private bool hoveredP1 = false;
    private bool hoveredP2 = false;
    private Vector2 pos;
    private bool Destroyed = false;
    public Shader unitSh;
    public Shader outlineSh;
    public int healthMax = 3;
    public int currentHealth = 3;

    private void Start()
    {
        //Get this objects Animator, Renderer, the Scene's progress bar and the mentioned Shaders
        animator = GetComponent<Animator>();
    	sprRender = GetComponent<SpriteRenderer>();
        scoreboard = GameObject.Find("ProgressSlider");
    	//unitSh = Shader.Find("Sprites/Default");
    	//outlineSh = Shader.Find("Shader Graphs/shdOutline");
        //Store the current position so that we can make it never move
        pos = transform.position;
    }
    
    //This is called when a player interacts with the obj
    public void DealDamage(bool isDestroying, int damageDealt = 1)
    {
        // If case that makes sure no action is taken if the object is already "destroyed" and is being hit again, ditto for objects at max health being "healed"
        if (!((currentHealth == 0) && isDestroying) && !((currentHealth == healthMax) && !isDestroying))
        {
            //Subtract or add the damage from/to the health counter
            currentHealth += damageDealt * (isDestroying ? -1 : 1);
            
            //Only change the "destroyed" value if either max or 0 is reached
            if (currentHealth == 0 || currentHealth == healthMax)
            {
                animator.SetBool("Destroyed", isDestroying);
                if (Destroyed != isDestroying)
                {
                    Destroyed = isDestroying;
                    scoreboard.GetComponent<GameUI>().updateNumDestroyed(isDestroying);
                }
            }
        }

    }

    //This is called when a player hovers over the obj
    public void SetHover(bool isHovered, int playerID)
    {
        if (playerID==1)
        {
            hoveredP1=isHovered;
        }
        else if (playerID==2)
        {
            hoveredP2=isHovered;
        }
    }

    private void Update()
    {
        //Make it not move... ever...
        //transform.position = pos;

        //Switch between shaders depending on who's hovering and the obj state
        if (hoveredP1 && Destroyed)
        {
            sprRender.material.shader = outlineSh;
        }
        else if (hoveredP2 && !Destroyed)
        {
            sprRender.material.shader = outlineSh;
        }
        else
        {
            sprRender.material.shader = unitSh;
        }
    }
}
