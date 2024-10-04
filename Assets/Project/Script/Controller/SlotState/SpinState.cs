using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;
using static UnityEditor.Progress;

[State("SpinState")]

public class SpinState : FSMState
{
    [Enter]
    private void Enter()
    {
        Settings.Invoke("EnableStopChest");
    }

    [Bind("StopSpining")]
    private void StopSpining() 
    {
        Parent.Change("StopSpinState");
    }
   
    [Exit]
    private void Exit()
    {
        Settings.Invoke("RandomItem");
        Settings.Invoke("StopViewSpining", Model.Get<int>("RandomSelectItem"));
        Settings.Invoke("StopSpinChest");
    }

}
