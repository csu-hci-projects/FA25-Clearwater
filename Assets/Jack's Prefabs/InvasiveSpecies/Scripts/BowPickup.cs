using UnityEngine;

public class BowPickup : MonoBehaviour
{
    public Transform bowHoldPoint;   
    public GameObject bowPrefab;     

    private bool playerInRange = false;
    private GameObject player;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            EquipBow();
        }
    }

    void Start()
    {
        if (bowHoldPoint == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                bowHoldPoint = player.transform.Find("BowHoldPoint");
            }
        }
    }

    void EquipBow()
    {
        GameObject bow = Instantiate(
            bowPrefab,
            bowHoldPoint.position,
            bowHoldPoint.rotation
        );

        bow.transform.SetParent(bowHoldPoint);

       
        BowController controller = bow.GetComponent<BowController>();
        if (controller != null)
            controller.enabled = true;

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }
}
