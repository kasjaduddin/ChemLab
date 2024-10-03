using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerHand
{
    Left,
    Right,
}

public class DGN_ChangeHand : MonoBehaviour
{

    [SerializeField] GameObject[] _handObjects;
    public int _currentHandIndex = 0;
    [SerializeField] ControllerHand _controller;
    UnityEngine.XR.InputDevice _device;
    bool _gotInput;
    void Start()
    {
        var controllers = new List<UnityEngine.XR.InputDevice>();

        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        if (_controller == ControllerHand.Right)
        {
            desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        }
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);

        if (controllers.Count > 0)
        {
            _device = controllers[0];
            print("controller found:" + _device.name);
        }
        else
        {
            Debug.Log("<color=red>Error: </color> No controller found");
        }
        HandChange();
    }

    void Update()
    {
        Vector2 triggerValue;
        if (_device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out triggerValue))
        {
            if (triggerValue.x > 0.7f)
            {
                if (!_gotInput)
                {
                    NextHand();
                    _gotInput = true;
                }
            }
            else if (triggerValue.x < -0.7f)
            {
                if (!_gotInput)
                {
                    PreviousHand();
                    _gotInput = true;
                }
            }
            else
            {
                _gotInput = false;
            }
        }
        var controllers = new List<UnityEngine.XR.InputDevice>();
        if (_device == null)
        {
            ControllerCheck();
        }
    }

    void ControllerCheck()
    {
        var controllers = new List<UnityEngine.XR.InputDevice>();

        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        if (_controller == ControllerHand.Right)
        {
            desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        }
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);

        if (controllers.Count > 0)
        {
            _device = controllers[0];
            print("controller found:" + _device.name);
        }

    }

    public void NextHand()
    {
        if (_handObjects.Length == 0) return;
        print("next hand on" + _device.name);
        _currentHandIndex += 1;
        if (_currentHandIndex >= _handObjects.Length)
        {
            _currentHandIndex = 0;
        }
        HandChange();
    }

    public void PreviousHand()
    {
        if (_handObjects.Length == 0) return;
        print("previous hand on" + _device.name);
        _currentHandIndex -= 1;
        if (_currentHandIndex < 0)
        {
            _currentHandIndex = _handObjects.Length - 1;
        }
        HandChange();
    }
    void HandChange()
    {
        if (_handObjects.Length == 0) return;
        for (int i = 0; i < _handObjects.Length; i++)
        {
            _handObjects[i].SetActive(i == _currentHandIndex);
        }
    }
}
