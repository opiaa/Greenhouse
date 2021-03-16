using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyables : MonoBehaviour
{
	private Animator animator;
	private SpriteRenderer sprRender;
    private GameObject scoreboard;
    private Collider2D dCollider;
    private Vector2 pos;
    private bool Destroyed = false;
    public Shader unitSh;
    public Shader outlineSh;
    public GameObject hitParticles;
    public Vector3 hitParticleOffset = new Vector3(0f,0f,0f);
    public GameObject healthBarPrefab;
    private GameObject healthBar;
    public Vector3 healthBarOffset = new Vector3(0f,1f,0f);
    public int healthMax = 3;
    public int currentHealth = 3;

    private void Start()
    {
        //Get this objects Animator, Renderer, the Scene's progress bar and the mentioned Shaders
        animator = GetComponent<Animator>();
    	sprRender = GetComponent<SpriteRenderer>();
        dCollider = GetComponent<Collider2D>();
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
            //Spawn some particles
            Instantiate(hitParticles, transform.position+hitParticleOffset, Quaternion.identity);

            //Only change the "destroyed" value if either max or 0 is reached
            if (currentHealth == 0 || currentHealth == healthMax)
            {
                animator.SetBool("Destroyed", isDestroying);
                Destroyed = isDestroying;
                scoreboard.GetComponent<GUISlider>().updateNumDestroyed(isDestroying);
            }
        }

        //Show/change the object's health bar
        if (healthBar)
        {
            healthBar.GetComponent<ObjHealthBar>().UpdateObjHealth(currentHealth, healthMax);
        }
        else
        {
            healthBar = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity);
            healthBar.GetComponent<ObjHealthBar>().Start(); //Why doesn't it run the Start() function before moving to the next line?!?!?!?!?!!?
            healthBar.GetComponent<ObjHealthBar>().UpdateObjHealth(currentHealth, healthMax);
        }


    }

    private void Update()
    {
        //Make it not move... ever...
        //transform.position = pos;
        //Switch between shaders depending on who's hovering and the obj state
        if (dCollider.IsTouching(GameObject.Find("PlayerHumanoid").GetComponent<Collider2D>()) && (Destroyed || healthMax-currentHealth>0))
        {
            sprRender.material.shader = outlineSh;
        }
        else if (dCollider.IsTouching(GameObject.Find("PlayerPet").GetComponent<Collider2D>()) && (!Destroyed || healthMax-currentHealth<healthMax))
        {
            sprRender.material.shader = outlineSh;
        }
        else
        {
            sprRender.material.shader = unitSh;
        }
    }

    void OnDestroy() {
        if (healthBar)
        {
            Destroy(healthBar);
        }
    }
}
