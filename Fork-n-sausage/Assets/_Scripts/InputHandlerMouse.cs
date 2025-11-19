using UnityEngine;

public class InputHandlerMouse : InputHandlerBase, ITickable
{

    public void Tick()
    {
        MainClickCheckStart();
        MainClickCheckFinish();
    }

    protected override void MainClickCheckStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaiseMainClickStart(Input.mousePosition);
        }
    }

    protected override void MainClickCheckFinish()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaiseMainClickFinish(Input.mousePosition);
        }
    }
}
