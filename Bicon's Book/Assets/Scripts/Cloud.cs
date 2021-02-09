using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    SpriteRenderer ThisSpriteRenderer;
    [SerializeField] Vector3 StartColor;
    public float TargetProcess = 0.33f;
    public float CurrentProcess = 0;
    void Start()
    {
        ThisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ThisSpriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }

    private void Update()
    {
        if(CurrentProcess < TargetProcess)
        {
            CurrentProcess += 0.01f;
        }
        Vector3 ColorAmount = Vector3.Lerp(StartColor, Vector3.one, CurrentProcess);
        ThisSpriteRenderer.color = new Color(ColorAmount.x,ColorAmount.y,ColorAmount.z);
    }

}
