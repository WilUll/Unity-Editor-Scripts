using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)] public int viewAngle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [Header("Debug Info")]
    public Color defaultGizmoColor = Color.cyan;


    //Makes it so radius cant be below 0
    private void OnValidate()
    {
        radius = Mathf.Clamp(radius, 0f, 100f);
    }

    private void Update()
    {
        LookForTarget();
    }

    public void LookForTarget()
    {
        Collider[] targetInRange = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (targetInRange.Length != 0)
        {
            foreach (var target in targetInRange)
            {
                Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < (viewAngle / 2))
                {
                    if (!Physics.Raycast(transform.position, directionToTarget, radius, obstacleMask))
                    {
                        Debug.DrawLine(transform.position, target.transform.position, new Color(1,0,0));
                    }
                }
            }
        }
    }
}
