using UnityEngine;

public class PlayerControllerNormal : PlayerControllerBase, IInjectable
{
    [Inject] private InputHandlerBase _inputHandler;
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _finishPosition = Vector3.zero;
    private Vector3 _moveDirection = Vector3.zero;
    private float _moveForce = 0;
    private Rigidbody _rb;

    public override void SetPlayer(Rigidbody rigidbody)
    {
        _rb = rigidbody;
    }

    protected override void SaveStartControllerPoint(Vector3 position)
    {
        _startPosition = position;
    }

    protected override void SaveFinishControllerPoint(Vector3 position)
    {
        _finishPosition = position;
        CalculatePlayerPath();
    }

    protected override void CalculatePlayerPath()
    {
        Vector3 delta = _finishPosition - _startPosition;
        _moveDirection = delta.normalized;

        float distance = delta.magnitude;

        _moveForce = Mathf.Lerp(1f, 10f, distance) / 10f;
        MovePlayer();
    }

    protected override void MovePlayer()
    {
        _rb.AddForce(_moveDirection * _moveForce, ForceMode.Impulse);
    }

    public override void Init()
    {
        _inputHandler.OnMainClickStarted += SaveStartControllerPoint;
        _inputHandler.OnMainClickFinished += SaveFinishControllerPoint;
    }

    public void Dispose()
    {
        _inputHandler.OnMainClickStarted -= SaveStartControllerPoint;
        _inputHandler.OnMainClickFinished -= SaveFinishControllerPoint;
    }
}
