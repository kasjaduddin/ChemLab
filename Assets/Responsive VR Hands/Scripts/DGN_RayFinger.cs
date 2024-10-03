using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class DGN_RayFinger : MonoBehaviour
{

    [SerializeField] float _fingerTipDistance;
    [SerializeField] float _fingerMoveSensivity = 0.015f;
    [SerializeField] float _raySphereRadius = 0.015f;
    [SerializeField] int _hitAmount;
    [SerializeField] Color _gizmoColor = Color.red;
    [SerializeField] Transform _actualFingerTip;
    [SerializeField] Transform _targetFingerTip;
    [SerializeField] Transform _rigFingerTip;
    [SerializeField] LayerMask _obstacleLayers;
    [SerializeField] AnimationCurve _movementCurve;
    [SerializeField] TwoBoneIKConstraint _boneIKConstraint;
    Vector3 _defaultLocPos;
    public bool _hasObstacle = false;
    Vector3 _touchPoint;
    float _forceAmount;
    float _thisLocalPosAmount;
    Transform m_Transform;
    Collider _col;
    RaycastHit[] _hit;

    void Start()
    {
        m_Transform = transform;
        _defaultLocPos = m_Transform.localPosition;
        if (!_col && GetComponent<Collider>())
        {
            _col = GetComponent<Collider>();
        }
        _hit = new RaycastHit[15];
    }

    void FixedUpdate()
    {
        DetectObstacleThick();
    }

    private void LateUpdate()
    {
        PoseFinger();
    }

    void DetectObstacleThick()
    {
        if (!_actualFingerTip) return;
        float length = Vector3.Distance(m_Transform.position, _actualFingerTip.position);
        Vector3 direction = (_actualFingerTip.position - m_Transform.position).normalized;
        _hitAmount = Physics.SphereCastNonAlloc(m_Transform.position, _raySphereRadius, direction, _hit, length, _obstacleLayers);
        _fingerTipDistance = Vector3.Distance(_touchPoint, _actualFingerTip.position);
        if (_hitAmount > 0)
        {
            RaycastHit closestHit = _hit[0];
            float closestHitDistance = Vector3.Distance(_hit[0].point, _actualFingerTip.position);
            for (int i = 0; i < _hit.Length; i++)
            {
                var tempDistance = Vector3.Distance(_hit[i].point, _actualFingerTip.position);
                if (tempDistance < closestHitDistance)
                {
                    closestHit = _hit[i];
                }

            }
            var touchDistance = Vector3.Distance(_touchPoint, closestHit.point);

            if (touchDistance > _fingerMoveSensivity && closestHit.point != Vector3.zero)
            {
                _touchPoint = closestHit.point;
            }
            if (!_hasObstacle)
            {
                _hasObstacle = true;
            }
            _forceAmount = (length - closestHit.distance) / length;

        }
        else
        {
            _hasObstacle = false;
            _forceAmount = 0.0f;

        }
    }



    void PoseFinger()
    {
        float myTime = Time.deltaTime * 6f;
        Vector3 targetPos = _actualFingerTip.position;
        var easing = _movementCurve.Evaluate(myTime);
        float ikWeight = 0.0f;
        if (_hasObstacle && _targetFingerTip)
        {
            ikWeight = 1.0f;
            targetPos = _touchPoint;
            _hit = new RaycastHit[15];
        }

        if (!_hasObstacle)
        {
            ikWeight = 0.0f;
            targetPos = _actualFingerTip.position;
            _fingerTipDistance = 0.0f;
            _hit = new RaycastHit[15];
        }
        _targetFingerTip.position = Vector3.LerpUnclamped(_targetFingerTip.position, targetPos, easing);

        if (_col && !_col.enabled)
        {
            _col.enabled = true;
            m_Transform.localScale = Vector3.one;
        }
        if (_boneIKConstraint)
        {
            ikWeight = Mathf.Clamp01(ikWeight);
            _boneIKConstraint.weight = Mathf.Lerp(_boneIKConstraint.weight, ikWeight, easing);
        }
        _actualFingerTip.LookAt(m_Transform);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!_actualFingerTip) return;
        Gizmos.color = _gizmoColor;
        float length = Vector3.Distance(transform.position, _actualFingerTip.position);
        Vector3 direction = (_actualFingerTip.position - transform.position).normalized * length;
        Gizmos.DrawRay(transform.position, direction);
        if (_hasObstacle)
        {
            Gizmos.DrawWireSphere(_touchPoint, 0.01f);
            Gizmos.DrawWireSphere(_actualFingerTip.position, 0.01f);
        }
    }
#endif

}
