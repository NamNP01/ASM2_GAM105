using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Security.Cryptography.X509Certificates;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Attack")]
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public GameObject firePrefab;
    public GameObject attackPrefab;
    private Vector2 fireDirection;
    public float fireSpeed = 8;

    [Header("Player")]
    public Animator anim;
    public float Score = 0;
    public Text ScoreText;
    public int maxHealth = 100;
    public int maxMana=10;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int currentMana;

    [Header("BarManager")]
    public HealthBar healthBar;
    public manaBar ManaBar;
    public GameObject _GameOver;
    public bool IsPaused;
    private float safeTime;
    public float safeTimeDuration = 0f;

    [Header("Audio")]
    public AudioSource src;
    public AudioClip  skill;


    [Header("Fire")]
    public Image abilityImage2;
    public Text abilityText2;
    public KeyCode ability2Key;
    public float ability2Cooldown = 8;
    private float currentAbility2Cooldown;
    private bool isAbility2Cooldown;
    public float attackRate = 1f;
    float nextAttackTime = 0f;
    [Header("Data")]
    public PlayerData playerData;


    private void Start()
    {

        LoadPlayerData();

        currentHealth = maxHealth;
        currentMana = maxMana;


        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
        if (ManaBar != null)
            ManaBar.UpdateMana(currentMana, maxMana);

        abilityImage2.fillAmount = 0;

        abilityText2.text = "";

    }

    public void ManaCost(int mana)
    {
        currentMana -= mana;
        
        if (ManaBar != null)
            ManaBar.UpdateMana(currentMana, maxMana);
    }
    public void TakeDam(int damage)
    {
        if (safeTime <= 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                anim.SetTrigger("Death");
                StartCoroutine(GameOverAfterDelay(2f));
            }

            // If player then update health bar
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
        }
    }
    private void Update()
    {
        Debug.Log("da vao update");

        if (Input.GetKeyDown(KeyCode.A))
        {
            fireDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            fireDirection = Vector2.right;
        }

        if (Time.time >= nextAttackTime)
        {
            if ( Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
       

        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && currentMana>=3 && !isAbility2Cooldown)
        {
            src.clip = skill;
            src.Play();
            isAbility2Cooldown = true;
            currentAbility2Cooldown = ability2Cooldown;
            ManaCost(3);
            CastSpell();
        }


        AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, abilityImage2, abilityText2);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        if (collision.gameObject.tag.Equals("Enemies"))
        {
            anim.SetTrigger("Hurt");
            TakeDam(20);
        }
        if (collision.gameObject.tag.Equals("NextLevel"))
        {
            Debug.Log("xxx");
            LoadNextScene();
        }

        if (collision.gameObject.tag.Equals("Score"))
        {
            Destroy(collision.gameObject);
            playerData.playerLevel++;
            ScoreText.text = " " + playerData.playerScore.ToString();
        }
        if (collision.gameObject.tag.Equals("HP"))
        {
            Destroy(collision.gameObject);
            TakeDam(-20);
        }
        if (collision.gameObject.tag.Equals("MN"))
        {
            Destroy(collision.gameObject);
            ManaCost(-2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag.Equals("JumpPad"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 40f);
        }
        if (collision.gameObject.tag.Equals("Enemies"))
        {

            anim.SetTrigger("Hurt");
            TakeDam(20);
        }
    }

    public void LoadNextScene()
    {
        Debug.Log("da vao load next");
        playerData.playerLevel++;
        playerData.playerScore += 0;
        // lưu thông tin playerLevel vào PlayerPrefs
        PlayerPrefs.SetInt("PlayerScore", playerData.playerScore);
        PlayerPrefs.SetInt("PlayerLevel", playerData.playerLevel); 
        
        //Debug.Log("Score game" + playerData.playerScore.ToString());
        //Debug.Log("playerData.playerScore" + playerData.playerScore.ToString());

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadThisScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void CastSpell()
    {
        anim.SetTrigger("CastSpell");
        GameObject newBullet = Instantiate(firePrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {

            bulletRb.velocity = fireDirection * fireSpeed;
        }
    }

   
    void Attack()
    {
        anim.SetTrigger("Attack");
        GameObject newBullet = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
    }


    public void gameOver()
    {
        StartCoroutine(GameOverAfterDelay(1f));
        _GameOver.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
    IEnumerator GameOverAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameOver();
    }

    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, Text skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if (skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }
                if (skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }
            }
        }
    }

    void LoadPlayerData()
    {
        // Đọc dữ liệu người chơi từ file lưu trữ
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            playerData.playerLevel = PlayerPrefs.GetInt("PlayerLevel");
            playerData.playerScore = PlayerPrefs.GetInt("Score");
            ScoreText.text = " " + playerData.playerScore.ToString();
            Debug.Log("Player data loaded.");

        }
        else
        {
            //Debug.LogWarning("Player data not found. Starting with default values.");
            // Gán giá trị mặc định nếu không tìm thấy dữ liệu người chơi
            playerData.playerLevel = 0;
            playerData.playerScore = 0;
        }

    }
}

