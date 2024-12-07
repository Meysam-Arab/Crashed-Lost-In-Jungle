using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SortingLayerController.SetDynamicSortingLayer(spriteRenderer, Mathf.RoundToInt(gameObject.transform.position.y), Mathf.RoundToInt(gameObject.transform.position.x));
    }
}
