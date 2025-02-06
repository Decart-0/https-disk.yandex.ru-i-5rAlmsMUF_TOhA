using UnityEngine;

public class AnimatorData : MonoBehaviour
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
        public static readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int AttackTrigger = Animator.StringToHash(nameof(AttackTrigger));
    }
}