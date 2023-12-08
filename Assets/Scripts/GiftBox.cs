using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftBox : MonoBehaviour//Singleton<GiftBox> 
{
    //public TextMeshProUGUI coinText;

    //int coinCount = 0;

    //PowerUps powerup = PowerUps.Helicopter;
    //int powerUpCount = 0;

    //public List<Sprite> powerUpSprites;
    //public Image powerUpImage;
    //public TextMeshProUGUI powerUpCountText;
    //public AudioSource audioSource;
    //public AudioClip openBoxClip;
    //public AudioClip itemDropClip;
    //public AudioClip buttonClickClip;


    //private void Start()
    //{
    //    audioSource = transform.parent.GetComponent<AudioSource>();
    //    transform.parent.gameObject.SetActive(false);
    //    gameObject.transform.parent.localScale = Vector3.one;
        

    //}
    //public void ActivateGiftBox()
    //{
    //    transform.parent.gameObject.SetActive(true);
    //    ChangeGiftElements();
    //}


    //void ChangeGiftElements()
    //{
    //    CoinChanger();
    //    PowerUpChanger();
    //}
    //void CoinChanger()
    //{
    //    int random = Random.Range(5, 11)*10;
    //    coinCount = random;
    //    coinText.text ="X "+ random.ToString();
    //}
    //void PowerUpChanger()
    //{
    //    int countRandom=Random.Range(1, 3);
    //    int powerupRandom = Random.Range(0, 3);
    //    if (PlayerPrefs.GetInt("Level") < 8)
    //    {
    //        countRandom = 1;

    //        powerupRandom=0;
    //    }
    //    if (powerupRandom == 0)
    //    {
    //        //helicopter
    //        powerUpImage.sprite = powerUpSprites[0];
    //        powerUpCountText.text="X "+countRandom.ToString();

    //       int helicoptrHintLeft=PlayerPrefs.GetInt(PowerUps.Helicopter.ToString());
    //        helicoptrHintLeft += countRandom;
    //        //PlayerPrefs.SetInt(PowerUps.Helicopter.ToString(), helicoptrHintLeft);
            
    //    }
    //    if (powerupRandom == 1)
    //    {
    //        //Hint(next car)
    //        powerUpImage.sprite = powerUpSprites[1];
    //        powerUpCountText.text = "X " + countRandom.ToString();


    //        int HintLeft = PlayerPrefs.GetInt(PowerUps.Hint.ToString());
    //        HintLeft += countRandom;
    //        //PlayerPrefs.SetInt(PowerUps.Hint.ToString(), HintLeft);
    //    }
    //    if (powerupRandom == 2)
    //    {
    //        //Freeze(slowDown)
    //        powerUpImage.sprite = powerUpSprites[2];
    //        powerUpCountText.text = "X " + countRandom.ToString();


    //        int FreeezeHintLeft = PlayerPrefs.GetInt(PowerUps.Freeze.ToString());
    //        FreeezeHintLeft += countRandom;
    //        //PlayerPrefs.SetInt(PowerUps.Freeze.ToString(), FreeezeHintLeft);
    //    }
    //    CoinManager.Instance.AddCoins(100 * countRandom);

    //}
    //public void ClaimGiftBox()
    //{
    //    CoinManager.Instance.AddCoins(coinCount);
    //    Vibration.VibrateAndroid(30);
    //    audioSource.PlayOneShot(buttonClickClip);
    //    LevelMapInstantiator.Instance.SetContentHeight();
    //    transform.parent.gameObject.SetActive(false);
    //}
    //public void OpenBoxSoundPlay()
    //{
    //    audioSource.PlayOneShot(openBoxClip);
    //}
    //public void DropItemSoundPlay()
    //{
    //    audioSource.PlayOneShot(itemDropClip);
    //}
}
