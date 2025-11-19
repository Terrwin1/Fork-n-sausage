using System;
using UnityEngine;

public abstract class InputHandlerBase
{
    public event Action<Vector3> OnMainClickStarted;
    public event Action<Vector3> OnMainClickFinished;

    protected void RaiseMainClickStart(Vector3 position) => OnMainClickStarted?.Invoke(position);
    protected void RaiseMainClickFinish(Vector3 position) => OnMainClickFinished?.Invoke(position);

    protected abstract void MainClickCheckStart();
    protected abstract void MainClickCheckFinish();
}
