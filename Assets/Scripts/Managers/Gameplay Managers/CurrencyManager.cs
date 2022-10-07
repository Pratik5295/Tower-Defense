using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    //This manager will be responsible for all money related functions and transactions in the game
    //Money is called as "Zings" in the came
    public static CurrencyManager Instance = null;

    [SerializeField] private int amount;

    public Action<int> OnCurrencyUpdatedEvent;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        OnCurrencyUpdatedEvent?.Invoke(amount);
    }

    public int GetAmount()
    {
        return amount;
    }

    public void AddAmount(int cost)
    {
        amount += cost;

        OnCurrencyUpdatedEvent?.Invoke(amount);
    }
    public void ReduceAmount(int cost)
    {
        amount -= cost;
        OnCurrencyUpdatedEvent?.Invoke(amount);
    }

}
