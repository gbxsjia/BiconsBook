using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TextEffect : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI TextMesh;
    [SerializeField]
    private Animator animator;

    public string content;
    public Color color;
    public float frontSize;
    public string animationName;

    public void ShowTextMesh(string text ,Color c, float size, string animName)
    {
        content = text;
        color = c;
        frontSize = size;
        animationName = animName;

        TextMesh.text = text;
        TextMesh.color = c;
        TextMesh.fontSize = size;
        animator.Play(animName);
        TextMesh.enabled = true;
    }

}
