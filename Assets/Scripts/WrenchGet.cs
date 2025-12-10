using UnityEngine;

public class WrenchGet : MonoBehaviour
{

    [SerializeField] GameMaster gameMaster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameMaster.PlayerObtainWrench();
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
