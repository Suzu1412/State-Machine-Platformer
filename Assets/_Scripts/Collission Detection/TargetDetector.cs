using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private bool targetDetected;
    [SerializeField] private float detectionDelay = 0.3f;
    private Collider2D agentCollider;
    private CollissionSensesDataSO collissionData;
    private GameObject target;
    private Transform detectorOrigin;
    private IEnumerator detectionCoroutine;

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
    public float DistanceToClosestTarget => (target.transform.position - detectorOrigin.position).sqrMagnitude;

    public void SetCollissionData(CollissionSensesDataSO data)
    {
        collissionData = data;
    }

    private void Awake()
    {
        detectorOrigin = transform;
        detectionCoroutine = DetectionCoroutine();
    }


    private IEnumerator DetectionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(detectionDelay);

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
    }

    private void OnBecameVisible()
    {
        StartCoroutine(detectionCoroutine);
    }

    private void OnBecameInvisible()
    {
        StopCoroutine(detectionCoroutine);
    }

    private void OnDrawGizmos()
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
