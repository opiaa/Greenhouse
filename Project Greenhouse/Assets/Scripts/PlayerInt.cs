using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInt : MonoBehaviour
{
	private List<Collider2D> objD = new List<Collider2D>();
    public int PlayerNumber;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer==10) //Layer 10 is the "Destroyables" layer"
        {
            //Remember the current colliding object and make sure we know it's colliding
            objD.Add(col);
        }
    }

    //As soon as we're no longer colliding with the object, set the obj to false
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer==10) //Layer 10 is the "Destroyables" layer"
        {
            //Remember the current colliding object and make sure we know it's colliding
            objD.Remove(col);
        }    }
    
    //When the "fire" button is pressed & we're colliding with a valid object,
    //set it to "destroyed=false"
    public void OnFire(InputValue input)
    {
        if (input.Get<float>() == 1)
        {
            foreach (var col in objD)
            {
                if (PlayerNumber==1)
                {
                    col.gameObject.GetComponent<Destroyables>().DealDamage(false);
                }
                if (PlayerNumber==2)
                {
                    col.gameObject.GetComponent<Destroyables>().DealDamage(true);
                }
            }
        }
    }
}
