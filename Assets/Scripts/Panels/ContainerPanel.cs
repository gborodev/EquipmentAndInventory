using UnityEngine;

public abstract class ContainerPanel : MonoBehaviour
{
    public ItemContainer Container { get; private set; }

    private void OnValidate()
    {
        Container = GetComponentInChildren<ItemContainer>(false);
    }

    public bool IsActive => gameObject.activeSelf;

    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
    public virtual void ChangeActivate(bool activate) => gameObject.SetActive(activate);
}
