using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NativeFishController : MonoBehaviour
{
    public float swimSpeed = 3f;
    public float turnSpeed = 2f;
    public float directionChangeInterval = 2f;

    public float minY = 2f;
    public float maxY = 8f;

    [Header("Death")]
    public string invasiveTag = "Invasive";

    private Vector3 targetDirection;
    private Rigidbody rb;
    private bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        PickNewDirection();
        InvokeRepeating(nameof(PickNewDirection), 0f, directionChangeInterval);
    }

    void FixedUpdate()
    {
        if (isDead) return;

        Quaternion targetRot = Quaternion.LookRotation(targetDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, Time.fixedDeltaTime * turnSpeed));

        rb.linearVelocity = transform.forward * swimSpeed;

        ClampY();
    }

    void PickNewDirection()
    {
        targetDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.3f, 0.3f),
            Random.Range(-1f, 1f)
        ).normalized;
    }

    void ClampY()
    {
        Vector3 pos = rb.position;

        if (pos.y < minY)
        {
            pos.y = minY;
            targetDirection.y = Mathf.Abs(targetDirection.y);
        }

        if (pos.y > maxY)
        {
            pos.y = maxY;
            targetDirection.y = -Mathf.Abs(targetDirection.y);
        }

        rb.position = pos;
        targetDirection = targetDirection.normalized;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag(invasiveTag))
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector3.zero;
        Destroy(gameObject);
    }
}