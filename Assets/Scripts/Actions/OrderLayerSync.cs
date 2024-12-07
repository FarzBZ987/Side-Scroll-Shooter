using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OrderLayerSync : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform targetTransform;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        spriteRenderer.sortingOrder = -Mathf.RoundToInt(targetTransform.position.y * 100);
    }
}