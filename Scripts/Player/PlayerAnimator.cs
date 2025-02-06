using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DetectorGround))]
[RequireComponent(typeof(PlayerMotion))]
[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private DetectorGround _detectorGround;
    private PlayerMotion _playerMotion;
    private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _detectorGround = GetComponent<DetectorGround>();
        _playerMotion = GetComponent<PlayerMotion>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _detectorGround.GroundStatusChanged += Setup;
        _playerMotion.Moving += Setup;
        _player.Attacking += SetupAttack;
    }

    private void OnDisable()
    {
        _detectorGround.GroundStatusChanged -= Setup;
        _playerMotion.Moving -= Setup;
        _player.Attacking -= SetupAttack;
    }

    private void Setup()
    {
        _animator.SetFloat(AnimatorData.Params.Speed, Mathf.Abs(_playerMotion.Direction));
        _animator.SetBool(AnimatorData.Params.IsGrounded, _detectorGround.IsOnGround);
    }

    private void SetupAttack()
    {
        if (_player.IsAttack)
        {
            _animator.SetTrigger(AnimatorData.Params.Attack);
        }
    }
}