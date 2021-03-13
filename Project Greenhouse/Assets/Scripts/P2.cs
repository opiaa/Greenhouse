using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2 : MonoBehaviour
{
	private Collider2D objDestroyable;
    private bool onObjDstr = false;

	public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer==10) //Layer 10 is the "Destroyables" layer"
        {
            //Remember the current colliding object and make sure we know it's colliding
            objDestroyable = col;
            onObjDstr=true;
            objDestroyable.gameObject.GetComponent<Destroyables>().SetHover(true, 2);
        }
    }

    //As soon as we're no longer colliding with the object, set the obj to false
    public void OnTriggerExit2D(Collider2D col)
    {
        if (onObjDstr)
        {
            onObjDstr = false;
            objDestroyable.gameObject.GetComponent<Destroyables>().SetHover(false, 2);
        }

    }

    //When the "fire" button is pressed & we're colliding with a valid object,
    //set it to "destroyed=true"
    public void OnFire(InputValue input)
    {
        if (input.Get<float>() == 1)
        {
            if (onObjDstr)
            {
                objDestroyable.gameObject.GetComponent<Destroyables>().DealDamage(true);
                GetComponent<Player2DContr>().ApplyPowerup(PowerUp.Speed);
            }
        } 
    }
}
