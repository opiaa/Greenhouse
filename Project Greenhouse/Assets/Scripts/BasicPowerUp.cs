using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPowerUp : MonoBehaviour
{

    void Start()
    {
        PlayerInt.onInteraction += SetPowerUp;
    }
    void OnDisable()
    {
        PlayerInt.onInteraction -= SetPowerUp;
    }

    public int PlayerLock;
    public string ClipNameLock="Idle";
    public Animator _animator;


    public delegate void ApplyPlayerPowerUps(GameObject player, PowerUp powerType, float factor);
    public static event ApplyPlayerPowerUps ApplyPowerUps;

    public void SetPowerUp(GameObject player, GameObject interactionObj)
    {
        if (!_animator && GetComponent<Animator>())
        {
            _animator = GetComponent<Animator>();
        }

        
        if (PlayerLock==0)
        {
            Debug.LogWarning("No PlayerLockSet, allowing all players to interact");
        }
        if (interactionObj == gameObject) //If this is the object they're interacting with
        {
            if (_animator && (_animator.GetBool("Destroyed") || _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains(ClipNameLock)))
            {
                return;
            }
            if (PlayerLock==0 || player.GetComponent<PlayerInt>().PlayerNumber==PlayerLock) //If it's the right player
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
