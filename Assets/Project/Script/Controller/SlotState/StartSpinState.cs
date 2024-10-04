using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State("StartSpinState")]
public class StartSpinState : FSMState
{
    [Enter]
    private void Enter()
    {
        Settings.Invoke("StartViewSpining");
        Settings.Invoke("OpenChest");
    }

    [One(3)]
    private void CheckInput()
    {
        Parent.Change("SpinState");

    }

  


}
