using UnityEngine;

public class NativeFishController : MonoBehaviour
{
    public float swimSpeed = 3f;
    public float turnSpeed = 2f;
    public float directionChangeInterval = 2f;

    
    public float minY = 2f;   
    public float maxY = 8f;   

    private Vector3 targetDirection;

    void Start()
    {
        PickNewDirection();
        InvokeRepeating(nameof(PickNewDirection), 0f, directionChangeInterval);
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(targetDirection),
            Time.deltaTime * turnSpeed
        );

        transform.position += transform.forward * swimSpeed * Time.deltaTime;

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
        Vector3 pos = transform.position;

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

        transform.position = pos;
        targetDirection = targetDirection.normalized;
    }
}

