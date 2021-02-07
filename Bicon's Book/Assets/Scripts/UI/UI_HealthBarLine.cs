using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HealthBarLine : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private HealthBar healthBar;
    public int Count;

    public Vector3[] Positions;
    private BodyPart bodypart;
    private void Start()
    {
        StartCoroutine(StartProcess());
    }
    private IEnumerator StartProcess()
    {
        yield return null;
        bodypart = healthBar.bodyPart;
        bodypart.MouseEnterEvent += Bodypart_MouseEnterEvent;
        bodypart.MouseExitEvent += Bodypart_MouseExitEvent;
        Positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(Positions);
        Vector3 start = Positions[0];
        for (int i = 0; i < Positions.Length; i++)
        {
            Positions[i] -= start;
        }
        Count = lineRenderer.positionCount;
        gameObject.SetActive(false);
    }

    private void Bodypart_MouseExitEvent()
    {
        gameObject.SetActive(false);
    }

    private void Bodypart_MouseEnterEvent()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Count > 0)
        {
            for (int i = 0; i < Count - 1; i++)
            {
                lineRenderer.SetPosition(i, transform.position + Positions[i]);
            }
            lineRenderer.SetPosition(Count - 1, bodypart.transform.position);
        }
    }
}
