using UnityEngine;
using TMPro;

public class CurrencyUIManager : MonoBehaviour
{
    //This manager is responsible for updating currency and UI based on

    public static CurrencyUIManager Instance = null;

    private CurrencyManager currencyManager;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI currencyText;
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
        currencyManager = CurrencyManager.Instance;
        currencyManager.OnCurrencyUpdatedEvent += OnCurrencyUpdatedEventHandler;
    }

    private void OnDisable()
    {
        currencyManager.OnCurrencyUpdatedEvent -= OnCurrencyUpdatedEventHandler;
    }

    private void OnCurrencyUpdatedEventHandler(int amount)
    {
        currencyText.text = $"Zings: {amount.ToString()}";
    }
}
