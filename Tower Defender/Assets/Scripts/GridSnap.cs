using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnap : MonoBehaviour
{

    [SerializeField]    private float maxSnapHeight = 100;
    [SerializeField]    private Vector3 gridSize = new Vector3(1, 1, 1);

    private Renderer m_rend;
    private Vector3 m_planeExtensions;

    private Snappable[] m_snappablesArray;
    private List<Snappable> m_snappables;

    void Awake()
    {
    
        m_snappables = new List<Snappable>();

        m_rend = GetComponent<Renderer>();
        m_planeExtensions = m_rend.bounds.extents;

        m_snappablesArray = FindObjectsOfType<Snappable>();

        foreach(Snappable snappable in m_snappablesArray)
        {
            m_snappables.Add(snappable);
        }

    }
    void LateUpdate()
    {
        
        UpdateSnappablesPosition();
        
    }

    private void UpdateSnappablesPosition()
    {
        
        foreach(Snappable snapabble in m_snappables)
        {

            if(CheckIfInsideMe(snapabble))
            {
                SnapToGrid(snapabble);
            }

        }

    }

    private void SnapToGrid(Snappable snappable)
    {

        Vector3 snappedPos = GetNearestGridPoint(snappable.snapTarget.position);
        
        snappable.SetPosition(snappedPos);

    }

    private Vector3 GetNearestGridPoint(Vector3 point)
    {

        Vector3 snappedPos = Vector3.zero;

        snappedPos.x = Mathf.Round(point.x / gridSize.x) * gridSize.x;
        snappedPos.y = Mathf.Round(point.y / gridSize.y) * gridSize.y;
        snappedPos.z = Mathf.Round(point.z / gridSize.z) * gridSize.z;

        return snappedPos;

    }

    private bool CheckIfInsideMe(Transform otherTransform)
    {

        // Check if the other object is inside each axis
        if(otherTransform.position.x < transform.position.x - m_planeExtensions.x || otherTransform.position.x > transform.position.x + m_planeExtensions.x)    return false;
        if(otherTransform.position.y < transform.position.y - m_planeExtensions.y - maxSnapHeight || otherTransform.position.y > transform.position.y + m_planeExtensions.y + maxSnapHeight)    return false;
        if(otherTransform.position.x < transform.position.z - m_planeExtensions.z || otherTransform.position.z > transform.position.z + m_planeExtensions.z)    return false;

        return true;

    }

    private bool CheckIfInsideMe(Snappable snappable)
    {

        // Check if the other object is inside each axis
        if(snappable.snapTarget.position.x < transform.position.x - m_planeExtensions.x || snappable.snapTarget.position.x > transform.position.x + m_planeExtensions.x)    return false;
        if(snappable.snapTarget.position.y < transform.position.y - m_planeExtensions.y - maxSnapHeight || snappable.snapTarget.position.y > transform.position.y + m_planeExtensions.y + maxSnapHeight)    return false;
        if(snappable.snapTarget.position.x < transform.position.z - m_planeExtensions.z || snappable.snapTarget.position.z > transform.position.z + m_planeExtensions.z)    return false;

        return true;

    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;

        for(float x = transform.position.x - m_planeExtensions.x; x <= transform.position.x + m_planeExtensions.x; x += gridSize.x)
        {
            for(float z = transform.position.y - m_planeExtensions.z; z <= transform.position.z + m_planeExtensions.z; z += gridSize.z)
            {
                var snapPoint = GetNearestGridPoint(new Vector3(x, 0, z));
                Gizmos.DrawSphere(snapPoint, 0.1f);
            }
        }

    }


}
