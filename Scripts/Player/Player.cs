using System;
using System.Collections;
using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(InputScheme))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldownAttack;
    [SerializeField] private float _attackRadius;
    [SerializeField] private Transform _attackPosition;

    private InputScheme _inputScheme;
    private Health _health;
    private bool _isCooldown;

    public event Action Attacking;

    public bool IsAttack { get; private set; }

    private void Awake()
    {
        _inputScheme = GetComponent<InputScheme>();
        _health = GetComponent<Health>();
        _isCooldown = false;
    }

    private void OnEnable()
    {
        _health.Changed += CheckHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_inputScheme.Attack) && _isCooldown == false) 
        {
            StartAttack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRadius);
    }

    private void OnDisable()
    {
        _health.Changed -= CheckHealth;
    }

    private void Attack() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRadius);

        foreach (Collider2D collider in colliders) 
        {
            if (collider.TryGetComponent(out Ghost ghost)) 
            {
                ghost.TakeDamage(_damage);
            }  
        }
    }

    private void StartAttack()
    {   
        _isCooldown = true;
        IsAttack = true;
        Attacking?.Invoke();
        Attack();
        StartCoroutine(WaitAttack());
    }

    private void CheckHealth()
    {
        if (_health.Value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(_cooldownAttack);
        
        _isCooldown = false;
        IsAttack = false;
        Attacking?.Invoke();  
    }
}