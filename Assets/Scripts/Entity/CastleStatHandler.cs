using UnityEngine;

public class CastleStatHandler : MonoBehaviour
{
    [Range(1,100)] [SerializeField] private int health = 10;

    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }
}