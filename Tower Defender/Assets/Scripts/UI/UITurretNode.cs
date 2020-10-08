using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class UITurretNode : BaseUIPanel, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
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

    public Turret TurretPrefab { get => turretPrefab; set { } }


    private bool _isSelected = false;


    private Image myImage = null;
    private Button myButton = null;
    private UITurretsLayout turretsLayout = null;
    private BuildManager buildManager = null;
    private UIStatsHolder statsPanel = null;
    [SerializeField] Turret turretPrefab = null;

    private UnityAction onButtonClicked = null;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        myImage = GetComponent<Image>();
        turretsLayout = GetComponentInParent<UITurretsLayout>();
        buildManager = BuildManager.Instance;
        statsPanel = GetComponentInChildren<UIStatsHolder>(true);

        turretPrefab = Instantiate(turretPrefab, new Vector3(-100, -100, -100), Quaternion.identity);
        //turretPrefab.gameObject.SetActive(false);

        myButton.onClick.AddListener(onButtonClicked);
    }

    private void OnEnable()
    {
        onButtonClicked += SetTurretToBuildManager;
    }

    private void OnDisable()
    {
        onButtonClicked -= SetTurretToBuildManager;
    }


    private void Start()
    {
        CheckForError();

        myImage.color = turretsLayout.turretNodeFlyWeight.unselectedColor;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        turretsLayout.SelectNode(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        statsPanel.OpenBehavior();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        statsPanel.CloseBehavior();
    }

    private void CheckForError()
    {
        if (index == -1)
        {
            gameObject.SetActive(false);
            Debug.LogError("[UITurretNode] Not in layout list");
        }
    }
    private void SetTurretToBuildManager()
    {
        buildManager.TurretToBuild = turretPrefab.gameObject;
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

}

