using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 start = Vector3.zero;
    private Vector3 end = Vector3.zero;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void CreateLine(Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount = 2;
        this.start = start;
        this.end = end;
    }

    private void Update()
    {
        if (start != Vector3.zero && end != Vector3.zero)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }
    }

    public void UpdateLine(float zOffset)
    {
        start.z -= zOffset;
        end.z -= zOffset;
    }
}