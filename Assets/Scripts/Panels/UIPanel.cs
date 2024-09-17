using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;

    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
}
