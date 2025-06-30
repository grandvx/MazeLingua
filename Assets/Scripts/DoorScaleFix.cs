using UnityEngine;

public class DoorScaleFix : MonoBehaviour
{
    public Vector3 fixedScale; // Set the desired scale in the Inspector.

    void LateUpdate()
    {
        transform.localScale = fixedScale; // Enforce the scale every frame.
    }
}
