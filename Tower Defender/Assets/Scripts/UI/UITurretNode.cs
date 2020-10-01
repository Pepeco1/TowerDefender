using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITurretNode : BaseUIPanel, IPointerDownHandler
{
    public int index = -1;
    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }

        set
        {
            _isSelected = value;
            ToogleSelectedColor();
        }
    }

    private bool _isSelected = false;

    private Image myImage = null;
    private UITurretsLayout turretsLayout = null;

    private void Awake()
    {
        myImage = GetComponent<Image>();
        turretsLayout = GetComponentInParent<UITurretsLayout>();
    }

    private void Start()
    {
        CheckForError();

        myImage.color = turretsLayout.turretNodeFlyWeight.unselectedColor;

    }

    private void CheckForError()
    {
        if (index == -1)
        {
            gameObject.SetActive(false);
            Debug.LogError("[UITurretNode] Not in layout list");
        }
    }

    private void ToogleSelectedColor()
    {
        if (_isSelected)
        {
            myImage.color = turretsLayout.turretNodeFlyWeight.selectedColor;
        }
        else
        {
            myImage.color = turretsLayout.turretNodeFlyWeight.unselectedColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        turretsLayout.SelectNode(this);
    }
}

