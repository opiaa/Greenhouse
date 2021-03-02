using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyables : MonoBehaviour
{
	public Animator animator;
    public bool Destroyed = false;
    private Vector2 pos;


    public void SetDestroyed(bool isDestroyed)
    {
        animator.SetBool("Destroyed", isDestroyed);
        Destroyed=isDestroyed;
    }

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.position = pos;
    }
}
