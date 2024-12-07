using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteController : MonoBehaviour
{
    private bool hasAudio;
    private bool hasMusic;

    public GameObject btnAudio;
    public Sprite audioOff;
    public Sprite audioOn;

    public GameObject btnMusic;
    public Sprite musicOff;
    public Sprite musicOn;

    // Start is called before the first frame update
    void Start()
    {
        hasAudio = PlayerPrefs.GetInt("AUDIO") == 1;

        if (hasAudio)
            btnAudio.GetComponent<Image>().sprite = audioOn;
        else
            btnAudio.GetComponent<Image>().sprite = audioOff;


        hasMusic = PlayerPrefs.GetInt("MUSIC") == 1;
        if (hasMusic)
            btnMusic.GetComponent<Image>().sprite = musicOn;
        else
            btnMusic.GetComponent<Image>().sprite = musicOff;
    }

    public void AudioPressed()
    {
        hasAudio = !hasAudio;
        PlayerPrefs.SetInt("AUDIO", hasAudio ? 1 : 0);

        if (hasAudio)
            btnAudio.GetComponent<Image>().sprite = audioOn;
        else
            btnAudio.GetComponent<Image>().sprite = audioOff;

    }

    public void MusicPressed()
    {
        hasMusic = !hasMusic;
        PlayerPrefs.SetInt("MUSIC", hasMusic ? 1 : 0);
        if (hasMusic)
            btnMusic.GetComponent<Image>().sprite = musicOn;
        else
            btnMusic.GetComponent<Image>().sprite = musicOff;


        GameObject.Find("_Music").GetComponent<MusicController>().PlayMenuMusic();
    }
}
