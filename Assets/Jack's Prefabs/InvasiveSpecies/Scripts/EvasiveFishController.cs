using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class EvasiveFishController : MonoBehaviour
{
    [Header("Movement")]
    public float swimSpeed = 6f;
    public float turnSpeed = 3f;
    public float directionChangeInterval = 2f;

    [Header("Depth Limits")]
    public float minY = 2f;
    public float maxY = 8f;

    [Header("Hunting")]
    public float detectionRadius = 25f;
    public string preyTag = "Fish";

    [Header("Eating Effects")]
    public ParticleSystem eatEffect;
    public float slowDuration = 1.5f;
    public float slowMultiplier = 0.4f;

    private Vector3 targetDirection;
    private Transform currentTarget;
    private float baseSpeed;
    private bool isSlowed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Rigidbody setup (same as normal fish)
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        baseSpeed = swimSpeed;

        PickNewDirection();
        InvokeRepeating(nameof(PickNewDirection), 0f, directionChangeInterval);
    }

    void FixedUpdate()
    {
        FindNearestFish();

        // If there's a target, chase it
        if (currentTarget != null)
        {
            targetDirection = (currentTarget.position - rb.position).normalized;
        }

        // Smooth rotation
        Quaternion targetRot = Quaternion.LookRotation(targetDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, Time.fixedDeltaTime * turnSpeed));

        // Forward motion
        rb.linearVelocity = transform.forward * swimSpeed;

        ClampY();
    }

    void PickNewDirection()
    {
        if (currentTarget != null) return;

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
        if (other.CompareTag(preyTag))
        {
            Destroy(other.gameObject);
            currentTarget = null;

            if (eatEffect != null)
                eatEffect.Play();

            if (!isSlowed)
                StartCoroutine(SlowAfterEating());
        }
    }

    IEnumerator SlowAfterEating()
    {
        isSlowed = true;
        swimSpeed = baseSpeed * slowMultiplier;
        yield return new WaitForSeconds(slowDuration);
        swimSpeed = baseSpeed;
        isSlowed = false;
    }

    void FindNearestFish()
    {
        GameObject[] fish = GameObject.FindGameObjectsWithTag(preyTag);

        float closestDist = detectionRadius;
        currentTarget = null;

        foreach (GameObject f in fish)
        {
            float dist = Vector3.Distance(rb.position, f.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                currentTarget = f.transform;
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}