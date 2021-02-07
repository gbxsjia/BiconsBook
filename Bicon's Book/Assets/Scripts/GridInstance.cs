using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInstance : MonoBehaviour
{
    [SerializeField]
    private Sprite CharacterSprite;
    [SerializeField]
    private Sprite NormalSprite;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Transform SpriteTransform;

    public void UpdateGridInstance(Vector3 position,bool isCharacter)
    {
        transform.position = position;
        if (isCharacter)
        {
            spriteRenderer.sprite = CharacterSprite;
        }
        else
        {
            spriteRenderer.sprite = NormalSprite;
        }
    }
}
