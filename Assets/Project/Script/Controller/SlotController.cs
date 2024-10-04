using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;
public class SlotController : MonoBehaviourExtBind
{
    [SerializeField] public int _itemDiaposon = 3;
    private int _selectItem = 0;
    [OnStart]
    private void StartThis()
    {
        Settings.Fsm = new FSM();
        Settings.Fsm.Add(new InitState());
        Settings.Fsm.Add(new IdleState());
        Settings.Fsm.Add(new StopSpinState());
        Settings.Fsm.Add(new SpinState());
        Settings.Fsm.Add(new StartSpinState());

        Settings.Fsm.Start("InitState");
    }
    [OnUpdate]
    private void UpdateThis()
    {
        Settings.Fsm.Update(Time.deltaTime);
    }
    [Bind("RandomItem")]
    public void RandomItem() 
    {
        _selectItem = Random.Range(0, _itemDiaposon);
        Model.Set("RandomSelectItem", _selectItem);

    }
    [Bind("OnStartSpinClick")]
    public void StartSpining() 
    {
        Settings.Fsm.Invoke("StartSpining");
    }
    [Bind("OnStopSpinClick")]
    public void StopSpining()
    {
        Settings.Fsm.Invoke("StopSpining");
    }
}
