using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private bool targetDetected;
    private CollissionSensesDataSO collissionData;
    private GameObject target;
    private Transform detectorOrigin;
    private Coroutine detectionCoroutine;
    private WaitForSeconds waitForSeconds = new(0.2f);

    public bool TargetDetected => targetDetected;
    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            targetDetected = target != null; // Only Assign if Target is not null
        }
    }

    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;
    public float DistanceToClosestTarget { get => target != null ? (target.transform.position - detectorOrigin.position).sqrMagnitude : Mathf.Infinity; }

    public void SetCollissionData(CollissionSensesDataSO data)
    {
        collissionData = data;
    }

    private void Awake()
    {
        detectorOrigin = transform;
    }

    private void OnEnable()
    {
        detectionCoroutine = StartCoroutine(DetectionCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(detectionCoroutine);
    }

    private IEnumerator DetectionCoroutine()
    {
        while (true)
        {
            if (collissionData == null)
            {
                yield return null;
            }

            yield return waitForSeconds;

            DetectTargets();
        }
    }

    private void DetectTargets()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll((Vector2)detectorOrigin.position + collissionData.DetectorOriginOffset, collissionData.TargetDetectorSize, 0, collissionData.TargetMask);
        if (targets != null)
        {
            if (targets.Length > 0)
            {
                foreach (Collider2D targetDetected in targets)
                {
                    float distanceToTarget = (targetDetected.transform.position - detectorOrigin.position).sqrMagnitude;

                    if (distanceToTarget < DistanceToClosestTarget)
                    {
                        target = targetDetected.gameObject;
                    }
                }
            }
            else
            {
                target = null;
            }
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (detectorOrigin == null) return;

        Gizmos.color = collissionData.IsNotCollidingColor;

        if (targetDetected) Gizmos.color = collissionData.IsCollidingColor;

        DrawTargetDetectorGizmos(detectorOrigin);
    }

    private void DrawTargetDetectorGizmos(Transform detectorOrigin)
    {
        Gizmos.DrawCube((Vector2) detectorOrigin.position + collissionData.DetectorOriginOffset, collissionData.TargetDetectorSize);
    }
}
