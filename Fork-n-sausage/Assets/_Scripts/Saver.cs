public class Saver : IInjectable
{
    [Inject] private CoinsController _coinsController;
    [Inject] private SavingsLoader _savingsLoader;
     
    private void SaveCoins(int coins)
    {
        _savingsLoader.LoadSavings()._coins = coins;
    }

    public void Init()
    {
        _coinsController.OnCoinsChanged += SaveCoins;
    }

    public void Dispose()
    {
        _coinsController.OnCoinsChanged -= SaveCoins;
    }
}
