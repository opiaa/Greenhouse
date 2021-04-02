using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePwrUp : BasicPowerUp
{
    void Start()
    {
        PlayerInt.onInteraction += SetPowerUp;
    }
    void OnDisable()
    {
        PlayerInt.onInteraction -= SetPowerUp;
    }

}
