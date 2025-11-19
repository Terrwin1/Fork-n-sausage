using UnityEngine;

public class InputHandlerTouch : InputHandlerBase, ITickable
{
    public void Tick()
    {
        MainClickCheckStart();
        MainClickCheckFinish();
    }

    protected override void MainClickCheckStart()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                RaiseMainClickStart(touch.position);
            }
        }
    }

    protected override void MainClickCheckFinish()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                RaiseMainClickFinish(touch.position);
            }
        }
    }
}
