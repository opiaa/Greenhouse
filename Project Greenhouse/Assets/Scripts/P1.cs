using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P1 : MonoBehaviour
{
	private Collider2D objD;
    private bool onObjD = false;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer==10) //Layer 10 is the "Destroyables" layer"
        {
            //Remember the current colliding object and make sure we know it's colliding
            objD = col;
            onObjD=true;
            objD.gameObject.GetComponent<Destroyables>().SetHover(true, 1);
        }
    }

    //As soon as we're no longer colliding with the object, set the obj to false
    public void OnTriggerExit2D(Collider2D col)
    {
        if (onObjD)
        {
            onObjD = false;
            objD.gameObject.GetComponent<Destroyables>().SetHover(false, 1);
        }

    }
    
    //When the "fire" button is pressed & we're colliding with a valid object,
    //set it to "destroyed=false"
    public void OnFire(InputValue input)
    {
        if (input.Get<float>() == 1)
        {
            if (onObjD)
            {
                objD.gameObject.GetComponent<Destroyables>().SetDestroyed(false);
            }
        } 
    }
}
