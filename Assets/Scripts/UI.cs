using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] GameObject lefthanded;
    [SerializeField] GameObject righthanded;
    [SerializeField] Sprite on, off;
    [SerializeField] Image sounds;
    [SerializeField] Image music;
    [SerializeField] Image lefth;
    [SerializeField] Image righth;
    private bool soundsen=true;
    private bool musicen=true;
    [SerializeField] AudioSource musicSS, soundSS;


    [SerializeField] Image[] imageSkin;
    [SerializeField] GameObject[] buttonsBuy;
    [SerializeField] GameObject[] buttonsUse;

    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject skins_Panel;
    [SerializeField] GameObject shop_Panel;
    [SerializeField] GameObject settings_Panel;
    [SerializeField] GameObject language_Panel;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject coin_hide;

    [SerializeField] TextMeshProUGUI[] allTexts;
    [SerializeField] string[] ruTexts;
    [SerializeField] string[] engTexts;
    [SerializeField] string[] germanTexts;



    private string language;

    public void Start()
    {
        if (PlayerPrefs.GetString("Hand")=="left")
        {
            lefthanded.SetActive(true);
            righthanded.SetActive(false);
            lefth.color = new Color(1, 1, 1, 1);
            righth.color = new Color(1, 0.5f, 0.5f, 1);
        }
        else
        {
            righthanded.SetActive(true);
            lefthanded.SetActive(false);
            lefth.color = new Color(1, 0.5f, 0.5f, 1);
            righth.color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().volume = 0;
            music.sprite = off;
        }
        else
        {
            GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().volume = 1;
            music.sprite = on;
        }
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            soundSS.volume = 0;
            sounds.sprite = off;
        }
        else
        {
            soundSS.volume = 1;
            sounds.sprite = on;
        }
        if (PlayerPrefs.GetInt("First") == 1)
        {
            buttonsBuy[1].SetActive(false);
            buttonsUse[1].SetActive(true);
        }
        int a = PlayerPrefs.GetInt("Skin"); for (int i = 0; i < 2; i++) { imageSkin[i].color = new Color(1, 1, 1, 0.5f); }
        imageSkin[a].color = new Color(1, 1, 1, 1f);

        language = PlayerPrefs.GetString("Language");
        switch (language)
        {
            case "Russian":
                for (int i = 0; i < allTexts.Length; i++)
                {
                    allTexts[i].text = ruTexts[i];
                }
                break;
            case "English":
                for (int i = 0; i < allTexts.Length; i++)
                {
                    allTexts[i].text = engTexts[i];
                }
                break;
            case "German":
                for (int i = 0; i < allTexts.Length; i++)
                {
                    allTexts[i].text = germanTexts[i];
                }
                break;
            default:
                break;
        }
    }
    public void OpenPanel(string a)
    {
        switch (a)
        {
            case "Skins": skins_Panel.SetActive(true); coins.text = PlayerPrefs.GetInt("Coins").ToString(); coin.SetActive(true); coin_hide.SetActive(true);

                break;
            case "Shop": shop_Panel.SetActive(true); coins.text = PlayerPrefs.GetInt("Coins").ToString(); coin.SetActive(true); coin_hide.SetActive(true);
                break;
            case "Settings": settings_Panel.SetActive(true); coin.SetActive(false); coin_hide.SetActive(false);
                break;
            case "Language": language_Panel.SetActive(true); coin.SetActive(false); coin_hide.SetActive(false);
                break;
            case "Close": skins_Panel.SetActive(false); shop_Panel.SetActive(false); settings_Panel.SetActive(false); language_Panel.SetActive(false); coins.text = PlayerPrefs.GetInt("Coins").ToString(); coin.SetActive(true); coin_hide.SetActive(true);
                break;
            default: 
                break;
        }
    }
    public void ChangeLanguage(string a)
    {
        switch (a)
        {
            case "Russian":
                PlayerPrefs.SetString("Language", "Russian");
                for (int i = 0; i < allTexts.Length; i++)
                {
                    allTexts[i].text = ruTexts[i];
                }
                    break;
            case "English":
                PlayerPrefs.SetString("Language", "English");
                for (int i = 0; i < allTexts.Length; i++)
                {
                    allTexts[i].text = engTexts[i];
                }
                break;
            case "German":
                PlayerPrefs.SetString("Language", "German");
                for (int i = 0; i < allTexts.Length; i++)
                {
                    allTexts[i].text = germanTexts[i];
                }
                break;
            default:
                break;
        }
        PlayerPrefs.Save();
    }

    public void BuyCoins(int a)
    {
        int b = PlayerPrefs.GetInt("Coins");
        int c = b + a;
        PlayerPrefs.SetInt("Coins", c);
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
        PlayerPrefs.Save();
    }
    public void BuySkin(int cost)
    {
        if (PlayerPrefs.GetInt("Coins") >= cost)
        {
            int b = PlayerPrefs.GetInt("Coins");
            int c = b - cost;
            PlayerPrefs.SetInt("Coins", c);
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
            switch (cost)
            {
                case 1000:
                    buttonsBuy[1].SetActive(false);
                    buttonsUse[1].SetActive(true);
                    PlayerPrefs.SetInt("First",1);
                    break;
            }
        }
        PlayerPrefs.Save();
    }
    public void UseSkin(int a)
    {
        switch (a)
        {
            case 0:
                PlayerPrefs.SetInt("Skin", a); for (int i = 0; i < 2; i++) { imageSkin[i].color = new Color(1, 1, 1, 0.5f); }
                imageSkin[a].color = new Color(1, 1, 1, 1f);

                break;
            case 1:
                PlayerPrefs.SetInt("Skin", a); for (int i = 0; i < 2; i++) { imageSkin[i].color = new Color(1, 1, 1, 0.5f); }
                imageSkin[a].color = new Color(1, 1, 1, 1f);
                break;
        }
        playerController.SkikChange();
        PlayerPrefs.Save();
    }
    public void TempSkinDELETER()
    {
        buttonsBuy[1].SetActive(true);
        buttonsUse[1].SetActive(false);
        PlayerPrefs.SetInt("First", 0);
        PlayerPrefs.SetInt("Skin", 0); for (int i = 0; i < 2; i++) { imageSkin[i].color = new Color(1, 1, 1, 0.5f); }
        imageSkin[0].color = new Color(1, 1, 1, 1f);
        playerController.SkikChange();
        PlayerPrefs.Save();
    }

    public void Restart()
    {
        panel.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void SettingsChange(string a)
    {

        switch (a)
        {
            case "Sounds":
                soundsen = !soundsen;
                if (soundsen)
                {
                    sounds.sprite = on;
                    soundSS.volume = 1f;
                    PlayerPrefs.SetInt("Sound", 1);
                    PlayerPrefs.Save();
                }
                else
                {
                    sounds.sprite = off;
                    soundSS.volume = 0;
                    PlayerPrefs.SetInt("Sound", 0);
                    PlayerPrefs.Save();
                }

                break;
            case "Music":
                musicen= !musicen;
                if (musicen)
                {
                    music.sprite = on;
                    GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().volume = 1;
                    PlayerPrefs.SetInt("Music", 1);
                    PlayerPrefs.Save();
                }
                else
                {
                    music.sprite = off;
                    GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().volume=0;
                    PlayerPrefs.SetInt("Music", 0);
                    PlayerPrefs.Save();
                }
                break;
            case "LeftH":
                lefthanded.SetActive(true);
                righthanded.SetActive(false);
                lefth.color = new Color(1, 1, 1, 1);
                righth.color = new Color(1, 0.5f, 0.5f, 1);
                PlayerPrefs.SetString("Hand", "left");
                PlayerPrefs.Save();
                break;
            case "RightH":
                righthanded.SetActive(true);
                lefthanded.SetActive(false);
                lefth.color = new Color(1, 0.5f, 0.5f, 1);
                righth.color = new Color(1, 1, 1, 1);
                PlayerPrefs.SetString("Hand", "right");
                PlayerPrefs.Save();
                break;
        }
    }
}
