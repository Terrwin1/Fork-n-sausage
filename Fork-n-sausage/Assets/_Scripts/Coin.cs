using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action OnCoinCollected;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _coin;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnCoinCollected?.Invoke();
            _particleSystem.Play();
            _coin.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}
