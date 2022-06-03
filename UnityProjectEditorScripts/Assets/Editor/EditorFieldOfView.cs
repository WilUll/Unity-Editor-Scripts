using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class EditorFieldOfView : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        //Gets the angle to the endpoints for the seen area
        Vector3 angleA = new Vector3(Mathf.Sin(Mathf.Deg2Rad * -fov.viewAngle / 2), 0, Mathf.Cos(Mathf.Deg2Rad * -fov.viewAngle / 2));
        Vector3 angleB = new Vector3(Mathf.Sin(Mathf.Deg2Rad * fov.viewAngle / 2), 0, Mathf.Cos(Mathf.Deg2Rad * fov.viewAngle / 2));

        // Makes everyting relative to the ai pos
        Handles.matrix = fov.transform.localToWorldMatrix;

        //Draws view angles
        Handles.color = Color.black;
        if (fov.viewAngle != 360 || fov.viewAngle != 0)
        {
            Handles.DrawLine(Vector3.zero, Vector3.zero + angleA * fov.radius);
            Handles.DrawLine(Vector3.zero, Vector3.zero + angleB * fov.radius);
        }

        //Draws "unseen" area
        Handles.DrawWireArc(Vector3.zero, Vector3.up, angleB, 360 - fov.viewAngle, fov.radius);

        //Draws "seen" area
        Handles.color = fov.defaultGizmoColor;

        Handles.DrawSolidArc(Vector3.zero, Vector3.up, angleA, fov.viewAngle, fov.radius);
    }
}
