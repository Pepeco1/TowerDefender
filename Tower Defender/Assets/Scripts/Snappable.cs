using UnityEngine;

[ExecuteInEditMode]
public class Snappable : MonoBehaviour
{

    public Transform snapTarget;

    private Vector3 distanceToTarget;

    void Awake()
    {

        distanceToTarget = transform.position - snapTarget.position;
    
    }

    // Set new position considering the distance to the snapTarget
    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos + distanceToTarget;
    }

}
