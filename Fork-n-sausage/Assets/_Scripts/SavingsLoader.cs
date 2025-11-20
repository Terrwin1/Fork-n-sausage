using UnityEngine;

public class SavingsLoader
{
    public Savings LoadSavings()
    {
        return Resources.Load<Savings>("Savings");
    }
}
