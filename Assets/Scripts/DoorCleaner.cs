using UnityEngine;
using System.Collections;

public class DoorCleaner : MonoBehaviour
{
    public float delayBeforeCleanup = 0.5f; // Time to wait for maze generation to complete
    public float detectionRadius = 1f; // Radius to check around the door for "End" walls

    void Start()
    {
        // Wait for the maze generation to complete before cleaning up doors
        StartCoroutine(DelayedCleanup());
    }

    IEnumerator DelayedCleanup()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeCleanup);

        // Cleanup doors after the delay
        CleanupDeadEndDoors();
    }

    void CleanupDeadEndDoors()
    {
        // Find all instantiated doors in the scene with the tag "Door"
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject door in doors)
        {
            //Debug.Log($"Door {door.name}.");
            // Check if the door is near a dead-end wall with the name "End"
            if (door.name.StartsWith("door_wall") && IsNearDeadEnd(door.transform.position))
            {
                Destroy(door); // Remove the door if it's near a dead-end wall
                //Debug.Log($"Removed door {door.name} near a dead-end wall.");
            }
        }
    }

    private bool IsNearDeadEnd(Vector3 doorPosition)
    {
        // Find all colliders in a certain radius around the door
        Collider[] nearbyColliders = Physics.OverlapSphere(doorPosition, detectionRadius);

        foreach (Collider col in nearbyColliders)
        {
            //Debug.Log($"Door {col.name}.");
            // Check if the nearby collider has the name "End" (dead-end wall)
            if (col.gameObject.name.StartsWith("Cube"))
            {
                return true; // If there's an "End" wall nearby, it's a dead-end
            }
        }

        return false; // No "End" wall found nearby
    }
}
