
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class LevelMapInstantiator : MonoBehaviour//Singleton<LevelMapInstantiator>
{
    public GameObject levelImages;

    public GameObject normalLevel;
    public GameObject prevLevel;
    public GameObject currLevel;
    public GameObject bossLevel;
    public GameObject giftLevel;

    public GameObject powerUpRef;
    public List<Sprite> powerUpsSprites;
    public List<int> refLevels;

    public Sprite prevSprite;
    public Sprite currSprite;
    public Color prevCol;

    public bool isNewLevel = false;

    public int totalLevelCount=0;
    public int level = 0;
    public int startLevel = 0;
    public Vector3 firstPos;
    public Vector3 firstPos02;

    bool isGiftBox = false;
    public Vector3 secondPos;
    public Vector3 secondPos02;

    Vector3 startPos;
    Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        isNewLevel = false;
        level = PlayerPrefs.GetInt("Level", 1);
        if (!PlayerPrefs.HasKey("RoadLevel"))
        {
            PlayerPrefs.SetInt("RoadLevel", 1);
        }
        if (level != PlayerPrefs.GetInt("RoadLevel"))
        {
            if((level-1)%5==0&& (level - 1) % 10 != 0)
            {
                Invoke("OpenGiftBox", 0.1f);
                isGiftBox= true;
            }
            isNewLevel = true;
            PlayerPrefs.SetInt("RoadLevel", level);
        }



        if (level < 21)
        {
            startLevel = 1;
            totalLevelCount += level;

        }
        else
        {
            startLevel = level - 20;
            totalLevelCount += 20;
        }
        totalLevelCount += 60;
        
        if(!isGiftBox)
        {
            SetContentHeight();
        }
        
        
    }
    void OpenGiftBox()
    {
       // GiftBox.Instance.ActivateGiftBox();
    }
    public void SetContentHeight()
    {
        var totalContentCount = (float)totalLevelCount;
       if(totalContentCount>75.0f)
        {
            totalContentCount = 75;
        }
        if (level < 16)
        {
            totalContentCount -= 1.5f;
        }
      

        var vert = GetComponent<VerticalLayoutGroup>();
        
        var child = levelImages.transform as RectTransform;

        float scrollHeight = 0f;
        if (child != null) scrollHeight = (child.rect.height + vert.spacing) * (totalContentCount);
        

        var rect = GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().sizeDelta = new Vector2(rect.x, scrollHeight);
        

        if (level < 21)
        {
            GetComponent<RectTransform>().anchoredPosition = firstPos;
            startPos = firstPos02;
            endPos = firstPos;
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = secondPos;
            startPos = secondPos02;
            endPos = secondPos;
        }

        SetLevelImages();
    }
    void ShowTheActiveLevel()
    {
       
        var activeLevel = 5;
        //Debug.Log("Active Level " + activeLevel);

        var child = transform.GetChild(activeLevel);


        if (child != null) child.GetComponent<Image>().color = Color.blue;

        //MainMenuUIManagerScript.Instance.MoveToActivePlayableLevel();
    }

    //To instantiate the Level Images
    void SetLevelImages()
    {
        Transform oldLevel = null;
        var imagesNoToInstantiate = totalLevelCount;
        //Debug.Log("StartCalled " + imagesNoToInstantiate);
        for (int i = 0; i < imagesNoToInstantiate; i++)
        {
            if((i+startLevel)%10==0)
            {
                levelImages = bossLevel;
            }
            else if((i + startLevel) % 5 == 0)
            {
                levelImages = giftLevel;
            }
            else
            {
                levelImages = normalLevel;
            }


            if (this == null) continue;
            var image = Instantiate(levelImages, transform, true);
            image.name= levelImages.name+(i+startLevel).ToString();
            image.transform.localScale = Vector3.one * 3;
            image.GetComponentInChildren<TextMeshProUGUI>().text = (i + startLevel).ToString();

            //image.transform.GetChild(1).GetComponent<Image>().color = Color.gray ;
            //if ((i + startLevel) %10==0)
            //{
            //    image.transform.GetChild(1).GetComponent <Image>().color=Color.red;

            //}else
            if ((i + startLevel) % 5 == 0&&( (i + startLevel) % 10 != 0)&&(i+startLevel)<level)
            {
               image.transform.GetChild(2).gameObject.SetActive(false);
            }
            if (level == i + startLevel)
            {
                if(level%10 != 0)
                {
                    image.transform.GetChild(1).GetComponent<Image>().sprite = currSprite;
                }
                if(isNewLevel)
                {
                    StartCoroutine(NextLevelAnimation(oldLevel, image.transform));
                }
                else
                {
                    image.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 2f;
                }
                
                
            }
            if (i + startLevel < level)
            {
                image.transform.GetChild(1).GetComponent<Image>().sprite = prevSprite;
                image.transform.GetChild(0).GetComponent<Image>().color = prevCol;
            }
            oldLevel = image.transform;
            //Debug.Log("Called");
            //if (i + startLevel < 11)
            //{
            //    if (i + startLevel == 6)
            //    {
            //        var refPrefab = Instantiate(powerUpRef, image.transform, false);
            //        refPrefab.transform.GetChild(0).GetComponent<Image>().sprite = powerUpsSprites[0];
            //        refPrefab.GetComponent<RectTransform>().localPosition = Vector3.zero;
            //        refPrefab.GetComponent<RectTransform>().localScale = Vector3.one;
            //    }
            //    if (i + startLevel == 8)
            //    {
            //        var refPrefab = Instantiate(powerUpRef, image.transform, false);
            //        refPrefab.transform.GetChild(0).GetComponent<Image>().sprite = powerUpsSprites[1];
            //        refPrefab.GetComponent<RectTransform>().localPosition = Vector3.zero;
            //        refPrefab.GetComponent<RectTransform>().localScale = Vector3.one;
            //    }
            //    if (i + startLevel == 10)
            //    {
            //        var refPrefab = Instantiate(powerUpRef, image.transform, false);
            //        refPrefab.transform.GetChild(0).GetComponent<Image>().sprite = powerUpsSprites[2];
            //        refPrefab.GetComponent<RectTransform>().localPosition = Vector3.zero;
            //        refPrefab.GetComponent<RectTransform>().localScale = Vector3.one;
            //    }
            //}
            if (i + startLevel >=level)
            {
                for (int j = 0; j < refLevels.Count; j++)
                {
                    if (refLevels[j] == i + startLevel)
                    {
                        var refPrefab = Instantiate(powerUpRef, image.transform, false);
                        refPrefab.transform.GetChild(0).GetComponent<Image>().sprite = powerUpsSprites[j];
                        refPrefab.GetComponent<RectTransform>().localPosition = Vector3.zero;
                        refPrefab.GetComponent<RectTransform>().localScale = Vector3.one;
                    }
                }
            }
            
        }

        //ShowTheActiveLevel();
        
    }
    IEnumerator NextLevelAnimation(Transform oldLevel,Transform newLevel)
    {
        GetComponent<RectTransform>().anchoredPosition = startPos;
        oldLevel.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 2f;
        newLevel.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 1.4f;
        yield return new WaitForSeconds(0.4f);
        float elapsedTime = 0f;
        
        while (elapsedTime < 1f)
        {
            elapsedTime+= Time.deltaTime;
            oldLevel.GetChild(1).GetComponent<RectTransform>().localScale =
                Vector3.Lerp(Vector3.one * 2f, Vector3.one * 1.4f, elapsedTime);
            newLevel.GetChild(1).GetComponent<RectTransform>().localScale=
                Vector3.Lerp( Vector3.one * 1.4f, Vector3.one * 2f, elapsedTime);
            GetComponent<RectTransform>().anchoredPosition=Vector3.Lerp(startPos,endPos,elapsedTime);

            yield return null;
        }
        GetComponent<RectTransform>().anchoredPosition = endPos;
    }
}
