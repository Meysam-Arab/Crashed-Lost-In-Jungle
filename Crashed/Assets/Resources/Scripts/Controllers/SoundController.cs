using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour
{
    private AudioSource _audioSource;
    private SoundPlayer soundPlayer;

    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;			//The highest a sound effect will be randomly pitched.

   

    //for controlling music
    [HideInInspector]
    public bool playSounds;
    //////////////////////////////////

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();

    }


    public void PlaySound(bool loop, AudioClip soundClip, float delay)
    {
        //meysam - check if background sound and music are allowed...
   

        playSounds = PlayerPrefs.GetInt("AUDIO") == 1;
        /////////////////////////////////////////////////////////////

        if (soundPlayer != null)
            soundPlayer.StopSound();
        soundPlayer = new SoundPlayer();
        soundPlayer.Initialize(_audioSource, soundClip, null);
        soundPlayer.PlaySound(loop, delay);

        if (!playSounds)
        {
            StopSound();
            return;
        }
    }



    public void StopSound()
    {
        if (soundPlayer != null)
            soundPlayer.StopSound();

        if (_audioSource.isPlaying)
            _audioSource.Stop();
    }


    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        _audioSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        _audioSource.clip = clips[randomIndex];

        //Play the clip.
        _audioSource.Play();
    }

}

