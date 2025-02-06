using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GhostAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(AnimatorData.Params.AttackTrigger);
    }
}