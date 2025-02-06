using System;
using UnityEngine;

public class Health : MonoBehaviour
{   
    [field: SerializeField] public float MaxValue { get; private set; }

    [field: SerializeField] public float Value { get; private set; }

    public event Action Changed;

    public void Restore(float amount)
    {
        Value += amount;

        if (Value > MaxValue)
        {
            Value = MaxValue; 
        }

        Changed?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        Value -= damage;       

        if (Value <= 0)
        {
            Value = 0;
        }

        Changed?.Invoke();
    }
}