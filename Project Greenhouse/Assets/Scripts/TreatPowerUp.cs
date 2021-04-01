using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatPowerUp : MonoBehaviour
{

    void Start()
    {
        PlayerInt.onInteraction += SetPowerUp;
    }
    void OnDisable()
    {
        PlayerInt.onInteraction -= SetPowerUp;

    }
    public delegate void ApplyPlayerPowerUps(GameObject player, PowerUp powerType, float factor);
    public static event ApplyPlayerPowerUps ApplyPowerUps;
    public void SetPowerUp(GameObject player, GameObject interactionObj)
    {
        if (interactionObj == gameObject)
        {
            if (player.GetComponent<PlayerInt>().PlayerNumber==2) 
            {
                if (ApplyPowerUps!=null)
                {
                    Destroy(gameObject); //Destroy the object and set the animator state BEFORE we set the power-up
                    ApplyPowerUps(player, PowerUp.Speed,2);
                    ApplyPowerUps(player, PowerUp.Power,2);
                }
            }
        }
    }
}
