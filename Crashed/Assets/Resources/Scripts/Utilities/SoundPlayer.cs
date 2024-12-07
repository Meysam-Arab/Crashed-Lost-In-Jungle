using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer
{

	private AudioSource audioSource;
	private AudioClip audioClip;
	private AudioClip[] audioClips;

	public float lowPichRange = .95f;
	public float highPichRange = 1.05f;

	public void Initialize(AudioSource newAudioSource, AudioClip newAuodioClip,AudioClip[] newAuodioClips)
	{
		audioSource = newAudioSource;
		audioClip = newAuodioClip;
		audioClips = newAuodioClips;
	}

	public void PlaySound(bool looping, float delay)
	{
		if (looping)
			audioSource.loop = true;
		else
			audioSource.loop = false;
		audioSource.clip = audioClip;
		audioSource.PlayDelayed (delay);

	}

	public void StopSound()
	{
		audioSource.Stop();

	}

	public void PlaySounds( bool randomPitch, bool looping)
	{
		if (looping)
			audioSource.loop = true;
		else
			audioSource.loop = false;
		
		if (randomPitch)
		{
			float randomPitchValue = Random.Range (lowPichRange, highPichRange);

			audioSource.pitch = randomPitchValue;
		}
		int randomIndex = Random.Range (0, audioClips.Length);

		audioSource.clip = audioClips [randomIndex];
		audioSource.Play ();
	}

	public bool IsPlaying()
	{
		return audioSource.isPlaying;
	}

}
