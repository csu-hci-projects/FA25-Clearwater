using UnityEngine;

public class BowController : MonoBehaviour
{
    private Animator bowAnimator;
    public string arrowItemName = "Arrow";
    private bool isDrawing = false;

    public GameObject arrowPrefab;
    public Transform spawnPosition;
    public float shootingForce = 100f;

    private void Start()
    {
        bowAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleBowDrawing();
    }

    private void HandleBowDrawing()
    {
        if (Input.GetMouseButtonDown(1))
            StartDraw();

        if (Input.GetMouseButtonUp(1) && isDrawing)
            CancelDraw();

        if (Input.GetKeyDown(KeyCode.E) && isDrawing)
            ReleaseArrow();
    }

    private void StartDraw()
    {
        isDrawing = true;
        bowAnimator.SetBool("IsDrawing", true);
    }

    private void CancelDraw()
    {
        isDrawing = false;
        bowAnimator.SetBool("IsDrawing", false);
    }

    private void ReleaseArrow()
    {
        if (!isDrawing) return;

        isDrawing = false;
        bowAnimator.SetBool("IsDrawing", false);

        ShootArrow();
    }

    private void ShootArrow()
{
    if (arrowPrefab == null || spawnPosition == null)
    {
        Debug.LogError("BowController missing Arrow Prefab or Spawn Position!");
        return;
    }

    Vector3 shootingDirection = CalculateDirection().normalized;

    Vector3 spawnPos = spawnPosition.position + shootingDirection * 0.2f;

    GameObject arrow = Instantiate(
        arrowPrefab,
        spawnPos,
        Quaternion.LookRotation(shootingDirection)
    );

    Rigidbody rb = arrow.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.linearVelocity = -shootingDirection * shootingForce;  
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    Collider arrowCol = arrow.GetComponent<Collider>();
    Collider playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
    if (arrowCol != null && playerCol != null)
    {
        Physics.IgnoreCollision(arrowCol, playerCol);
    }
}

    public Vector3 CalculateDirection()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);

        return targetPoint - spawnPosition.position;
    }
}