using AxGrid.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AxGrid.Path;
using UnityEngine.Events;
using System.IO;

public class WheelElemetView : MonoBehaviourExt
{
    private float _maxYPos;
    private float _startYPos;
    private UnityEvent<WheelElemetView> _objectInTop = new UnityEvent<WheelElemetView>();

    public UnityEvent<WheelElemetView> ObjectInTop { get => _objectInTop; set => _objectInTop = value; }
    public bool DisableClamp { get => _disableClamp; set => _disableClamp = value; }

    private bool _disableClamp;
    public void SetParamers(float minYPos, float startYPos) 
    {
        _maxYPos = minYPos;
        _startYPos = startYPos;
    }
   
    [OnUpdate]
    private void TeleportPosition()
    {
        if (!_disableClamp) 
        {
            if (transform.localPosition.y > _maxYPos-1)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, _startYPos, 0);
                _objectInTop?.Invoke(this);
            }
        }
    }
}
