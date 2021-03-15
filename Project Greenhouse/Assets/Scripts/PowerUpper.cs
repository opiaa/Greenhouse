using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpper : MonoBehaviour
{
    private Collider2D thisCol;
    //public enum PowerUp { Speed, Power };
    public PowerUp powerType;
    public enum PlayerLimit { None, Player1, Player2 };
    public PlayerLimit playerLimit = PlayerLimit.None;
    // Start is called before the first frame update
    void Start() 
    {
        thisCol = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (playerLimit)
        {
            case PlayerLimit.Player1:
                if (col.gameObject.name == "PlayerHumanoid")
                {
                    col.gameObject.GetComponent<Player2DContr>().ApplyPowerup(powerType);
                    Destroy(gameObject);
                }
                break;
            case PlayerLimit.Player2:
                if (col.gameObject.name == "PlayerPet")
                {
                    col.gameObject.GetComponent<Player2DContr>().ApplyPowerup(powerType);
                    Destroy(gameObject);
                }
                break;
            case PlayerLimit.None:
                col.gameObject.GetComponent<Player2DContr>().ApplyPowerup(powerType);   
                Destroy(gameObject);
                break;
        }
    }
}
