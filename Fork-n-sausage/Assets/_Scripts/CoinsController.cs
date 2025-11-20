using System;
using UnityEngine;

public class CoinsController : IInjectable
{
    [Inject] private SavingsLoader _savingsLoader;
    public event Action<int> OnCoinsChanged;
    private int _coins;
    public int Coins => _coins;

    private void AddCoin()
    {
        _coins += 1;
        OnCoinsChanged?.Invoke(_coins);
    }

    public bool TrySpendCoins(int coins)
    {
        if (coins >= 0 && coins <= _coins)
        {
            _coins -= coins;
            OnCoinsChanged?.Invoke(_coins);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LoadData()
    {
        _coins = _savingsLoader.LoadSavings()._coins;
    }

    public void Init()
    {
        LoadData();
        Coin.OnCoinCollected += AddCoin;
    }
}
