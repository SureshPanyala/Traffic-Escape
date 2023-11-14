using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StarsManager : MonoBehaviour
{
    public static StarsManager instance;
    private void Awake()
    {
        instance = this;
    }
    public Image[] stars;

    public Sprite goldStar;

    public void DisplayStars(int num)
    {
        StartCoroutine(ApplyGoldStar(num));
    }

    private IEnumerator ApplyGoldStar(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Image starImage = stars[i];
            starImage.transform.localScale = Vector3.zero;
            starImage.sprite = goldStar;
            starImage.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(1f);
        }
    }
}