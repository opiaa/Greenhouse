using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTreatsBag : MonoBehaviour
{
    public GameObject TreatsPrefab;
    private GameObject TreatsInstance;
    private int timeTilNextTreat = 0;
    private Animator _animator;
    private Collider2D _collider;
    public Material powerUpMaterial;
    public Material outlineMaterial;
    public Material regularMaterial;
    public bool destroyedLastFrame = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!destroyedLastFrame && !TreatsInstance)
        {
            destroyedLastFrame=true;
            StartCoroutine(waitTime(timeTilNextTreat));
        }

        //If there's no treat and we're destroyed then create a Treat Instance, otherwise delete the instance
        if (timeTilNextTreat < 1 && !TreatsInstance && _animator.GetBool("Destroyed"))
        {
			TreatsInstance = Instantiate(TreatsPrefab, transform.position+new Vector3(-0.01f,0f,0f), Quaternion.identity);
            timeTilNextTreat=5;
            destroyedLastFrame=false;
        } else if (TreatsInstance && !_animator.GetBool("Destroyed"))
        {
            Destroy(TreatsInstance);
        }

    }


    IEnumerator waitTime(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        timeTilNextTreat=0;
    }
}
