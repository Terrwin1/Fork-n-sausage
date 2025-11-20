using UnityEngine;
using TMPro;

public class CoinsViewGame : MonoBehaviour, IInjectable
{
    [Inject] private CoinsController _coinsController;
    [SerializeField] private TMP_Text _coinsText;

    private void RefreshText(int coins)
    {
        _coinsText.text = coins.ToString();
    }

    public void Init()
    {
        RefreshText(_coinsController.Coins);
        _coinsController.OnCoinsChanged += RefreshText;
    }

    private void OnDestroy()
    {
        _coinsController.OnCoinsChanged -= RefreshText;
    }
}
