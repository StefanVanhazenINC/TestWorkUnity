using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;



[State("IdleState")]

public class IdleState : FSMState
{
 

    [Bind("StartSpining")]
    private void CheckInput()
    {
        Parent.Change("StartSpinState");
    }
   
}
