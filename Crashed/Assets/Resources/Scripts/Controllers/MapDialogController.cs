using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MapDialogController : MonoBehaviour
{
    public Animator an;
   
    public Button okButton;

    private CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public MapDialogController OnOk(UnityAction action)
    {
      
        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(action);
        return this;
    }

  
    public MapDialogController SetAnimationIndex(int animIndex)
    {
        an.runtimeAnimatorController = RefrenceRepository.GetMapAnimationByIndex(animIndex) as RuntimeAnimatorController;
        return this;
    }

    // show the dialog, set it's canvasGroup.alpha to 1f or tween like here
    public void Show()
    {
        this.transform.SetAsLastSibling();
     
        cg.blocksRaycasts = true;
        cg.interactable = true;
        cg.alpha = 1f;
        an.Rebind();

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

    private static MapDialogController instance;
    public static MapDialogController Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(MapDialogController)) as MapDialogController;
            if (!instance)
                Debug.Log("There need to be at least one active GenericDialog on the scene");
        }

        return instance;
    }
}
