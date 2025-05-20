using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum State { Idle, Running }

    [Header("Setting")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float viewAngle = 180f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleLayer;

    private State state;
    private Transform targetRunner;

    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                SearchForTarget();
                break;
            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        foreach (var collider in detectedColliders)
        {
            if (collider.TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                {
                    continue;
                }

                Vector3 directionToTarget = (runner.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, directionToTarget);

                if (angle <= viewAngle / 2)
                {
                    if (HasLineOfSightTo(runner.transform))
                    {
                        runner.SetTarget();
                        targetRunner = runner.transform;
                        StartRunningTowardsTarget();
                        return;
                    }
                }
            }
        }
    }

    private bool HasLineOfSightTo(Transform target)
    {
        Vector3 directionToTarget = target.position - transform.position;
        float distanceToTarget = directionToTarget.magnitude;

        Ray ray = new Ray(transform.position, directionToTarget.normalized);
        RaycastHit hit;

        Debug.DrawRay(transform.position, directionToTarget.normalized * searchRadius, Color.red, 0.1f);

        if (Physics.Raycast(ray, out hit, distanceToTarget, obstacleLayer))
        {
            return false;
        }

        return true;
    }

    private void StartRunningTowardsTarget()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void RunTowardsTarget()
    {
        if (targetRunner == null)
        {
            state = State.Idle;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, moveSpeed * Time.deltaTime);

        // Look at target while moving
        Vector3 direction = (targetRunner.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

        Vector3 viewAngleA = DirectionFromAngle(-viewAngle / 2);
        Vector3 viewAngleB = DirectionFromAngle(viewAngle / 2);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * searchRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * searchRadius);
    }

    private Vector3 DirectionFromAngle(float angleInDegrees)
    {
        angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}