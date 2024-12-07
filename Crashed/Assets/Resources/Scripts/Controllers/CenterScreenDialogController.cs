using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CenterScreenDialogController : MonoBehaviour
{

    float delay;

    public TMP_Text message;

    public static string TAG_CENTER_SCREEN_DIALOG = "CenterScreenDialog";

    private void OnEnable()
    {
        message.transform.parent.gameObject.SetActive(false);
     
    }

    public void ShowMessage(string messageTextI, float delayI, Color32 textColorI, int fontSizeI)
    {

        delay = delayI;


        message.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        message.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        message.color = textColorI;
        message.fontSize = fontSizeI;
        message.text = messageTextI;

        float w = 0.3f; // proportional width (0..1)
        float h = 0.2f; // proportional height (0..1)
        RectTransform rect = message.GetComponent<RectTransform>();
        rect.anchoredPosition.Set((Screen.width * (1 - w)) / 2 , (Screen.height * (1 - h)) / 2);
        //rect.y = (Screen.height * (1 - h)) / 2;
        rect.sizeDelta = new Vector2(Screen.width * w, Screen.height * h);
        //rect.height = Screen.height * h;

        //message.Set = rect;

        StartCoroutine(LifeTimeRoutine());

    }

    public void HideMessage()
    {
        message.transform.parent.gameObject.SetActive(false);
    }

    IEnumerator LifeTimeRoutine()
    {
        message.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        message.transform.parent.gameObject.SetActive(false);


        yield break;
    }
}
