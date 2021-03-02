using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2 : MonoBehaviour
{
	private bool firing;

	public void OnTriggerStay2D(Collider2D col)
    {
    	print(col.gameObject.name);
        if (firing)
        {
            if (col.gameObject.layer==10)
            {
                col.gameObject.GetComponent<Destroyables>().SetDestroyed(true);
                firing = false;
            }
        }

        firing=false;
    }

    public void OnFire(InputValue input)
    {
    	print(input.Get<float>());
        if (input.Get<float>() == 1)
        {
            firing = true;
        } 
        else
        {
            firing = false;
        }
    }
}
