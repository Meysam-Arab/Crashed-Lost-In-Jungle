using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFaderController : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed = 100f;
    public bool sceneStarting = true;

    public float waitClearDuration = 3f; // This will be your time in seconds.
    public float waitBlackenDuration = 3f; // This will be your time in seconds.
    public float waitInBetweenDuration = 3f; // This will be your time in seconds.
    void Awake()
    {
        FadeImg.color = Color.black;
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
      
    }

    public void SetColorToBlack()
    {
        FadeImg.color = Color.black;
    }

    public void QuestProcess(float? waitInBetweenDuration = null, float? waitBlackenDuration = null, float? waitClearDuration = null)
    {
        if (waitInBetweenDuration != null)
            this.waitInBetweenDuration = (float)waitInBetweenDuration;
        else
            this.waitInBetweenDuration = 3f;

        if (waitBlackenDuration != null)
            this.waitBlackenDuration = (float)waitBlackenDuration;
        else
            this.waitBlackenDuration = 0;

        if (waitClearDuration != null)
            this.waitClearDuration = (float)waitClearDuration;
        else
            this.waitClearDuration = 3f;


        StartCoroutine(QuestProcessRoutine());
    }

    private IEnumerator QuestProcessRoutine()
    {
        StartCoroutine("BlackenSceneRoutine");

        yield return new WaitForSeconds(waitInBetweenDuration);

        StartCoroutine(ClearSceneRoutine());
    }

    void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    public void StartScene()
    {
        StartCoroutine(StartSceneRoutine());
    }

    private IEnumerator StartSceneRoutine()
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;
        FadeImg.color = Color.black;
        yield return new WaitForSeconds(waitClearDuration);
        do
        {
            // Start fading towards clear.
            FadeToClear();

            // If the screen is almost clear...
            if (FadeImg.color.a <= 0.05f)
            {
                // ... set the colour to clear and disable the RawImage.
                FadeImg.color = Color.clear;
                FadeImg.enabled = false;

                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    private IEnumerator EndSceneRoutine(int SceneNumber)
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;
        FadeImg.color = Color.clear;
        yield return new WaitForSeconds(waitBlackenDuration);
        do
        {
            // Start fading towards black.
            FadeToBlack();

            // If the screen is almost black...
            if (FadeImg.color.a >= 0.95f)
            {
                FadeImg.color = Color.black;
                // ... reload the level
                SceneManager.LoadScene(SceneNumber);
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void EndScene(int SceneNumber)
    {
        sceneStarting = false;
        StartCoroutine("EndSceneRoutine", SceneNumber);
    }

    private IEnumerator BlackenSceneRoutine()
    {
        

        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;
        FadeImg.color = Color.clear;
        yield return new WaitForSeconds(waitBlackenDuration);
        do
        {
            // Start fading towards black.
            FadeToBlack();

            // If the screen is almost black...
            if (FadeImg.color.a >= 0.95f)
            {
                FadeImg.color = Color.black;
            
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void BlackenScene(float? waitBlackenDuration = null)
    {
        if (waitBlackenDuration != null)
            this.waitBlackenDuration = (float)waitBlackenDuration;
        else
            this.waitBlackenDuration = 0;

       
        StartCoroutine("BlackenSceneRoutine");
    }

    public void ClearScene(float? waitClearDuration = null)
    {

        if (waitClearDuration != null)
            this.waitClearDuration = (float)waitClearDuration;
        else
            this.waitClearDuration = 3f;

        StartCoroutine(ClearSceneRoutine());
    }

    private IEnumerator ClearSceneRoutine()
    {

        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;
        FadeImg.color = Color.black;
        yield return new WaitForSeconds(waitClearDuration);
        do
        {
            // Start fading towards clear.
            FadeToClear();

            // If the screen is almost black...
            if (FadeImg.color.a <= 0.05f)
            {
                // ... set the colour to clear and disable the RawImage.
                FadeImg.color = Color.clear;
                FadeImg.enabled = false;

                yield break;
            }
            else
            {
                
                yield return null;
            }
        } while (true);
    }
}
