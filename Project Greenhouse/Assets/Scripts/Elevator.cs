using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Elevate(GameObject player)
    {
        if(player.transform.position.y < 1) // downstairs, move up
        {
            player.transform.position = new Vector3(gameObject.transform.position.x, 2.0f , player.transform.position.z);


        }
        else // upstairs, move down
        {
            player.transform.position = new Vector3(gameObject.transform.position.x, -1.22f, player.transform.position.z);

        }

    }
}
