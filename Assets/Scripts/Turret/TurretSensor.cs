using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSensor : MonoBehaviour
{
    public float radius;

    [Range(0f, 360f)]
    public float angle;
    public GameObject targetRef;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool targetInSight;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    public void FieldOfViewCheck()
    {
        if (targetRef == null || !CheckLineOfSight(targetRef.transform))
        {
            // Check if current target is out of sight, update targetRef
            targetInSight = false;
            targetRef = null;
        }
            
        float distToTarget = float.MaxValue;
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                float distToObj = Vector3.Distance(col.transform.position, transform.position);
                if (distToObj < distToTarget && CheckLineOfSight(col.transform))
                {
                    // if this is closer and insight
                    targetInSight = true;
                    targetRef = col.gameObject;
                }
            }
        }
        if (targetRef == null)
        {
            targetInSight=false;
        }
    }

    bool CheckLineOfSight(Transform target)
    {
        if (Vector3.Distance(target.position, transform.position) > radius)
        {
            return false;
        }

        bool inSight = false;
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
        {
            float distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
            {
                inSight = true;
            }
        }

        return inSight;
    }
}
