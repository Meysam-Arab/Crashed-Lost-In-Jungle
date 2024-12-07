
using UnityEngine;
using UnityEngine.UI;

public class DamageFlashController : MonoBehaviour
{

    public Image flashDamageImage;

    public static Color damagedColor = new Color32(138, 3, 3, 255);

    public static DamageFlashController dfc;

    public AudioClip[] bloodSoundClips;


    private bool damaged = false;
    [HideInInspector]
    public bool doDamage = false;
 

    private float damageFlashSpeed = 0.075f; // This will determine the defaultSmoothness of the lerp. Smaller values are smoother. Really it's the time between updates.

    private void Awake()
    {
        flashDamageImage.gameObject.SetActive(false);
        if (dfc == null)
            dfc = this;
    }

    private void Start()
    {
        flashDamageImage.gameObject.SetActive(false);      
    }

    void Update()
    {
        if (doDamage)
        {
            DamageFlash();
        }
    }


    public void ShowDamage()
    {
        flashDamageImage.gameObject.SetActive(true);
        damaged = true;
        doDamage = true;

        int index = Random.Range(1, 9);
        Sprite damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_1_Sprite_Refrence);

        if (index == 1)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_1_Sprite_Refrence);
        else if (index == 2)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_2_Sprite_Refrence);
        else if (index == 3)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_3_Sprite_Refrence);
        else if (index == 4)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_4_Sprite_Refrence);
        else if (index == 5)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_5_Sprite_Refrence);
        else if (index == 6)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_6_Sprite_Refrence);
        else if (index == 7)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_7_Sprite_Refrence);
        else if (index == 8)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_8_Sprite_Refrence);

        flashDamageImage.gameObject.GetComponent<Image>().sprite = damageSprite;

        if (PlayerPrefs.GetInt("AUDIO") == 1)
        {
            AudioSource asTemp = flashDamageImage.gameObject.GetComponent<AudioSource>();
            int indexRandom = Random.Range(0, bloodSoundClips.Length);
            asTemp.clip = bloodSoundClips[indexRandom];
            asTemp.Play();
        }
       


        }

        private void DamageFlash()
    {
        if(damaged)
        {
            flashDamageImage.color = damagedColor;
            damaged = false;
        }
        else
        {
            flashDamageImage.color = Color.Lerp(flashDamageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
            if (flashDamageImage.color.a < 0.9)
            {
                doDamage = false;
                flashDamageImage.color = Color.clear;
                flashDamageImage.gameObject.GetComponent<Image>().sprite = null;
                flashDamageImage.gameObject.SetActive(false);
            }
               
        }
    }
}
