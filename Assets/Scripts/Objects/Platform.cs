using ColdCry.Utility;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Platform : MonoBehaviour
{
    // wykorzystane do object poola
    public string poolParent;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    // kladzie przeciwnika nad kladka
    public void PutEnemy(GameObject gameObject)
    {
        gameObject.transform.position = transform.position + new Vector3( 0, boxCollider.bounds.extents.y );
        gameObject.transform.parent = transform;
    }
}
