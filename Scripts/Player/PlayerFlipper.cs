using UnityEngine;

public class PlayerFlipper : MonoBehaviour
{
    private const float AngleRight = 0f;
    private const float AngleLeft = 180f;

    [SerializeField] private float _rotationSpeed;

    public void Flip(float direction)
    {
        float targetAngle = direction > 0 ? AngleRight : AngleLeft;
        Quaternion targetRotation = Quaternion.Euler(new Vector2(0, targetAngle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed);
    }
}
