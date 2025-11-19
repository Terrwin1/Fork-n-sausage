using UnityEngine;

public abstract class PlayerControllerBase : IInjectable
{
    public abstract void SetPlayer(Rigidbody rigidbody); 
    protected abstract void SaveStartControllerPoint(Vector3 position);
    protected abstract void SaveFinishControllerPoint(Vector3 posiiton);
    protected abstract void CalculatePlayerPath();
    protected abstract void MovePlayer();
    public abstract void Init();
}
