using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;


[State("StopSpinState")]

public class StopSpinState: FSMState
{
    [Enter]
    private void Enter()
    {
        Exit();
    }

    private void Exit()
    {
        Parent.Change("IdleState");
    }
}
