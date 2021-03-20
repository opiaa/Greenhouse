using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetPowerUp(GameObject player)
    {
        //And we're here!
        if (player.GetComponent<PlayerInt>().PlayerNumber==2) 
        {
            Destroy(gameObject); //Destroy the object and set the animator state BEFORE we set the power-up
            player.GetComponent<Player2DContr>().ApplyPowerup(PowerUp.Speed,2);
            player.GetComponent<Player2DContr>().ApplyPowerup(PowerUp.Power,2);
        }
    }
}
