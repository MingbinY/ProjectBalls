using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TurretSensor))]
public class TurretFOVEditor : Editor
{

    private void OnSceneGUI()
    {
        TurretSensor sensor = (TurretSensor)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(sensor.transform.position, Vector3.up, Vector3.forward, 360, sensor.radius);

        Vector3 viewAngle01 = DirectionFromAngle(sensor.transform.eulerAngles.y, -sensor.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(sensor.transform.eulerAngles.y, sensor.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(sensor.transform.position, sensor.transform.position + viewAngle01 * sensor.radius);
        Handles.DrawLine(sensor.transform.position, sensor.transform.position + viewAngle02 * sensor.radius);

        if (sensor.targetInSight)
        {
            Handles.color = Color.green;
            Handles.DrawLine(sensor.transform.position, sensor.targetRef.transform.position);
        }

    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDeg)
    {
        angleInDeg += eulerY;

        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }
}
