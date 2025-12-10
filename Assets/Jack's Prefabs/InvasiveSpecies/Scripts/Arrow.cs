using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    public int damage = 1;
    private bool isStuck = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 6f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isStuck) return;

        isStuck = true;

        // Stop arrow motion
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // IF we hit the SeaMonster
        SeaMonsterHealth monster = collision.transform.GetComponent<SeaMonsterHealth>();

        if (monster != null)
        {
            monster.TakeDamage(damage);
            Destroy(gameObject); // arrow disappears on hit
            return;
        }

        // Otherwise stick into whatever it hit
        transform.SetParent(collision.transform);
    }
}


/*using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private bool isStuck = false;

    [Header("Homing Help")]
    public float homingStrength = 2.5f;     
    public float maxHomingDistance = 25f;   

    private Transform homingTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, 5f);

        FindHomingTarget();
    }

    private void FixedUpdate()
    {
        if (rb == null || isStuck || homingTarget == null)
            return;

        float dist = Vector3.Distance(transform.position, homingTarget.position);
        if (dist > maxHomingDistance)
            return;

        Vector3 desiredDir = (homingTarget.position - transform.position).normalized;
        Vector3 newVelocity = Vector3.Lerp(
            rb.linearVelocity.normalized,
            desiredDir,
            homingStrength * Time.fixedDeltaTime
        );

        rb.linearVelocity = newVelocity * rb.linearVelocity.magnitude;

        transform.forward = rb.linearVelocity.normalized;
    }

    private void FindHomingTarget()
    {
        EvasiveFishController[] monsters =
            GameObject.FindObjectsOfType<EvasiveFishController>();

        float closestDist = Mathf.Infinity;

        foreach (EvasiveFishController m in monsters)
        {
            float d = Vector3.Distance(transform.position, m.transform.position);
            if (d < closestDist)
            {
                closestDist = d;
                homingTarget = m.transform;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
{
    TryKillMonster(collision.transform);
}

private void OnTriggerEnter(Collider other)
{
    TryKillMonster(other.transform);
}

void TryKillMonster(Transform hit)
{
    Debug.Log("Arrow hit: " + hit.name);

    
    EvasiveFishController monster =
        hit.GetComponentInParent<EvasiveFishController>();

    if (monster != null)
    {
        Debug.Log("Monster KILLED!");
        Destroy(monster.gameObject);
        Destroy(gameObject);
    }
}*/

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (isStuck) return;
    //     isStuck = true;

    //     if (rb != null)
    //     {
    //         rb.linearVelocity = Vector3.zero;
    //         rb.angularVelocity = Vector3.zero;
    //         rb.isKinematic = true;
    //     }

    //     EvasiveFishController monster =
    //         collision.transform.GetComponent<EvasiveFishController>();

    //     if (monster != null)
    //         Destroy(monster.gameObject);   

    //     Destroy(gameObject);
    // }
//}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private int arrowDamage = 25;
  
    private bool isStuck = false; // Flag to track if the arrow is stuck
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, 5f); // Destroy the arrow after 5 seconds if it doesn't hit anything

        Collider arrowCollider = GetComponent<Collider>();

       
    }

    // This method is called when the arrow hits a collider
    private void OnCollisionEnter(Collision collision)
    {
        if (!isStuck && !collision.transform.CompareTag("Player"))
        {
            isStuck = true;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        EvasiveFishController monster = collision.transform.GetComponent<EvasiveFishController>();
        if (monster != null)
        {
            monster.Die();
            Destroy(gameObject); // Optional: destroy arrow on hit
        }
    }

 
}*/