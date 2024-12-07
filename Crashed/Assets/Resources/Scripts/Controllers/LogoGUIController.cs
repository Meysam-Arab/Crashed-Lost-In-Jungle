using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoGUIController : MonoBehaviour
{
    ScreenFaderController sfc;

    public GameObject logoGO;
    public GameObject posterGO;
    public Animator anPoster;
    public Image pnlImage;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anPoster.enabled = false;
        GameObject blackScreen = GameObject.FindGameObjectWithTag("BlackScreen");
        sfc = blackScreen.GetComponent<ScreenFaderController>();

        logoGO.SetActive(false);
        posterGO.SetActive(false);

        StartCoroutine(LoadSceneWithDelay());
    }

  

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(1); //wait
        logoGO.SetActive(true);
        pnlImage.color = new Color32(255, 255, 255, 255);
        sfc.ClearScene();

   

        yield return new WaitForSeconds(6); //wait
  

        sfc.BlackenScene();

        yield return new WaitForSeconds(3); //wait
        logoGO.SetActive(false);

        yield return new WaitForSeconds(1); //wait
        posterGO.SetActive(true);
      
        pnlImage.color = new Color32(0, 0, 0, 255);
        sfc.ClearScene();

        yield return new WaitForSeconds(6); //wait
        anPoster.enabled = true;
        if (PlayerPrefs.GetInt("AUDIO") == 1)
        {
            audioSource.Play();
        }
        

        yield return new WaitForSeconds(4); //wait
        sfc.BlackenScene();

        yield return new WaitForSeconds(3); //wait
        logoGO.SetActive(false);

        yield return new WaitForSeconds(1); //wait
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(1);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }
        yield break;
            
    }
}
