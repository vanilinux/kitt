using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioSource AudioS;
    [SerializeField] AudioClip[] coinSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip loseSound;
    private int coins;
    [SerializeField] private SpriteRenderer skin;
    [SerializeField] public Sprite[] sprites;
    [SerializeField] private GameObject UiShkaf;
    [SerializeField] private Animator UIanim;
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController[] controllers;
    [SerializeField] private GameObject[] particles;
    [SerializeField] private ParticleSystem[] particlesystem;
    [SerializeField] GameObject MenuUI;
    [SerializeField] Image staminabar;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject[] prefabPlatform;
    [SerializeField] GameObject prefabDeathPlatform;
    [SerializeField] GameObject coinPrefab;
    private int jumpCount = 0;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] private int score;
    [SerializeField] TextMeshProUGUI bestScore;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject scoreObj;
    [SerializeField] GameObject player;
    private bool turnedRight=true;
    Vector3 sa = new Vector3(1f, 0f, 0);
    private float stamina=3;
    private bool gamestarted = false;
    private float increment = 1.0f;
    Vector3 lastPlayerPos = new Vector3(0, -0.5f, 0);

    private int skinID=0;

    private bool pressed=false;

    [SerializeField] Sprite leftTurn, rightTurn;
    [SerializeField] Image Turner;

    void Start()
    {
        SkikChange();
        coins = PlayerPrefs.GetInt("Coins");
        gamestarted = false;
        MenuUI.SetActive(true);
        scoreObj.SetActive(false);
        bestScore.text = (PlayerPrefs.GetInt("MaxScore")).ToString();
        coinsText.text = (PlayerPrefs.GetInt("Coins")).ToString();
        stamina = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("MaxScore", 0);
        }
        if (gamestarted == true)
        {
            // game
            stamina -= increment * Time.fixedDeltaTime;
            staminabar.fillAmount = stamina / 3;
            if (stamina <= 0)
            {
                if (PlayerPrefs.GetInt("MaxScore") < score)
                {
                    PlayerPrefs.SetInt("MaxScore", score);
                }
                bestScore.text = (PlayerPrefs.GetInt("MaxScore")).ToString();
                gamestarted = false;
                panel.SetActive(true);
                PlayerPrefs.Save();
            }
        }     
    }
    public void JumpForward()
    {
        if (!pressed)
        {
            StartCoroutine(CounterPlusF());
        }
    }
    public void JumpBackward()
    {
        if (!pressed)
        {
            StartCoroutine(CounterPlusB());
        }
    }
    IEnumerator CounterPlusF()
    {
        pressed = true;
        switch (skinID)
        {
            case 0: animator.SetBool("Poof", true);
                break;
            case 1: animator.SetBool("Poof", true);
                break;
        }

        increment += 0.02f / (1 + jumpCount * 0.1f); // Гиперболическое затухание
        jumpCount++;
        if (gamestarted == false)
        {
            for (int i = 0; i < 10; i++)
            {
                var plats = Instantiate(prefabPlatform[UnityEngine.Random.Range(0, 2)]);
                int randomValues = UnityEngine.Random.value < 0.5f ? -1 : 1;
                int outputValues = randomValues == -1 ? 1 : -1;
                plats.transform.position = new Vector3(sa.x + randomValues, sa.y + 1f, sa.z);
                var platdeaths = Instantiate(prefabDeathPlatform);
                platdeaths.transform.position = new Vector3(sa.x + outputValues, sa.y + 1f, sa.z);
                platdeaths.transform.localScale = new Vector3(randomValues, 0.3f, 1);
                sa = plats.transform.position;
            }
            StartCoroutine(GameStarted());
        }
        gamestarted = true;
        stamina = 3;
        var plat = Instantiate(prefabPlatform[UnityEngine.Random.Range(0, 2)]);
        int randomValue = UnityEngine.Random.value < 0.5f ? -1 : 1;
        int outputValue = randomValue == -1 ? 1 : -1;
        plat.transform.position = new Vector3(sa.x + randomValue, sa.y + 1f, sa.z);
        var platdeath = Instantiate(prefabDeathPlatform);
        platdeath.transform.position = new Vector3(sa.x + outputValue, sa.y + 1f, sa.z);
        platdeath.transform.localScale = new Vector3(randomValue, 0.3f, 1);
        sa = plat.transform.position;
        //монетка
        if (UnityEngine.Random.Range(0, 10) == 1)
        {
            var coin = Instantiate(coinPrefab);
            coin.transform.position = new Vector3(sa.x, sa.y + .8f, sa.z);
        }

        if (turnedRight)
        {
            player.transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y + 1f, player.transform.position.z);
        }
        if (!turnedRight)
        {
            player.transform.position = new Vector3(player.transform.position.x - 1f, player.transform.position.y + 1f, player.transform.position.z);
        }
        yield return new WaitForSeconds(0.05f);
        pressed = false;
        if (gamestarted)
        {
            score += 1;
            scoreText.text = score.ToString();
            lastPlayerPos = player.transform.position;
        }
        particlesystem[skinID].Play();
        yield return new WaitForSeconds(0.2f);
        switch (skinID)
        {
            case 0:
                animator.SetBool("Poof", false);
                break;
            case 1:
                animator.SetBool("Poof", false);
                break;
        }
    }
    IEnumerator CounterPlusB()
    {
        pressed = true;
        switch (skinID)
        {
            case 0:
                animator.SetBool("Poof", true);
                break;
            case 1:
                animator.SetBool("Poof", true);
                break;
        }

        Vector3 currentScale = transform.localScale;

        // Меняем знак масштаба по оси X
        currentScale.x = -currentScale.x;

        // Устанавливаем новый масштаб
        transform.localScale = currentScale;
        increment += 0.02f / (1 + jumpCount * 0.1f); // Гиперболическое затухание
        jumpCount++;
        stamina = 3;
        var plat = Instantiate(prefabPlatform[UnityEngine.Random.Range(0, 2)]);
        int randomValue = UnityEngine.Random.value < 0.5f ? -1 : 1;
        int outputValue = randomValue == -1 ? 1 : -1;
        plat.transform.position = new Vector3(sa.x + randomValue, sa.y + 1f, sa.z);
        var platdeath = Instantiate(prefabDeathPlatform);
        platdeath.transform.position = new Vector3(sa.x + outputValue, sa.y + 1f, sa.z);
        platdeath.transform.localScale = new Vector3(randomValue, 0.3f, 1);
        sa = plat.transform.position;
        //монетка
        if (UnityEngine.Random.Range(0, 10) == 1)
        {
            var coin = Instantiate(coinPrefab);
            coin.transform.position = new Vector3(sa.x, sa.y + .8f, sa.z);
        }
        if (turnedRight)
        {
            player.transform.position = new Vector3(player.transform.position.x - 1f, player.transform.position.y + 1f, player.transform.position.z);
            turnedRight = false;
            Turner.sprite = leftTurn;
        }
        else if (!turnedRight)
        {
            player.transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y + 1f, player.transform.position.z);
            turnedRight = true;
            Turner.sprite = rightTurn;
        }
        yield return new WaitForSeconds(0.05f);
        pressed = false;
        if (gamestarted)
        {
            score += 1;
            scoreText.text = score.ToString();
            lastPlayerPos = player.transform.position;
        }
        particlesystem[skinID].Play();
        yield return new WaitForSeconds(0.2f);
        switch (skinID)
        {
            case 0:
                animator.SetBool("Poof", false);
                break;
            case 1:
                animator.SetBool("Poof", false);
                break;
        }
    }
    IEnumerator GameStarted()
    {
        UIanim.Play("ShkafAnimation");
        MenuUI.SetActive(false);
        scoreObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        UiShkaf.SetActive(false);
    }
    public void Countinue()
    {
        player.transform.position = lastPlayerPos;
        panel.SetActive(false);
        stamina = 3;
        gamestarted = true;
        Time.timeScale = 1;
    }
    public void SkikChange()
    {
        skinID = PlayerPrefs.GetInt("Skin");
        animator.runtimeAnimatorController = controllers[skinID];
        for(int i=0; i < 2; i++)
        {
            particles[i].SetActive(false);
        }
        particles[skinID].SetActive(true);
        if (animator != null)
            animator.enabled = false;
        skin.sprite = sprites[skinID];
        if (animator != null)
            animator.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathPlatform"))
        {
            if (PlayerPrefs.GetInt("MaxScore") < score)
            {
                PlayerPrefs.SetInt("MaxScore", score);
            }
            bestScore.text = (PlayerPrefs.GetInt("MaxScore")).ToString();
            gamestarted =false;
            AudioS.PlayOneShot(loseSound);
            panel.SetActive(true);
            PlayerPrefs.Save();
        }
        if (collision.CompareTag("Platform"))
        {
            Animator cloudAnim = collision.GetComponent<Animator>();
            cloudAnim.SetBool("Bend",true);
        }
        if (collision.CompareTag("Coin"))
        {
            coins = coins + 50;
            PlayerPrefs.SetInt("Coins", coins);
            coinsText.text = (PlayerPrefs.GetInt("Coins")).ToString();
            Animator anims = collision.GetComponent<Animator>();
            anims.SetBool("Taken",true);
            AudioS.PlayOneShot(coinSound[Random.RandomRange(0,2)]);
            Destroy(collision.gameObject, 1f);
            PlayerPrefs.Save();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            Animator cloudAnim = collision.GetComponent<Animator>();
            cloudAnim.SetBool("Bend", false);
        }
    }
}


