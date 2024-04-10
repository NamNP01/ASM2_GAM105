using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrap : MonoBehaviour
{
    public float HP = 3;
    public Animator anim;
    public GameObject[] itemPrefabs;
    public GameObject prefabPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject, 0.2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack"))
        {
            HP--;
            if (HP == 2)
            {
                anim.SetTrigger("Hit1");
                if (itemPrefabs.Length > 0)
                {
                    int randomIndex = Random.Range(0, itemPrefabs.Length);
                    GameObject randomItemPrefab = itemPrefabs[randomIndex];

                    if (randomItemPrefab != null)
                    {
                        GameObject newItem = Instantiate(randomItemPrefab, prefabPoint.transform.position, Quaternion.identity);
                    }
                }
            }
            if (HP == 1)
            {
                anim.SetTrigger("Hit2");
                int randomIndex = Random.Range(0, itemPrefabs.Length);
                GameObject randomItemPrefab = itemPrefabs[randomIndex];

                if (randomItemPrefab != null)
                {
                    GameObject newItem = Instantiate(randomItemPrefab, prefabPoint.transform.position, Quaternion.identity);
                }
            }
            if (HP == 0)
            {
                int randomIndex = Random.Range(0, itemPrefabs.Length);
                GameObject randomItemPrefab = itemPrefabs[randomIndex];

                if (randomItemPrefab != null)
                {
                    GameObject newItem = Instantiate(randomItemPrefab, prefabPoint.transform.position, Quaternion.identity);
                }
            }
        }
        if (collision.gameObject.tag.Equals("Skill"))
        {
            HP--;
            if (HP == 2)
            {
                anim.SetTrigger("Hit1");
                if (itemPrefabs.Length > 0)
                {
                    int randomIndex = Random.Range(0, itemPrefabs.Length);
                    GameObject randomItemPrefab = itemPrefabs[randomIndex];

                    if (randomItemPrefab != null)
                    {
                        GameObject newItem = Instantiate(randomItemPrefab, prefabPoint.transform.position, Quaternion.identity);
                    }
                }
            }
            if (HP == 1)
            {
                anim.SetTrigger("Hit2");
                int randomIndex = Random.Range(0, itemPrefabs.Length);
                GameObject randomItemPrefab = itemPrefabs[randomIndex];

                if (randomItemPrefab != null)
                {
                    GameObject newItem = Instantiate(randomItemPrefab, prefabPoint.transform.position, Quaternion.identity);
                }
            }
            if (HP == 0)
            {
                int randomIndex = Random.Range(0, itemPrefabs.Length);
                GameObject randomItemPrefab = itemPrefabs[randomIndex];

                if (randomItemPrefab != null)
                {
                    GameObject newItem = Instantiate(randomItemPrefab, prefabPoint.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
