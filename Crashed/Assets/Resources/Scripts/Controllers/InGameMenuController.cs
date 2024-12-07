using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour
{
    public GameObject inGameMenu;


    public void ShowMenu()
    {
        LocalizeTexts();

        GameObject.Find("dlgCenterScreen").GetComponent<CenterScreenDialogController>().HideMessage();
       
        Time.timeScale = 0;
        inGameMenu.SetActive(true);

        if (GameController.instance != null)
        {
            GameController.instance.showingDialogCount++;
        }
    }


    public void Restart()
    {

        Time.timeScale = 1;
        GenericDialogController dialog = GenericDialogController.Instance();
        dialog.Title(MeysamLocalization.GetLocalizaStringByKey("Alert!"));
        dialog.Message(MeysamLocalization.GetLocalizaStringByKey("Are you sure?"));
        dialog.OnAccept(MeysamLocalization.GetLocalizaStringByKey("Yes"), () => { // define what happens when user clicks Yes:

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            dialog.Hide();
        });

        dialog.OnDecline(MeysamLocalization.GetLocalizaStringByKey("No thanks"), () => {

            dialog.Hide();

        });
        dialog.Show();
    }

    public void Continue()
    {
        inGameMenu.SetActive(false);
        Time.timeScale = 1;

        if (GameController.instance != null)
        {
            GameController.instance.showingDialogCount--;
            if (GameController.instance.showingDialogCount < 0)
                GameController.instance.showingDialogCount = 0;
        }
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        GenericDialogController dialog = GenericDialogController.Instance();
        dialog.Title(MeysamLocalization.GetLocalizaStringByKey("Alert!"));
        dialog.Message(MeysamLocalization.GetLocalizaStringByKey("Are you sure?"));
        dialog.OnAccept(MeysamLocalization.GetLocalizaStringByKey("Yes"), () => { // define what happens when user clicks Yes:

            GameObject.Find("cnvMenu").SetActive(false);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            dialog.Hide();
            
        });

        dialog.OnDecline(MeysamLocalization.GetLocalizaStringByKey("No thanks"), () => dialog.Hide());
        dialog.Show();

      
       
    }

    public void Help()
    {
        Time.timeScale = 1;
        MessageDialogController dialog2 = MessageDialogController.Instance();
        dialog2.Title(MeysamLocalization.GetLocalizaStringByKey("Help"));
        //dialog2.Message("It's a turn-base game!\n Tap on the screen to move around.\n You can kill the enemies with a spear.\n You can destroy rocks with a pickaxe.\n You can remove bushes with an axe.\n Pay attention to your energy!\n By consuming meat, you will get hyped and can move double the distance on the game by touch and hold!");
        dialog2.Message(MeysamLocalization.GetLocalizaStringByKey("It's a turn-base game!\n Tap on the screen to move around.\n You can kill the enemies with a spear.\n You can destroy rocks with a pickaxe.\n You can remove bushes with an axe.\n Pay attention to your energy!\n By consuming meat, you will get hyped and can move double the distance on the game by touch and hold!"));
        dialog2.OnOk(MeysamLocalization.GetLocalizaStringByKey("Got It!"), () =>
        { // define what happens when user clicks Yes:
           

            dialog2.Hide();
           
        });
        dialog2.Show();
    }

    private void LocalizeTexts()
    {
        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnContinue")[0].GetComponentsInChildren<TMP_Text>()[0].font = MeysamLocalization.GetCurrentAllowedFontAsset();
        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnContinue")[0].GetComponentsInChildren<TMP_Text>()[0].isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnContinue")[0].GetComponentsInChildren<TMP_Text>()[0].text = MeysamLocalization.GetLocalizaStringByKey("Continue");

        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnRestart")[0].GetComponentsInChildren<TMP_Text>()[0].font = MeysamLocalization.GetCurrentAllowedFontAsset();
        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnRestart")[0].GetComponentsInChildren<TMP_Text>()[0].isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnRestart")[0].GetComponentsInChildren<TMP_Text>()[0].text = MeysamLocalization.GetLocalizaStringByKey("Restart");

        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnHelp")[0].GetComponentsInChildren<TMP_Text>()[0].font = MeysamLocalization.GetCurrentAllowedFontAsset();
        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnHelp")[0].GetComponentsInChildren<TMP_Text>()[0].isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnHelp")[0].GetComponentsInChildren<TMP_Text>()[0].text = MeysamLocalization.GetLocalizaStringByKey("Help");

        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnReturnToMainMenu")[0].GetComponentsInChildren<TMP_Text>()[0].font = MeysamLocalization.GetCurrentAllowedFontAsset();
        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnReturnToMainMenu")[0].GetComponentsInChildren<TMP_Text>()[0].isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        inGameMenu.FindGameObjectsInChildrenWithName("Panel")[0].FindGameObjectsInChildrenWithName("btnReturnToMainMenu")[0].GetComponentsInChildren<TMP_Text>()[0].text = MeysamLocalization.GetLocalizaStringByKey("ReturnToMainMenu");

    }
}
