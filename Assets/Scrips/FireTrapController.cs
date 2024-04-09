using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapController : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject firepoint;
    public GameObject fireprefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(DelayFireOn(1f));
        }
    }
    IEnumerator DelayFireOn(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Fire();
    }
    private void Fire()
    {
        GameObject newFire = Instantiate(fireprefab, firepoint.transform.position, Quaternion.identity);
        Rigidbody2D FireRb = newFire.GetComponent<Rigidbody2D>();
    }
    IEnumerator DelayFireOff(float delayTime)
    {   
        yield return new WaitForSeconds(delayTime);
    }
}
