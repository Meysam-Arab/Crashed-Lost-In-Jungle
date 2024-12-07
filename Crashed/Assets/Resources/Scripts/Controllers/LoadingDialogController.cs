using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingDialogController : MonoBehaviour
{
    public TMP_Text title;

    private CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }


    public LoadingDialogController Title(string title)
    {
        this.title.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        this.title.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        this.title.text = title;
        return this;
    }


    // show the dialog, set it's canvasGroup.alpha to 1f or tween like here
    public void Show()
    {

        this.title.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        this.title.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        this.title.text = MeysamLocalization.GetLocalizaStringByKey("Please Wait...");

        this.transform.SetAsLastSibling();
     
        cg.blocksRaycasts = true;
        cg.interactable = true;
        cg.alpha = 1f;

        if(GameController.instance != null)
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

    private static LoadingDialogController instance;
    public static LoadingDialogController Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(LoadingDialogController)) as LoadingDialogController;
            if (!instance)
                Debug.Log("There need to be at least one active GenericDialog on the scene");
        }

        return instance;
    }
}
