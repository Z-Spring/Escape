using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HeartRateMonitor : MonoBehaviour
{
    [SerializeField] private float animDuration = 5f;

    private LineRenderer lineRenderer;
    private Vector3[] linePoints;
    private int pointsCount;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        pointsCount = lineRenderer.positionCount;
        linePoints = new Vector3[pointsCount];
        for (int i = 0; i < pointsCount; i++)
        {
            linePoints[i] = lineRenderer.GetPosition(i);
        }

        StartCoroutine(AnimateLine());
    }
    

    private IEnumerator AnimateLine()
    {
        while (true)
        {
            float segmentDuration = animDuration / pointsCount;

            for (int i = 0; i < pointsCount - 1; i++)
            {
                float startTime = Time.time;

                Vector3 startPos = linePoints[i] + transform.forward * 20f;

                Vector3 endPos = linePoints[i + 1] + transform.forward * 20f;

                Vector3 currentPos = startPos;
                while (Vector3.Distance(currentPos, endPos) > 0.001f)
                {
                    float t = (Time.time - startTime) / segmentDuration;
                    currentPos = Vector3.Lerp(startPos, endPos, t);
                    for (int j = i + 1; j < pointsCount; j++)
                    {
                        lineRenderer.SetPosition(j, currentPos);
                    }

                    yield return null;
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
}