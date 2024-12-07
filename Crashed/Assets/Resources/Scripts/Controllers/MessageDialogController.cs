using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MessageDialogController : MonoBehaviour
{

    public TMP_Text message;
    public TMP_Text title;
    public TMP_Text ok;
    public Button okButton;

    private CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public MessageDialogController OnOk(string text, UnityAction action)
    {
        ok.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        ok.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        ok.text = text;
   
        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(action);
        return this;
    }

    public MessageDialogController Title(string title)
    {
        this.title.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        this.title.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        this.title.text = title;
        return this;
    }

    public MessageDialogController Message(string message)
    {

        // Assign the new font asset.
        
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

    private static MessageDialogController instance;
    public static MessageDialogController Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(MessageDialogController)) as MessageDialogController;
            if (!instance)
                Debug.Log("There need to be at least one active GenericDialog on the scene");
        }

        return instance;
    }
}
