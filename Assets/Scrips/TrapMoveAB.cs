using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMoveAB : MonoBehaviour
{
    // Start is called before the first frame update
    public int direction = 1;
    private float moveSpeed = 10;
    public float a = 1.37f, b = -1.25f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector3(0f, direction);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        if (transform.position.y >= a)
        {
            direction *= -1;
        }
        if (transform.position.y <= b)
        {
            direction *= -1;
        }
    }
}
