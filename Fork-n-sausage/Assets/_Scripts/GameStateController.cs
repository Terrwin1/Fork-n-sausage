using UnityEngine;

public class GameStateController : IInjectable
{
    private Rigidbody _playerRb;

    private void StopGame()
    {
        _playerRb.isKinematic = true;
    }

    public void SetPlayer(Rigidbody rigidbody)
    {
        _playerRb = rigidbody;
    }

    public void Init()
    {
        FinishController.OnPlayerFinished += StopGame;
    }

    public void Dispose()
    {
        FinishController.OnPlayerFinished -= StopGame;
    }
}
