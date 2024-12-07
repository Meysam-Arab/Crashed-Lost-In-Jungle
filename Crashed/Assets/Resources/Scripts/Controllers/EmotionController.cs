using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EmotionController : MonoBehaviour
{
  
    private Coroutine blinkRoutin;
    private EntityController entityController;
    private UnityEngine.U2D.Animation.SpriteResolver sr;

    [HideInInspector]public string state;

    void Awake()
    {
        blinkRoutin = null;
        entityController = gameObject.GetComponent<EntityController>();
        sr = GetComponent<UnityEngine.U2D.Animation.SpriteResolver>();

        state = "Idle";
        sr.SetCategoryAndLabel(state, "Normal");
        sr.ResolveSpriteToSpriteRenderer();
    }

    public void ChangeEmotionState(string newState)
    {
        state = newState;
    }

    public void StartBlinking()
    {
        if (blinkRoutin != null)
        {
            StopCoroutine(blinkRoutin);
            blinkRoutin = null;
        }
        blinkRoutin = StartCoroutine(BlinkRoutine());
    }

    /// <summary>
    /// for stop the blinking in emergencies, i don't think it will be used
    /// </summary>
    public void StopBlinking()
    {
        if(blinkRoutin != null)
        {
            StopCoroutine(blinkRoutin);
            blinkRoutin = null;
        }
      
    }

    IEnumerator BlinkRoutine()
	{
        float waitTime = Random.Range(1, 10);
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            waitTime = Random.Range(1,10);
          
            sr.SetCategoryAndLabel(state, "Blink");
            sr.ResolveSpriteToSpriteRenderer();
            yield return new WaitForSeconds(0.15f);
            sr.SetCategoryAndLabel(state, "Normal");
            sr.ResolveSpriteToSpriteRenderer();
            
        }
	}

    public void StartFearing()
    {
        ChangeEmotionState("Fear");
        StartCoroutine(FearRoutine());
    }

    IEnumerator FearRoutine()
    {
        float waitTime = Random.Range(5, 10);

       
      
        sr.SetCategoryAndLabel(state, "Normal");
        sr.ResolveSpriteToSpriteRenderer();
        yield return new WaitForSeconds(waitTime);
        ChangeEmotionState("Idle");
        sr.SetCategoryAndLabel(state, "Normal");
        sr.ResolveSpriteToSpriteRenderer();
       

        yield return null;
      
    }

    public void StartHappiness()
    {
        ChangeEmotionState("Happy");
        StartCoroutine(HappinessRoutine());
    }

    IEnumerator HappinessRoutine()
    {
        float waitTime = Random.Range(5, 10);

        sr.SetCategoryAndLabel(state, "Normal");
        sr.ResolveSpriteToSpriteRenderer();
        yield return new WaitForSeconds(waitTime);
        ChangeEmotionState("Idle");
        sr.SetCategoryAndLabel(state, "Normal");
        sr.ResolveSpriteToSpriteRenderer();
       

        yield return null;

    }
}
