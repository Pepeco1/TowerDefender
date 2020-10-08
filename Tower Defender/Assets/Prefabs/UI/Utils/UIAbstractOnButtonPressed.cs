using UnityEngine;
using UnityEngine.UI;

public abstract class UIAbstractOnButtonPressed : MonoBehaviour
{

    private Button m_Button;

    void Awake()
    {
        if (m_Button == null)
            m_Button = GetComponent<Button>();
    }


    private void Start()
    {
        m_Button.onClick.RemoveAllListeners();
        m_Button.onClick.AddListener(() => { OnButtonPressed(); });
    }

    private void OnDisable()
    {
        m_Button.onClick.RemoveAllListeners();
    }

    protected abstract void OnButtonPressed();
}
