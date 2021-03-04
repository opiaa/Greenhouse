using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
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

    void Update()
    {
        //Make it not move... ever...
        transform.position = pos;

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
    
    //This is called when a player interacts with the obj
    public void SetDestroyed(bool isDestroyed)
    {
            animator.SetBool("Destroyed", isDestroyed);
            Destroyed=isDestroyed;
            scoreboard.GetComponent<GameUI>().updateNumDestroyed(isDestroyed);
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
}
