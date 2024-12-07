using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadeController : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    protected AudioSource audioSource;
    public AudioClip destroyClip;


    protected Animator animator;


    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();

      
    }

    private void Start()
    {
        SortingLayerController.SetDynamicSortingLayer(spriteRenderer, Mathf.RoundToInt(gameObject.transform.position.y), Mathf.RoundToInt(gameObject.transform.position.x));

        if(animator != null)
        {
            animator.enabled = false;
            StartCoroutine(AnimatorRoutine());
        }
            
    }

    public void DelayedDestroy()
    {
        if(destroyClip != null && PlayerPrefs.GetInt("AUDIO") == 1)
        {
            float duration = destroyClip.length + 1f;
            audioSource.clip = destroyClip;
            audioSource.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            StartCoroutine(DelayedDestroyRoutine(duration));
        }
        else
            Destroy(this.gameObject);


    }

    public float GetDestroyClipDuration()
    {
        if (destroyClip != null)
            return destroyClip.length;
        return 0;
    }
    public void DelayedDestroyNoDisableSR()
    {
        if (destroyClip != null && PlayerPrefs.GetInt("AUDIO") == 1)
        {
            float duration = destroyClip.length + 1f;
            audioSource.clip = destroyClip;
            audioSource.Play();
            GetComponent<BoxCollider2D>().enabled = false;

            StartCoroutine(DelayedDestroyRoutine(duration));
        }
        else
            Destroy(this.gameObject);


    }

    IEnumerator DelayedDestroyRoutine(float duration)
    {
       
        yield return new WaitForSeconds(duration);

        Destroy(this.gameObject);
        yield break;
    }

  
    IEnumerator AnimatorRoutine()
    {
        float waitTime = Random.Range(1, 20);
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            waitTime = Random.Range(1, 20);

            animator.enabled = true;
            yield return new WaitForSeconds(waitTime);
            animator.enabled = false;

        }
    }
}
