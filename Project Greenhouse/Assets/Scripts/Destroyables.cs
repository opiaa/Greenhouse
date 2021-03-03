using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyables : MonoBehaviour
{
	public Animator animator;
	public SpriteRenderer sprRender;
    public bool Destroyed = false;
    public bool Hovered = false;
    private bool hoveredP1 = false;
    private bool hoveredP2 = false;
    private Vector2 pos;
    private Shader unitSh;
    private Shader outlineSh;
    
    public void SetDestroyed(bool isDestroyed)
    {
            animator.SetBool("Destroyed", isDestroyed);
            Destroyed=isDestroyed;
    }

    void Start()
    {
    	sprRender = GetComponent<SpriteRenderer>();
    	unitSh = Shader.Find("Sprites/Default");
    	outlineSh = Shader.Find("Shader Graphs/shdOutline");
        pos = transform.position;
    }

    void Update()
    {
        transform.position = pos;

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
