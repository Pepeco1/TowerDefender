using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public Color hoverColor;

    private GameObject nodeTurret = null;
    private Renderer rend = null;
    private Color startColor;

    private BuildManager m_buildManager = null;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        startColor = rend.material.color;

        m_buildManager = BuildManager.Instance;
    }

    private void OnMouseDown()
    {
        
        if(nodeTurret != null)
        {
            Debug.Log("[Node] Cannot build here");
            return;
        }

        if (CurrencyManager.Instance.ChangeMoney(-10f))
        {
            nodeTurret = BuildManager.Instance.BuildTurret();
            nodeTurret.transform.position = this.transform.position;
        }
        else
        {
            Debug.LogWarning("[Node] TODO - Display not enough coins message");
        }

    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
