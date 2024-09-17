using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }
    public virtual void Awake()
    {
        if (Instance is null)
        {
            Instance = (T)this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
