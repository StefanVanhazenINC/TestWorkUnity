using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using System;
using System.Reflection;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WheelView : MonoBehaviourExtBind
{
    [SerializeField] private WheelElemetView[] _elementInWheel;
    [SerializeField] private Transform _center;
    [SerializeField] private float _speedWheel = 5;
    [SerializeField] private float _timeToStop = 5;
    [SerializeField] private float _minYPos;
    [SerializeField] private float _startYPos;

    private bool _stopped;
    private WheelElemetView _needWheelElement;
    private int _needIndex = 0;
    private bool _nonStop;
    float[] StartDist = new float[3];
    [OnStart]
    private void SetWheel() 
    {
        for (int i = 0; i < _elementInWheel.Length; i++)
        {
            _elementInWheel[i].SetParamers(_minYPos, _startYPos);
            _elementInWheel[i].ObjectInTop.AddListener(CheckElement);
        }

        Vector2 start = _elementInWheel[_needIndex].transform.localPosition;

        StartDist = new float[3];
        for (int i = 0; i < _elementInWheel.Length; i++)
        {
            StartDist[i] = i * 200;
        }
        for (int i = 0; i < _elementInWheel.Length; i++)
        {

            int temp = CalculateDistance(0, i, out bool above);
            if (above)
            {
                _elementInWheel[i].transform.localPosition = (_elementInWheel[_needIndex].transform.localPosition + (transform.up * (StartDist[temp])));
            }
            else if (!above)
            {
                _elementInWheel[i].transform.localPosition = (_elementInWheel[_needIndex].transform.localPosition + (transform.up * -(StartDist[temp])));
            }
            
        }
    }


    [Bind("StopViewSpining")]
    public void StopWheel(int value)
    {
        _stopped = true;
        _needIndex = value;
        _needWheelElement = _elementInWheel[value];
    }

    [Bind("StartViewSpining")]
    public void StartWheel()
    {
        _needWheelElement = null;
        _stopped = false;
        StartRollAndLoopAnimation();
    }
    public void StartRollAndLoopAnimation()
    {
        _nonStop = true;
        
        Path = new CPath()
          .EasingLinear(3, 0, 1, (f) =>
          {
              for (int i = 0; i < _elementInWheel.Length; i++)
              {
                  _elementInWheel[i].transform.localPosition = Vector2.MoveTowards(_elementInWheel[i].transform.localPosition, _elementInWheel[i].transform.localPosition + transform.up*(_speedWheel * (f)), (_speedWheel * (f)) * Time.deltaTime); ;
              }

          })
          .Add((context) =>
          {
              for (int i = 0; i < _elementInWheel.Length; i++)
              {
                  _elementInWheel[i].transform.localPosition = Vector2.MoveTowards(_elementInWheel[i].transform.localPosition, _elementInWheel[i].transform.localPosition + transform.up * (_speedWheel ), (_speedWheel * Time.deltaTime));
              }
              if (_nonStop == false)
              {
                  return Status.OK;
              }
              return Status.Continue;
          });

    }
    public void StopAnimation() 
    {
        _nonStop = false;
        Path.StopPath();
        Path = new CPath();
        Path.EasingLinear(_timeToStop ,_speedWheel, 0, (f) =>
        {
            for (int i = 0; i < _elementInWheel.Length; i++)
            {

                if (i == _needIndex)
                {
                    _elementInWheel[_needIndex].transform.localPosition = Vector2.Lerp(_elementInWheel[_needIndex].transform.localPosition, _center.localPosition, _speedWheel/ f * Time.deltaTime);

                }
                else
                {
                    int temp = CalculateDistance(_needIndex, i, out bool above);
                    if (above)
                    {
                        _elementInWheel[i].transform.localPosition = (_elementInWheel[_needIndex].transform.localPosition + (transform.up * (StartDist[temp])));
                    }
                    else if (!above)
                    {
                        _elementInWheel[i].transform.localPosition = (_elementInWheel[_needIndex].transform.localPosition + (transform.up * -(StartDist[temp])));
                    }
                }
            }
        }).Action(() =>
        {
            Settings.Invoke("StopAnimChest");
        });
    }
    int CalculateDistance(int selectedIndex, int comparisonIndex, out bool above)
    {
        int temp = 0;
        int n = _elementInWheel.Length;
        WheelElemetView selectedElement = _elementInWheel[selectedIndex];
        WheelElemetView comparisonElement = _elementInWheel[comparisonIndex];

        // Проверка, находится ли элемент выше или ниже по оси Y
        bool isAbove = comparisonElement.transform.localPosition.y > selectedElement.transform.localPosition.y;
        bool isBelow = comparisonElement.transform.localPosition.y < selectedElement.transform.localPosition.y;

        if (comparisonElement.transform.localPosition.y > _minYPos)
        {
            isAbove = false;
            isBelow = true;
        }

        // Расстояние по индексам (вверх и вниз по кольцевому массиву)
        int distanceUp = (comparisonIndex >= selectedIndex)
            ? comparisonIndex - selectedIndex
            : n - selectedIndex + comparisonIndex;
        int distanceDown = (selectedIndex >= comparisonIndex)
            ? selectedIndex - comparisonIndex
            : selectedIndex + n - comparisonIndex;
        // Выводим расстояние с учетом положения элемента
        if (isAbove)
        {
            temp = distanceUp;
        }
        else if (isBelow)
        {
            temp = distanceDown;
        }
        above = isAbove;
        return temp;
    }
    public void CheckElement(WheelElemetView view)
    {
       
        if (_stopped)
        {
            if (view == _needWheelElement) 
            {
                _stopped = false;
                Path.StopPath(); 
                StopAnimation();
            }
        }
    }
   
}
