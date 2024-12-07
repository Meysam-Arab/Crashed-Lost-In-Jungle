using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip[] menuSoundClips;
    public AudioClip[] creditSoundClips;
    public AudioClip[] peaceSoundClips;
    private SoundPlayer soundPlayer;

    private bool isMenuMusicPlaying;
    private bool isCreditMusicPlaying;
    private bool isGameMusicPlaying;

    //for controlling music
    [HideInInspector]
    public bool playMusics;
    //////////////////////////////////

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();

        
    }

    private void Start()
    {
        //meysam - check if background sound and music are allowed...

        playMusics = PlayerPrefs.GetInt("MUSIC") == 1;

        /////////////////////////////////////////////////////////////

        SetFalseAllMusic();

        soundPlayer = new SoundPlayer();
        soundPlayer.Initialize(_audioSource, null, peaceSoundClips);

    }


    public void PlayMenuMusic()
    {
        //meysam - check if background sound and music are allowed...

        playMusics = PlayerPrefs.GetInt("MUSIC") == 1;
        /////////////////////////////////////////////////////////////

        if (isMenuMusicPlaying || !playMusics)
        {
            if (!playMusics)
                StopMusic();
            return;
        }

        
        SetFalseAllMusic();
        soundPlayer.StopSound();
        soundPlayer = new SoundPlayer();
        soundPlayer.Initialize(_audioSource, null, menuSoundClips);
        soundPlayer.PlaySounds(true, true);
        isMenuMusicPlaying = true;
    }

    public void PlayCreditMusic()
    {
        //meysam - check if background sound and music are allowed...
    

        playMusics = PlayerPrefs.GetInt("MUSIC") == 1;

        /////////////////////////////////////////////////////////////

        if (isCreditMusicPlaying || !playMusics)
        {
            if (!playMusics)
                StopMusic();
            return;
        }
        SetFalseAllMusic();
        soundPlayer.StopSound();
        soundPlayer = new SoundPlayer();
        soundPlayer.Initialize(_audioSource, null, creditSoundClips);
        soundPlayer.PlaySounds(true, true);
        isCreditMusicPlaying = true;
    }

    public void PlayGamePeaceMusic()
    {
        //meysam - check if background sound and music are allowed...
        playMusics = PlayerPrefs.GetInt("MUSIC") == 1;



        /////////////////////////////////////////////////////////////

        if (isGameMusicPlaying || !playMusics)
        {
            if (!playMusics)
                StopMusic();
            return;
        }
    
        SetFalseAllMusic();
        soundPlayer.StopSound();
        soundPlayer = new SoundPlayer();
        soundPlayer.Initialize(_audioSource, null, peaceSoundClips);
        soundPlayer.PlaySounds(true, true);
        isGameMusicPlaying = true;
    }

   
    public void StopMusic()
    {
        if(isMenuMusicPlaying ||
            isGameMusicPlaying ||
            isCreditMusicPlaying)
            soundPlayer.StopSound();

        if(_audioSource.isPlaying)
            _audioSource.Stop();

        isMenuMusicPlaying = false;
        isCreditMusicPlaying = false;
        isGameMusicPlaying = false;
    }

    public void SetFalseAllMusic()
    {
        isMenuMusicPlaying = false;
        isCreditMusicPlaying = false;
        isGameMusicPlaying = false;
    }
}
