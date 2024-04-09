using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireInFireTrap : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(DestroyAfterDelay(2f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

