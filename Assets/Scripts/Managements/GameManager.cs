using System;

public class GameManager : Singleton<GameManager>
{
    public event Action<int> OnGoldChanged;

    private int gold = 999;
    public int Gold
    {
        get => gold;
        set
        {
            gold = value;

            OnGoldChanged?.Invoke(value);
        }
    }
}
