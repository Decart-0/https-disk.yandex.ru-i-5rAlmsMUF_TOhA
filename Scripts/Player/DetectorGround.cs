using System;
using UnityEngine;

public class DetectorGround : MonoBehaviour
{
    private int _currentAmountGround = 0;

    public event Action GroundStatusChanged;

    public bool IsOnGround => _currentAmountGround > 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Ground>())
        {
            _currentAmountGround++;
            GroundStatusChanged?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Ground>())
        {
            _currentAmountGround--;
            GroundStatusChanged?.Invoke();
        }
    }
}