using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInt : MonoBehaviour
{
	private List<Collider2D> objD = new List<Collider2D>(); //List of destroyables
    private List<Collider2D> objP = new List<Collider2D>(); //List of powerups
    private List<Collider2D> objE = new List<Collider2D>(); //List of elevators

    public int PlayerNumber;
    private int power = 1;
    public bool HoveringPowerUp { get; set; }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.layer)
        {
            case 10:
                //Remember the current colliding object and make sure we know it's colliding
                objD.Add(col);
                break;
            case 11:
                objP.Add(col);
                break;
            case 12:
                objE.Add(col);
                break;
            default:
                break;
        }
    }

    //As soon as we're no longer colliding with the object, set the obj to false
    public void OnTriggerExit2D(Collider2D col)
    {

        switch (col.gameObject.layer)
        {
            case 10://Layer 10 is the "Destroyables" layer
                objD.Remove(col);
                break;
            case 11:
                objP.Remove(col);
                break;
            case 12:
                objE.Remove(col);
                break;
            default:
                break;
        }
    }
    
    //When the "fire" button is pressed & we're colliding with a valid object,
    //trigger a hit, where we pass a bool to let the game know wether we're destroying an object or not (so it's repairing) the object
    public void OnFire(InputValue input)
    {
        if (input.Get<float>() == 1)
        {
            foreach (var col in objD)
            {
                if (col)
                {
                    if (PlayerNumber==1)
                    {
                        DoHit(col.gameObject, power, false);
                    }
                    if (PlayerNumber==2)
                    {
                        DoHit(col.gameObject, power, true);
                    }
                }
                else
                {
                    objD.Remove(col);
                }
            }
        }
    }

    public void OnInteract(InputValue input)
    {
        if (input.Get<float>() != 0) 
        {
            foreach (var col in objP)
            {
                if (col.gameObject.GetComponent<CoffeeMachine>()) 
                {
                    col.gameObject.GetComponent<CoffeeMachine>().SetPowerUp(gameObject);
                }
            }

            if (objE.Count > 0)
            {
                if (objE[0].gameObject.GetComponent<Elevator>()) {
                    objE[0].gameObject.GetComponent<Elevator>().Elevate(gameObject);
                }

            }
        }
    }

    //This is called when a PowerUp.Power thing is called, it simply sets this boi's "power" variable
    public void SetPower(int newPower)
    {
        power = newPower;
    }

    //For each "power" then deal damage or heal the object that number of times
    //This is ever so slighlty easier than just doing the maths in the Destroyables script
    public void DoHit(GameObject obj, int repetitions, bool destroying)
    {
        for (int i = 0; i < repetitions; i++)
        {
            obj.GetComponent<Destroyables>().DealDamage(destroying);
        }
    }
}
