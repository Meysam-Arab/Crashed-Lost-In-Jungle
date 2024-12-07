using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GenericDialogController : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text message;
    public TMP_Text accept, decline;
    public Button acceptButton, declineButton;

    private CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public GenericDialogController OnAccept(string text, UnityAction action)
    {
        accept.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        accept.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        accept.text = text;
     
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(action);
        return this;
    }



    public GenericDialogController OnDecline(string text, UnityAction action)
    {
        decline.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        decline.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
       
        decline.text = text;
        declineButton.onClick.RemoveAllListeners();
        declineButton.onClick.AddListener(action);
        return this;
    }

    public GenericDialogController Title(string title)
    {
        this.title.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        this.title.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        this.title.text = title;
        return this;
    }

    public GenericDialogController Message(string message)
    {
        this.message.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        this.message.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        this.message.text = message;
        return this;
    }

    // show the dialog, set it's canvasGroup.alpha to 1f or tween like here
    public void Show()
    {
        this.transform.SetAsLastSibling();
     
        cg.blocksRaycasts = true;
        cg.interactable = true;
        cg.alpha = 1f;

        if (GameController.instance != null)
        {
            GameController.instance.showingDialogCount++;
        }
    }

    public void Hide()
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;
        cg.alpha = 0f;


        if (GameController.instance != null)
        {
            GameController.instance.showingDialogCount--;
            if (GameController.instance.showingDialogCount < 0)
                GameController.instance.showingDialogCount = 0;
        }

    }

    private static GenericDialogController instance;
    public static GenericDialogController Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(GenericDialogController)) as GenericDialogController;
            if (!instance)
                Debug.Log("There need to be at least one active GenericDialog on the scene");
        }

        return instance;
    }
}
