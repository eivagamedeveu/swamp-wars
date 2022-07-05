using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIScreen : MonoBehaviour
{
    protected CanvasGroup CanvasGroup;

    protected virtual void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
    }
}
