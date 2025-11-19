using System;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public static event Action OnPlayerFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            OnPlayerFinished?.Invoke();
        }  
    }
}
