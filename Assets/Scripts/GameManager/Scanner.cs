using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float scanRange;
    [SerializeField] private LayerMask targetLayer;
    private RaycastHit2D[] targets;
    [SerializeField] public Transform nearestTarget;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    public Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPosition = transform.position;
            Vector3 targetPosition = target.transform.position;
            float currentDiff = Vector3.Distance(myPosition, targetPosition);

            if (currentDiff < diff)
            {
                diff = currentDiff;
                result = target.transform;
            }
        }

        return result;
    }
}