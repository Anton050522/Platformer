using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private GameObject _curentHitObject;
    [SerializeField] private float _circleRadius;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    private Vector2 _origin;
    private Vector2 _direction;
    private EnemyController _enemyController;

    private float _currentHitDistance;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        _origin = transform.position;

        if (_enemyController.IsFacingRight)
        {
            _direction = Vector2.right;
        }
        else
        {
            _direction = Vector2.left;
        }
        RaycastHit2D hit = Physics2D.CircleCast(_origin, _circleRadius, _direction, _maxDistance, _layerMask);
        //Создай луч в форме окружности от точки ориджин с радиусом в направлении дирекшн на растоянии максдистанс и луч обращал внимание только на лайер маск

        if (hit)
        {
            _curentHitObject = hit.transform.gameObject;
            _currentHitDistance = hit.distance;

            if (_curentHitObject.CompareTag("Player"))
            {
                _enemyController.StartChanginPlayer();
            }
        }
        else
        {
            _curentHitObject = null;
            _currentHitDistance = _maxDistance;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_origin, _origin + _direction * _currentHitDistance);
        Gizmos.DrawWireSphere(_origin + _direction * _currentHitDistance, _circleRadius);

    }

}
