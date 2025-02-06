using System;
using UnityEngine;

public class DetectorPlayer : MonoBehaviour
{
    public event Action SawPlayer;

    public bool IsPlayerVisible { get; private set; }

    public Health HealthPlayer { get; private set; }

    private void Awake()
    {
        IsPlayerVisible = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {     
        if (collider.TryGetComponent(out Health healthPlayer))
        {
            HealthPlayer = healthPlayer;
            IsPlayerVisible = true;
            SawPlayer?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>())
        {
           IsPlayerVisible = false;
           SawPlayer?.Invoke();
        }
    }
}