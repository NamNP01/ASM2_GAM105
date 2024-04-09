using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : MonoBehaviour
{
    public float HP;
    public GameObject ScorePrefab;
    public Animator anim;
    private bool isDead = false;
    public GameObject[] itemPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack"))
        {
            HP--;
            if (HP <= 0)
            {
                Die();
            }
        }
        if (collision.gameObject.tag.Equals("Skill"))
        {
            HP-=5;
            if (HP <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        if (isDead)
            return;

        anim.SetTrigger("Die");
        isDead = true;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
            collider.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        Destroy(gameObject, 0.2f);

        if (itemPrefabs.Length > 0)
        {
            // Chọn ngẫu nhiên một vật phẩm từ danh sách
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject randomItemPrefab = itemPrefabs[randomIndex];

            if (ScorePrefab != null)
            {
                GameObject newItem = Instantiate(ScorePrefab, transform.position, Quaternion.identity);
            }

            if (randomItemPrefab != null)
            {
                GameObject newItem = Instantiate(randomItemPrefab, transform.position, Quaternion.identity);
            }
        }
    }


}
