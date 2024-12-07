using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{

    private TMP_Text textToShow;
    private Color32 defaultColor = new Color32(251, 195, 105, 255);
    
    private int defaultFontSize = 24;
    private float defaultFadeSpeed = 0.1f;
    private GameObject entityGO;

    private Coroutine fadeOutRoutine;

    private bool onRandomSpeech;
    private bool onHateSpeech;
    private bool onThankSpeech;
    private bool onEventSpeech;
    private bool onADSpeech;

    /// <summary>
    /// This will be your time in seconds.
    /// </summary>
    private float defaultDuration = 5f;

    /// <summary>
    /// This will determine the defaultSmoothness of the lerp. Smaller values are smoother. Really it's the time between updates.
    /// </summary>
    private float defaultSmoothness = 0.02f;

    /// <summary>
    /// universal probability as to show random speech or not upon player contact
    /// </summary>
    public float randomSpeechShowProbability;

    /// <summary>
    /// universal probability as to show hate speech or not upon player contact
    /// </summary>
    public float hateSpeechShowProbability;

    /// <summary>
    /// universal probability as to show thank speech or not upon player contact
    /// </summary>
    public float thankSpeechShowProbability;

    private EntityController ec;


    //////////Default Random Speech///////////////////////////////////////

    List<Speech> randomSpeechs;//initialize for each npcGO tag or name
    List<Speech> hateSpeechs;//initialize for each npcGO tag or name
    List<Speech> thankSpeechs;//initialize for each npcGO tag or name
    List<Speech> adSpeechs;//initialize for each npcGO tag or name

    List<Speech> eventSpeechs;//for events - come from outside - no initialization
 
    //////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        textToShow = GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<TMP_Text>();

        onRandomSpeech = false;
        onHateSpeech = false;
        onThankSpeech = false;
        onEventSpeech = false;
        onADSpeech = false;

        fadeOutRoutine = null;

        randomSpeechs = new List<Speech>();
        hateSpeechs = new List<Speech>();
        thankSpeechs = new List<Speech>();
        adSpeechs = new List<Speech>();
    }

    private void Start()
    {
        entityGO = gameObject.transform.parent.gameObject;
        InitializeRandomSpeech();
        InitializeADSpeech();
        StartCoroutine(RandomSpeechRoutine());
    }

    private void OnEnable()
    {
        textToShow.enabled = false;
    }

    //public void InitializeSpeechs()
    //{
   

    //    entityGO = gameObject.transform.parent.gameObject;
    //    randomSpeechs = new List<Speech>();
    //    thankSpeechs = new List<Speech>();
    //    hateSpeechs = new List<Speech>();
    //    adSpeechs = new List<Speech>();

    //    InitializeRandomSpeech();
    //    InitializeThankSpeech();
    //    InitializeHateSpeech();
    //    InitializeADSpeech();

    //}

    private void InitializeHateSpeech()
    {
        Speech speech;
        if (entityGO.tag == EnemyModel.TAG_ENEMY_BEAR)
        {
            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("Damn it!");
            speech.speechStartColor = MeysamUtility.ColorBloodRed;
            speech.speechEndColor = MeysamUtility.ColorBloodRed;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            hateSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("That hurts!");
            speech.speechStartColor = MeysamUtility.ColorBloodRed;
            speech.speechEndColor = MeysamUtility.ColorBloodRed;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            hateSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("Ahhhh");
            speech.speechStartColor = MeysamUtility.ColorBloodRed;
            speech.speechEndColor = MeysamUtility.ColorBloodRed;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            hateSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("Aghhh");
            speech.speechStartColor = MeysamUtility.ColorBloodRed;
            speech.speechEndColor = MeysamUtility.ColorBloodRed;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            hateSpeechs.Add(speech);

        }
        
    }

    private void InitializeThankSpeech()
    {
        Speech speech;

       if (entityGO.tag == EnemyModel.TAG_ENEMY_GORILLA)
        {

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("Thanks man!");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            thankSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("Wow! How generous of you!");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            thankSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("That hits the spot!");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            thankSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("Wow!");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            thankSpeechs.Add(speech);

        }
    }
 
    private void InitializeRandomSpeech()
    {
        Speech speech;

        if (entityGO.tag == "Player")
        {
            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("It's too hot!");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            randomSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("Too many bugs!");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            randomSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("Can i survive this mess?");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            randomSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("What was that?");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            randomSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("What should i do?");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            randomSpeechs.Add(speech);


        }
    }
    private void InitializeADSpeech()
    {
        Speech speech;
        if (entityGO.tag == "Player")
        {
            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("I'm Low on energy! Better to watch some video ads so I can get some!");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            adSpeechs.Add(speech);

            speech = new Speech();
            speech.speechText = MeysamLocalization.GetLocalizaStringByKey("Low energy! I must either pick something to eat or watch some video ads!");
            speech.speechStartColor = defaultColor;
            speech.speechEndColor = defaultColor;
            speech.speechDuration = defaultDuration;
            speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
            speech.speechFontSize = defaultFontSize;
            speech.speechFadeSpeed = defaultFadeSpeed;
            speech.speechDistinctItemDropCount = 0;
            speech.speechItemMinCount = null;
            speech.speechItemMaxCount = null;
            speech.speechItemTag = null;
            speech.speechItemProbability = null;
            speech.speechProbability = 1.0f;
            speech.speechSmoothness = defaultSmoothness;
            adSpeechs.Add(speech);

        }

    }
    IEnumerator FadeOut(Speech speech)
    {
        
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = speech.speechSmoothness / speech.speechDuration; //The amount of change to apply.
        while (progress < 1)
        {

            textToShow.transform.position += new Vector3(0f, 0.004f, 0f);
            textToShow.color = Color.Lerp(speech.speechStartColor, speech.speechEndColor, increment);

            progress += increment;
            yield return new WaitForSeconds(speech.speechSmoothness);
        }
        
        textToShow.enabled = false;
        onRandomSpeech = false;
        onHateSpeech = false;
        onThankSpeech = false;
        onEventSpeech = false;
        onADSpeech = false;
        yield break;
    }

  

    private void ShowRandomSpeech()
    {
        if (onRandomSpeech || onEventSpeech || onHateSpeech || onThankSpeech || onADSpeech)
            return;

    
     
        if (randomSpeechs.Count == 0)
            return;

        float probAppear = Random.Range(0f, 1f);
        if (probAppear <= randomSpeechShowProbability)
        {
            textToShow.enabled = true;
          
            textToShow.transform.position = gameObject.transform.position;

            float[] chances = new float[randomSpeechs.Count];
            for (int i = 0; i < randomSpeechs.Count; i++)
            {
                chances[i] = randomSpeechs[i].speechProbability;
            }
            int index = MeysamUtility.GetRandomIndexByChance(chances);

            textToShow.color = randomSpeechs[index].speechStartColor;
            textToShow.font = MeysamLocalization.GetCurrentAllowedFontAsset();
            textToShow.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
            textToShow.text = randomSpeechs[index].speechText;
            
            textToShow.fontSize = randomSpeechs[index].speechFontSize;

            onRandomSpeech = true;

            if (fadeOutRoutine != null)
            {
                StopCoroutine(fadeOutRoutine);
                fadeOutRoutine = null;
                fadeOutRoutine = StartCoroutine(FadeOut(randomSpeechs[index]));
            }
            else
                fadeOutRoutine = StartCoroutine(FadeOut(randomSpeechs[index]));
          
        }
    }

    public void ShowThankSpeech()
    {
        if (onEventSpeech || onHateSpeech)
            return;

        if (ec != null)
            if (!ec.IsVisibleToCamera())
                return;

        if (thankSpeechs.Count == 0)
            return;

        float probAppear = Random.Range(0f, 1f);
        if (probAppear <= thankSpeechShowProbability)
        {
            textToShow.enabled = true;
            textToShow.transform.position = gameObject.transform.position;

            float[] chances = new float[thankSpeechs.Count];
            for (int i = 0; i < thankSpeechs.Count; i++)
            {
                chances[i] = thankSpeechs[i].speechProbability;
            }
            int index = MeysamUtility.GetRandomIndexByChance(chances);

            textToShow.color = thankSpeechs[index].speechStartColor;
            textToShow.font = MeysamLocalization.GetCurrentAllowedFontAsset();
            textToShow.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
            textToShow.text = thankSpeechs[index].speechText;
         
            textToShow.fontSize = thankSpeechs[index].speechFontSize;

            onThankSpeech = true;
            if (fadeOutRoutine != null)
            {
                StopCoroutine(fadeOutRoutine);
                fadeOutRoutine = null;
                fadeOutRoutine = StartCoroutine(FadeOut(thankSpeechs[index]));
            }
            else
                fadeOutRoutine = StartCoroutine(FadeOut(thankSpeechs[index]));
        }

            
    }

    public void ShowHateSpeech()
    {
        if (onEventSpeech)
            return;

        if (ec != null)
            if (!ec.IsVisibleToCamera())
                return;

        if (hateSpeechs.Count == 0)
            return;


        float probAppear = Random.Range(0f, 1f);
        if (probAppear <= hateSpeechShowProbability)
        {
            textToShow.enabled = true;
            textToShow.transform.position = gameObject.transform.position;

            float[] chances = new float[hateSpeechs.Count];
            for (int i = 0; i < hateSpeechs.Count; i++)
            {
                chances[i] = hateSpeechs[i].speechProbability;
            }
            int index = MeysamUtility.GetRandomIndexByChance(chances);

            textToShow.font = MeysamLocalization.GetCurrentAllowedFontAsset();
            textToShow.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
            textToShow.color = hateSpeechs[index].speechStartColor;
            textToShow.text = hateSpeechs[index].speechText;
        
            textToShow.fontSize = hateSpeechs[index].speechFontSize;

            onHateSpeech = true;
            if (fadeOutRoutine != null)
            {
                StopCoroutine(fadeOutRoutine);
                fadeOutRoutine = null;
                fadeOutRoutine = StartCoroutine(FadeOut(hateSpeechs[index]));
            }
            else
                fadeOutRoutine = StartCoroutine(FadeOut(hateSpeechs[index]));
        }

      
    }

    public void ShowADSpeech()
    {

        if (onEventSpeech || onADSpeech)
            return;


        if (adSpeechs.Count == 0)
            return;

        textToShow.enabled = true;

            textToShow.transform.position = gameObject.transform.position;

            float[] chances = new float[adSpeechs.Count];
            for (int i = 0; i < adSpeechs.Count; i++)
            {
                chances[i] = adSpeechs[i].speechProbability;
            }
            int index = MeysamUtility.GetRandomIndexByChance(chances);

            textToShow.color = adSpeechs[index].speechStartColor;
            textToShow.font = MeysamLocalization.GetCurrentAllowedFontAsset();
            textToShow.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
            textToShow.text = adSpeechs[index].speechText;

            textToShow.fontSize = adSpeechs[index].speechFontSize;

            onADSpeech = true;

            if (fadeOutRoutine != null)
            {
                StopCoroutine(fadeOutRoutine);
                fadeOutRoutine = null;
                fadeOutRoutine = StartCoroutine(FadeOut(adSpeechs[index]));
            }
            else
                fadeOutRoutine = StartCoroutine(FadeOut(adSpeechs[index]));

        
    }

    IEnumerator RandomSpeechRoutine()
    {
        float waitTime = Random.Range(30, 100);
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            waitTime = Random.Range(30, 100);

            ShowRandomSpeech();

        }
    }

    public void ShowForcedSpeech(string speechToShow)
    {
        if (onRandomSpeech || onEventSpeech || onHateSpeech || onThankSpeech || onADSpeech)
            return;

        onEventSpeech = true;

        textToShow.enabled = true;

        textToShow.transform.position = gameObject.transform.position;
    

        Speech speech = new Speech();
        speech.speechText = speechToShow;
        speech.speechStartColor = defaultColor;
        speech.speechEndColor = defaultColor;
        speech.speechDuration = defaultDuration;
        speech.speechFont = MeysamLocalization.GetCurrentAllowedFontAsset();
        speech.speechFontSize = defaultFontSize;
        speech.speechFadeSpeed = defaultFadeSpeed;
        speech.speechDistinctItemDropCount = 0;
        speech.speechItemMinCount = null;
        speech.speechItemMaxCount = null;
        speech.speechItemTag = null;
        speech.speechItemProbability = null;
        speech.speechProbability = 1.0f;
        speech.speechSmoothness = defaultSmoothness;

        textToShow.font = MeysamLocalization.GetCurrentAllowedFontAsset();
        textToShow.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        textToShow.color = speech.speechStartColor;
        textToShow.text = speechToShow;
 
        textToShow.fontSize = speech.speechFontSize;

        onRandomSpeech = true;

        if (fadeOutRoutine != null)
        {
            StopCoroutine(fadeOutRoutine);
            fadeOutRoutine = null;
            fadeOutRoutine = StartCoroutine(FadeOut(speech));
        }
        else
            fadeOutRoutine = StartCoroutine(FadeOut(speech));

    }
}
