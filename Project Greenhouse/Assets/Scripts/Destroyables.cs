using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyables : MonoBehaviour
{
	public Animator animator;
	public SpriteRenderer sprRender;
    public bool Destroyed = false;
    public bool Hovered = false;
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
    }

    public void SetHover(bool isHovered, int playerID)
    {
        if (isHovered && Destroyed && playerID==1)
        {
            sprRender.material.shader = outlineSh;

        }
        else if (isHovered && !Destroyed && playerID==2)
        {
            sprRender.material.shader = outlineSh;        
        }
        else
        {
            sprRender.material.shader = unitSh;
        }
    }
}
