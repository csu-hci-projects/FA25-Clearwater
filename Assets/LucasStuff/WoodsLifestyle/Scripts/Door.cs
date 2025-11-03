using UnityEngine;

public class Door : MonoBehaviour
{
    private bool playerDetection = false;
    private bool doorOpen = false;

    void Update()
    {
        if(playerDetection && Input.GetKeyDown(KeyCode.E))
        {
            // underlying door model positioning is fried, (temporary?) workaround
            if (!doorOpen)
            {
                gameObject.transform.localPosition = new Vector3(1.232f, -0.415f, -1.072f); // cursed
                gameObject.transform.Rotate(0f, 0f, 90f);
            }
            else
            {
                gameObject.transform.localPosition = new Vector3(0.7680206f, 0.0839988f, -1.072f); // cursed
                gameObject.transform.Rotate(0f, 0f, -90f);
            }

            doorOpen = !doorOpen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) playerDetection = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerDetection = false;
    }
}
