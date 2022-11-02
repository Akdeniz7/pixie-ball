using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent (typeof (Rigidbody2D))]
public class TopunKodu : MonoBehaviour
{
    Rigidbody2D body2D;
    CircleCollider2D cir2D;
    [Tooltip("Karakterin hızını belirler.")]
    [Range(0,20)]
    public float playerspeed;
    [Tooltip("Karakterin ne kadar yüksege zıplayacagını belirler.")]
    [Range(0,1500)]
    public float jumppower;
    [Tooltip("Karakterin yere degip  degmeyecegini belli eder.")]
    public bool isGrounded;
    Transform groundCheck;
    const float GroundCheckRadius = 0.2f;
    [Tooltip("Yerin ne olduğunu belirler.")]
    public LayerMask groundLayer;
    Vector3 Fark;
    public GameObject Kamera;
    Vector3 Toplam;
    // Karakteri Döndürme
    bool facingright= true;
    Animator playerAnimController;
    // Oyuncu Canı
    internal int maxPlayerHealth = 3;
    public int currentPlayerHealth;
    internal bool ishurt;
    GiveDamage givedamage;
    internal bool isDead;
    public float deadforce;
    public float knockBackForce;
    GameManager gameManager;
    //puanlama
    internal int currentPoints;
    internal int point = 10;
    AudioSource auSource;
    AudioClip au_Jump;
    AudioClip au_Hurt;
    AudioClip au_PickupCoin;
    AudioClip ac_Die;
    [Header("Bolum Sonu Panel")]
    public GameObject bolumSonu_P;
    [Space]
    SahneGecis sahneGecis;
    public string orjinal;
    internal bool canDamage;

    public void sonrakiLevelKontrolcusu()
    {
        string currentLevel = levelAdi(SceneManager.GetActiveScene().buildIndex);// (PlayerPrefs.GetString("suankiSecilenLevel");) Yenilendi çünkü level ekranından geçişte kaydettiğimiz leveli alıyorduk fakat sonraki levele bu sahneden geçince kayıtlı level eskisi kalıyor o yüzden direk aktif sahne build indexinden adını çağırıp işlem yaptırıyoruz.
        int currentLevelID = int.Parse(currentLevel.Split('_')[1]); //Level_id biçiminde olduğundan sağtaraf yani (id) 
        int nextLevel = PlayerPrefs.GetInt("level") + 1;

        if (currentLevelID == PlayerPrefs.GetInt("seviyeSayisi"))
        {
            Debug.Log("Oyun Sonu");

            
        }
        else
        {
            if (nextLevel - currentLevelID == 1)
                PlayerPrefs.SetInt("level", nextLevel);
            else
                Debug.Log("Önceden Açılmış bir bölüme girdiniz.");

                   
        }

        SceneManager.LoadScene(1); //level seçim ekranı
    }




    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        body2D.gravityScale = 5;
        body2D.freezeRotation = true;
        body2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        cir2D = GetComponent<CircleCollider2D>();
        
        groundCheck = transform.Find("GroundCheck");
        playerAnimController = GetComponent<Animator>();
        currentPlayerHealth = maxPlayerHealth;
        givedamage = FindObjectOfType<GiveDamage>();
        gameManager = FindObjectOfType<GameManager>();
        //Sound Effects
        auSource = GetComponent<AudioSource>();
        au_Jump = Resources.Load("SoundEffects/Jump") as AudioClip;
        au_Hurt = Resources.Load("SoundEffects/HitHurt") as AudioClip;
        au_PickupCoin = Resources.Load("SoundEffects/PickupCoin") as AudioClip;
        ac_Die = Resources.Load("SoundEffects/Die") as AudioClip;
        sahneGecis = GameObject.Find("GameManager").GetComponent<SahneGecis>();

    }
    void Update()
    { 
        UpdateAnimations();
        ReduceHealth();
        if (currentPlayerHealth > maxPlayerHealth)
            currentPlayerHealth = maxPlayerHealth;
        isDead = currentPlayerHealth <= 0;
        if (isDead)
            KillPlayer();

    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, groundLayer);
        if (isGrounded)
            canDamage = false;
        else
            canDamage = true;
       // body2D.velocity = new Vector2(h * playerspeed, body2D.velocity.y);
        //Flip(h);

    }
    public void Move(bool right)
    {
        if (right)
        {
            body2D.velocity = new Vector2(playerspeed, body2D.velocity.y);
            Flip(1);
        }
        else
        {
            body2D.velocity = new Vector3(-playerspeed, body2D.velocity.y);
            Flip(-1);
        }
        
    }
    public void ZeroVelocity()
    {
        body2D.velocity = Vector2.zero;
    }
    public void Jump()
    {
        if(isGrounded)
        {
            body2D.AddForce(new Vector2(0, jumppower));
            auSource.PlayOneShot(au_Jump);
            auSource.pitch = Random.Range(0.9f, 1.1f);
            canDamage = true;
        }
        
    }
    void Flip(float h) {
        if (h > 0 && !facingright || h<0 && facingright)
        {
            facingright = !facingright;
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

        }
            }
    void UpdateAnimations()
    {
        if(ishurt)
        playerAnimController.SetTrigger("ishurt");
        playerAnimController.SetBool("isdeath",isDead);
        if (ishurt && !isDead)
            playerAnimController.SetTrigger("ishurt");


    }
    void ReduceHealth()
    {
        if (ishurt)
        {
            currentPlayerHealth -= givedamage.damage;
            ishurt = false;

            //Eger havadaysak sol veya sag ve dikey yonde guc uygula
            if (facingright && !isGrounded)
                body2D.AddForce(new Vector2(0, 1200), ForceMode2D.Force);
            else if (!facingright && !isGrounded)
                body2D.AddForce(new Vector2(0, 1200), ForceMode2D.Force);

            //Eger yerdeysek sol veya sag yonde guc uygula
            if (facingright && isGrounded)
                body2D.AddForce(new Vector2(-knockBackForce, 0), ForceMode2D.Force);
            else if (!facingright && isGrounded)
                body2D.AddForce(new Vector2(knockBackForce, 0), ForceMode2D.Force);

            if (!isDead)
            {
                auSource.PlayOneShot(au_Hurt);
                auSource.pitch = Random.Range(0.8f, 1.2f);
            }

        }
    }

    void KillPlayer()
    {
        
            ishurt = false;
            body2D.AddForce(new Vector2(0,deadforce));
        body2D.drag = 4;
        body2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            cir2D.enabled = false;


        


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            currentPoints += point;
            Destroy(other.gameObject);
            auSource.PlayOneShot(au_PickupCoin);
            auSource.pitch = Random.Range(0.8f, 1.2f);
        }
        if (other.tag == "Enemy" && isDead==true)
            auSource.PlayOneShot(ac_Die);

        if (other.gameObject.tag.Contains("Finish"))
        {
            sonrakiLevelKontrolcusu();
            PlayerPrefs.SetInt("level",SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(1); //level seçim ekranı
        }
    }
    private void bolumSonuPanel()
    {
        bolumSonu_P.SetActive(true);//Panel Aç
        
        
    }

    string levelAdi(int id)//id den level'in ismini döndürüyor
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }

   

    public void TekrarOyna()//Tekrar Oyna Butonu
    {
        sahneGecis.SahneDegistir(levelAdi(SceneManager.GetActiveScene().buildIndex));
    }
}   

