using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DGN_RayPalm : MonoBehaviour
{

    [SerializeField] bool _hasObstacle;
    [Space]
    [SerializeField] float _rayLength = 1;
    [SerializeField] Color _gizmoColor = Color.red;
    [SerializeField] float _buriedZ = 0.2f;
    [Space]
    [SerializeField] Transform _handTransform;
    [SerializeField] Transform _palmTransform;
    [SerializeField] Transform _realPalmTransform;
    [SerializeField] LayerMask _obstacleLayers;
    Vector3 _defaultPalmToHandPosition;
    Vector3 _defaultHandLocalPosition;
    Vector3 _defaultPalmLocalPosition;
    Vector3 _obstaclePoint = Vector3.zero;
    Transform m_Transform;

    //Events
    public UnityEvent _palmTouchOn;
    public UnityEvent _palmBuried;
    public UnityEvent _palmTouchOff;

    void Start()
    {
        if (_handTransform && _palmTransform)
        {
            _defaultPalmToHandPosition = _palmTransform.InverseTransformPoint(_handTransform.position);
            _defaultHandLocalPosition = _handTransform.localPosition;
            _defaultPalmLocalPosition = _palmTransform.localPosition;
        }
        m_Transform = transform;
        _palmTouchOff.Invoke();
    }

    void LateUpdate()
    {
        DetectObstacles();
        SetHandPosition();
    }

    void DetectObstacles()
    {
        RaycastHit hit;

        if (Physics.Raycast(m_Transform.position, m_Transform.forward, out hit, _rayLength, _obstacleLayers))
        {
            _obstaclePoint = hit.point;
            if (!_hasObstacle)
            {
                _hasObstacle = true;
                _palmTouchOn.Invoke();
            }
        }
        else
        {
            if (_hasObstacle)
            {
                _palmTouchOff.Invoke();
                _hasObstacle = false;
            }
        }
    }

    void SetHandPosition()
    {
        float myTime = Time.deltaTime * 0.5f;
        if (_hasObstacle)
        {
            _palmTransform.position = _obstaclePoint;
            var targetHandPos = _obstaclePoint;
            _handTransform.position = Vector3.MoveTowards(_handTransform.position, targetHandPos, myTime);
            var buriedDistance = Vector3.Distance(_realPalmTransform.position, _palmTransform.position);
            if (buriedDistance > _buriedZ)
            {
                _palmBuried.Invoke();
            }
        }
        else
        {
            _handTransform.localPosition = Vector3.MoveTowards(_handTransform.localPosition, _defaultHandLocalPosition, myTime);
            _palmTransform.localPosition = _defaultPalmLocalPosition;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (!_handTransform || !_palmTransform) return;
        Gizmos.color = _gizmoColor;
        float length = _rayLength;
        Vector3 direction = transform.forward * length;
        Gizmos.DrawRay(transform.position, direction);
    }

}
