using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ene : MonoBehaviour
{
    // Start is called before the first frame update
    private int direction = 1;
    private float moveSpeed = 2;
    public float a = 1.37f, b = -1.25f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector3(direction, 0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        if (transform.position.x > a )
        {
            direction *= -1;
            transform.localScale = new Vector2(1, 1);
        }
        if (transform.position.x <b)
        {
            direction *= -1;
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
