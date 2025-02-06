using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _coin;

    public void AddCoins(int amount)
    {
        _coin += amount;
    }
}