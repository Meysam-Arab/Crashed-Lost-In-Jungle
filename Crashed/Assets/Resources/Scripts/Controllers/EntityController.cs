using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{

    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource audioSource;
    public AudioClip[] moveSounds;                //Audio clips to play when entity moves.

    protected virtual void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        SortingLayerController.SetDynamicSortingLayer(spriteRenderer, Mathf.RoundToInt(gameObject.transform.position.y), Mathf.RoundToInt(gameObject.transform.position.x));
    }

    public bool IsVisibleToCamera()
    {
        if (GetComponent<SpriteRenderer>().isVisible)
            return true;
        return false;
    }
}
