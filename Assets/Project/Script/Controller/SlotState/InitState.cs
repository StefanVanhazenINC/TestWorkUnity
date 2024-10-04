using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;


[State("InitState")]
public class InitState : FSMState
{


    [One(0.1f)]
    private void GoToIdle() 
    {
        Settings.Invoke("InitUI");
        Parent.Change("IdleState");
    }
}
