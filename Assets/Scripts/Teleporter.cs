using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportTarget; // Assign your destination empty GameObject here

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player (e.g., by tag)
        if (other.CompareTag("Player")) 
        {
            // Teleport the player to the target's position and rotation
            other.transform.position = teleportTarget.position;
            other.transform.rotation = teleportTarget.rotation;
        }
    }
}