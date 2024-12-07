using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenuController : MonoBehaviour
{

    public Button btnContinue;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("MUSIC"))
            PlayerPrefs.SetInt("MUSIC", 1);
        if (!PlayerPrefs.HasKey("AUDIO"))
            PlayerPrefs.SetInt("AUDIO", 1);
    }

    private void Start()
    {
        GameObject.Find("_Music").GetComponent<MusicController>().PlayMenuMusic();
        btnContinue.GetComponentInChildren<TMP_Text>().color = MeysamUtility.ColorYellowButtonText;
        btnContinue.GetComponent<Image>().color = MeysamUtility.ColorNormalButtonBorder;
        btnContinue.enabled = true;
        if (PlayerPrefs.HasKey("PlayerSaveData"))
        {
            SaveModel sm = SaveController.LoadSavedPlayerData();

            
            if (sm.level <= 1)
            {
                btnContinue.enabled = false;
                btnContinue.GetComponentInChildren<TMP_Text>().color = MeysamUtility.ColorGreyButtonText;
                btnContinue.GetComponent<Image>().color = MeysamUtility.ColorGreyButtonBorder;
            }
        }
        else
        {
            btnContinue.enabled = false;
            btnContinue.GetComponentInChildren<TMP_Text>().color = MeysamUtility.ColorGreyButtonText;
            btnContinue.GetComponent<Image>().color = MeysamUtility.ColorGreyButtonBorder;
        }


        LocalizeTexts();

    }

    /// <summary>
    /// start a new game
    /// </summary>
    public void NewGame()
    {
        if (PlayerPrefs.HasKey("PlayerSaveData"))
        {
            SaveModel sm = SaveController.LoadSavedPlayerData();


            if (sm.level <= 1)
            {
                PlayerPrefs.DeleteKey("LOADGAME");
                PlayerPrefs.DeleteKey("PlayerSaveData");
                GameObject.Find("cnvMenu").SetActive(false);
                // Use a coroutine to load the Scene in the background
                StartCoroutine(AsynchronousLoad(2));
            }
            else
            {
                GenericDialogController dialog = GenericDialogController.Instance();
                dialog.Title(MeysamLocalization.GetLocalizaStringByKey("Alert!"));
                dialog.Message(MeysamLocalization.GetLocalizaStringByKey("Are you sure? You will lose all of your saved progress!"));
                dialog.OnAccept(MeysamLocalization.GetLocalizaStringByKey("Yes"), () => { // define what happens when user clicks Yes:
                    PlayerPrefs.DeleteKey("LOADGAME");
                    PlayerPrefs.DeleteKey("PlayerSaveData");
                    //SceneManager.LoadScene(2);

                    GameObject.Find("cnvMenu").SetActive(false);
                    // Use a coroutine to load the Scene in the background
                    StartCoroutine(AsynchronousLoad(2));
                    dialog.Hide();
                });

                dialog.OnDecline(MeysamLocalization.GetLocalizaStringByKey("No thanks"), () => dialog.Hide());
                dialog.Show();
            }
        }
        else
        {
            PlayerPrefs.DeleteKey("LOADGAME");
            PlayerPrefs.DeleteKey("PlayerSaveData");
            GameObject.Find("cnvMenu").SetActive(false);
            // Use a coroutine to load the Scene in the background
            StartCoroutine(AsynchronousLoad(2));
        }
    }

    /// <summary>
    /// exit the game
    /// </summary>
    public void Continue()
    {
        PlayerPrefs.SetInt("LOADGAME", 1);
        

        GameObject.Find("cnvMenu").SetActive(false);
        // Use a coroutine to load the Scene in the background
        StartCoroutine(AsynchronousLoad(2));
    }

    public void ExitGame()
    {

        GenericDialogController dialog = GenericDialogController.Instance();
        dialog.Title(MeysamLocalization.GetLocalizaStringByKey("Alert!"));
        dialog.Message(MeysamLocalization.GetLocalizaStringByKey("Are you sure?"));
        dialog.OnAccept(MeysamLocalization.GetLocalizaStringByKey("Yes"), () => { // define what happens when user clicks Yes:
    
            GameObject.Find("cnvMenu").SetActive(false);
           
            dialog.Hide();
            Application.Quit();
        });

        dialog.OnDecline(MeysamLocalization.GetLocalizaStringByKey("No thanks"), () => dialog.Hide());
        dialog.Show();
      

    }

    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/boredalchemistgames/");

    }

    public void Website()
    {
        Application.OpenURL("https://www.boredalchemist.com/");
    }

    IEnumerator AsynchronousLoad(int scene)
    {
        

        //Show loading
        LoadingDialogController dialog = LoadingDialogController.Instance();
        dialog.Show();

        //yield return null;
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        //ao.allowSceneActivation = false;




        while (!ao.isDone)
        {
            yield return new WaitForSeconds(0.5f);
            //yield return null;
        }

        dialog.Hide();
        //ao.allowSceneActivation = true;
        yield return null;
    }

    private void LocalizeTexts()
    {
        btnContinue.GetComponentInChildren<TMP_Text>().font = MeysamLocalization.GetCurrentAllowedFontAsset();
        btnContinue.GetComponentInChildren<TMP_Text>().isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        btnContinue.GetComponentInChildren<TMP_Text>().text = MeysamLocalization.GetLocalizaStringByKey("Continue");
    

        GameObject.Find("btnNewGame").GetComponentInChildren<TMP_Text>().font = MeysamLocalization.GetCurrentAllowedFontAsset();
        GameObject.Find("btnNewGame").GetComponentInChildren<TMP_Text>().isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        GameObject.Find("btnNewGame").GetComponentInChildren<TMP_Text>().text = MeysamLocalization.GetLocalizaStringByKey("New Game");

        GameObject.Find("btnInstagram").GetComponentInChildren<TMP_Text>().font = MeysamLocalization.GetCurrentAllowedFontAsset();
        GameObject.Find("btnInstagram").GetComponentInChildren<TMP_Text>().isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        GameObject.Find("btnInstagram").GetComponentInChildren<TMP_Text>().text = MeysamLocalization.GetLocalizaStringByKey("Instagram");

        GameObject.Find("btnWebsite").GetComponentInChildren<TMP_Text>().font = MeysamLocalization.GetCurrentAllowedFontAsset();
        GameObject.Find("btnWebsite").GetComponentInChildren<TMP_Text>().isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        GameObject.Find("btnWebsite").GetComponentInChildren<TMP_Text>().text = MeysamLocalization.GetLocalizaStringByKey("Website");

        GameObject.Find("btnExit").GetComponentInChildren<TMP_Text>().font = MeysamLocalization.GetCurrentAllowedFontAsset();
        GameObject.Find("btnExit").GetComponentInChildren<TMP_Text>().isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        GameObject.Find("btnExit").GetComponentsInChildren<TMP_Text>()[0].text = MeysamLocalization.GetLocalizaStringByKey("Exit");

        GameObject.Find("txtVersion").GetComponentInChildren<TMP_Text>().font = MeysamLocalization.GetCurrentAllowedFontAsset();
        GameObject.Find("txtVersion").GetComponentInChildren<TMP_Text>().isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        GameObject.Find("txtVersion").GetComponent<TMP_Text>().text = MeysamLocalization.GetLocalizaStringByKey("Version");

        GameObject.Find("txtTitle").GetComponentInChildren<TMP_Text>().font = MeysamLocalization.GetCurrentAllowedFontAsset();
        GameObject.Find("txtTitle").GetComponentInChildren<TMP_Text>().isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        GameObject.Find("txtTitle").GetComponent<TMP_Text>().text = MeysamLocalization.GetLocalizaStringByKey("Title");
        

    }



}
