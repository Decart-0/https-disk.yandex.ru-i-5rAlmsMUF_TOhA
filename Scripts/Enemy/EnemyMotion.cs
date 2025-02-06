using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DetectorPlayer))]
public class EnemyMotion : MonoBehaviour
{
    private const float AngleLeft = 180f;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private float _waitTime;
    [SerializeField] private Transform _targetPoint;

    private DetectorPlayer _detectorPlayer;
    private Transform[] _targetPoints;
    private int _currentPointIndex;
    private bool _isWaiting;
    private bool _isVisiblePlayer;

    private void Awake()
    {
        _detectorPlayer = GetComponent<DetectorPlayer>();
        _targetPoints = new Transform[_targetPoint.childCount];
        _isWaiting = false;
        _isVisiblePlayer = false;

        for (int i = 0; i < _targetPoints.Length; i++)
        {
            _targetPoints[i] = _targetPoint.GetChild(i);
        }
    }

    private void OnEnable()
    {
        _detectorPlayer.SawPlayer += UpdateStatusVisible;
    }

    private void Start()
    {
        if (_targetPoints.Length > 0)
            transform.position = _targetPoints[_currentPointIndex].position;
    }

    private void Update()
    {
        if (_targetPoints.Length > 0)
            Move();
    }

    private void OnDisable()
    {
        _detectorPlayer.SawPlayer -= UpdateStatusVisible;
    }

    private void UpdateStatusVisible()
    {
        _isVisiblePlayer = _detectorPlayer.IsPlayerVisible;
    }

    private void Move()
    {
        Transform targetPoint = GetNextPoint();
        Vector2 direction = (targetPoint.position - transform.position).normalized;
        float targetX = Mathf.MoveTowards(transform.position.x, targetPoint.position.x, _speed * Time.deltaTime);
        transform.position = new Vector2(targetX, transform.position.y);

        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.1f && !_isWaiting)
        {
            StartCoroutine(WaitAtPoint());
        }

        Rotate(direction);
    }

    private void Rotate(Vector2 direction)
    {
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, AngleLeft, 0);
        }
    }

    private Transform GetNextPoint()
    {
        if (_isVisiblePlayer)
        {
            return _detectorPlayer.HealthPlayer.transform;
        }
        else
        {
            return _targetPoints[_currentPointIndex];
        }
    }

    private IEnumerator WaitAtPoint()
    {
        _isWaiting = true;

        yield return new WaitForSeconds(_waitTime);

        _currentPointIndex = (_currentPointIndex + 1) % _targetPoints.Length;
        _isWaiting = false;
    }
}