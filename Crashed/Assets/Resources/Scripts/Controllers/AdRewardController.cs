using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellPlusSDK;
using UnityEngine.UI;

public class AdRewardController : MonoBehaviour
{

    public Button btnAdVideoReward;
    private Animator anBtnAdVideoReward;
    private Coroutine checkAdVideoRewardRoutine;
    private Coroutine anVideoRewardAnimationRoutine;

    [HideInInspector] public PlayerController pc;//Store a reference to our PlayerController.

    private static string TAPSELLPLUS_KEY = "jeqanjtrpkgmtmosirqjrtnikacgrqmifapsmdtilseoirlrjfsretrjlpefnnjtiforoj";
    private static string ZONE_ID = "62b086e66b58345ef0019d65";
    private static string _responseId = null;

    // Start is called before the first frame update
    void Start()
    {
       

        TapsellPlus.Initialize(TAPSELLPLUS_KEY,
            adNetworkName => Debug.Log(adNetworkName + " Initialized Successfully."),
            error => Debug.Log(error.ToString()));

        anBtnAdVideoReward = btnAdVideoReward.GetComponent<Animator>();
        anBtnAdVideoReward.StopPlayback();
        anBtnAdVideoReward.enabled = false;
        Request();
    }

    public void Request()
    {
       
        TapsellPlus.RequestRewardedVideoAd(ZONE_ID,

                  tapsellPlusAdModel => {
                      Debug.Log("on response " + tapsellPlusAdModel.responseId);
                      _responseId = tapsellPlusAdModel.responseId;
                      btnAdVideoReward.gameObject.SetActive(true);
                      if (checkAdVideoRewardRoutine != null)
                      {
                          StopCoroutine(checkAdVideoRewardRoutine);
                          checkAdVideoRewardRoutine = null;
                      }
                      if (anVideoRewardAnimationRoutine == null)
                      {
                          anVideoRewardAnimationRoutine = StartCoroutine(RandomAdVideoAnimation());
                      }
                      

                  },
                  error => {
                      Time.timeScale = 1;
                      _responseId = null;
                      Debug.Log("Error " + error.message);
                      btnAdVideoReward.gameObject.SetActive(false);
                  }
              );
    }
    public void Show()
    {
        if (anVideoRewardAnimationRoutine != null)
        {
            StopCoroutine(anVideoRewardAnimationRoutine);
            anVideoRewardAnimationRoutine = null;
        }
        Time.timeScale = 0;
        TapsellPlus.ShowRewardedVideoAd(_responseId,

                  tapsellPlusAdModel => {
                      Debug.Log("onOpenAd " + tapsellPlusAdModel.zoneId);
                      Time.timeScale = 1;
                      _responseId = null;
                      btnAdVideoReward.gameObject.SetActive(false);
                  },
                  tapsellPlusAdModel => {
                      Debug.Log("Meysam onReward " + tapsellPlusAdModel.zoneId);


                      Time.timeScale = 1;
                      _responseId = null;
                      btnAdVideoReward.gameObject.SetActive(false);

                 
                      if (checkAdVideoRewardRoutine == null)
                          StartCoroutine(CheckForVideoAd());

                      //drop energy
                      string[] energyTags = new string[] { EnergyModel.TAG_ENERGY_BANANA, EnergyModel.TAG_ENERGY_COCONUT, EnergyModel.TAG_ENERGY_MEAT, EnergyModel.TAG_ENERGY_PINEAPPLE };
                      int randomIndex = Random.Range(0, energyTags.Length);
                      try
                      {
                          pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                          Object tmp = RefrenceRepository.GetPrefabByTag(energyTags[randomIndex]);
                          GameObject itemGO = Instantiate(tmp, pc.gameObject.transform.position, Quaternion.identity) as GameObject;
                     
                          itemGO.transform.SetParent(GameObject.Find("Items").transform);
                      }
                      catch (System.Exception ex)
                      {
                          Debug.Log("Meysam onReward - drop energy error: "+ ex.Message);
                          throw;
                      }
                     


                      Debug.Log("Meysam onReward - drop energy end");


                  },
                  tapsellPlusAdModel => {
                      Debug.Log("onCloseAd " + tapsellPlusAdModel.zoneId);

                     

                      Time.timeScale = 1;
                      _responseId = null;
                      btnAdVideoReward.gameObject.SetActive(false);


                  },
                  error => {
                      Debug.Log("onError " + error.errorMessage);
                      Time.timeScale = 1;
                      _responseId = null;
                      btnAdVideoReward.gameObject.SetActive(false);
                  }
              );
    }

    IEnumerator CheckForVideoAd()
    {
        while (true)
        {
            float randomTime = Random.Range(30, 90);
            yield return new WaitForSeconds(randomTime);
            Request();
            break;
        }


        checkAdVideoRewardRoutine = null;
        yield return null;
    }


    IEnumerator RandomAdVideoAnimation()
    {

        Debug.Log("in RandomAdVideoAnimation");
        while (true)
        {
            
            if (anBtnAdVideoReward.enabled == true)
            {
                yield return new WaitForSeconds(1f);
                anBtnAdVideoReward.StopPlayback();
                anBtnAdVideoReward.enabled = false;
            }
            else
            {
                float randomTime = Random.Range(3, 5);
                yield return new WaitForSeconds(randomTime);
                anBtnAdVideoReward.enabled = true;
                anBtnAdVideoReward.Rebind();
            }
                
          
        }
    }
}
