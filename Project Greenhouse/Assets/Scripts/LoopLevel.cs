using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopLevel : MonoBehaviour
{
	private Collider2D thisCol;
	private Collider2D looperCollider;
	private SpriteRenderer sprRender;
	public GameObject clonePrefab;
	private GameObject cloneSpr;
	private float loopDifference;

    void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
        thisCol = GetComponent<Collider2D>();
    }


    void Update()
    {
    	//If there's a clone sprite, then update it to be exactly the same as the real object
        if (cloneSpr)
        {
        	cloneSpr.GetComponent<SpriteRenderer>().sprite = sprRender.sprite;
        	cloneSpr.GetComponent<SpriteRenderer>().flipX = sprRender.flipX;

        	cloneSpr.transform.position = new Vector3(
        		transform.position.x-loopDifference,
        		transform.position.y,
        		transform.position.z);

        	//If you've moved into the looper a distance of 3, then teleport the player back a bit
        	if (thisCol.Distance(looperCollider).distance < -3)
        	{
        		transform.position = new Vector3(
    			transform.position.x+loopDifference*-1,
    			transform.position.y,
    			transform.position.z);
        	}
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {
    	//If you've collided with a looper, and there's no clone, then make one
    	if (col.tag == "Loopers")
    	{
	    	if (!cloneSpr)
	    	{
	    		loopDifference = col.transform.position.x;
	    		looperCollider = col;
	    		Vector3 newPos = new Vector3(
	    			transform.position.x-loopDifference, 
	    			transform.position.y-col.transform.position.y, 
	    			transform.position.z-col.transform.position.z);

	    		cloneSpr = Instantiate(clonePrefab, newPos, new Quaternion(0f,0f,0f,0f));
	    		cloneSpr.layer = gameObject.layer;
	    		cloneSpr.GetComponent<SpriteRenderer>().sortingOrder = sprRender.sortingOrder;
	    	}
	    }
    }

    void OnTriggerExit2D(Collider2D col)
    {
    	//Once we don't need the clone any more, delete it and reset everything else
    	if (col.tag == "Loopers")
	    {
	    	if (cloneSpr)
	    	{
	    		looperCollider = null;
	    		loopDifference = 0f;
	    		Destroy(cloneSpr);
	    	}
	    }
    }
}
