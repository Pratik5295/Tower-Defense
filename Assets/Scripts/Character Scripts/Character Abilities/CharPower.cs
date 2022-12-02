using UnityEngine;
using UnityEngine.UI;

public class CharPower : MonoBehaviour
{

    //Abbreviations to use

    /// <summary>
    /// 
    /// SP: Stats Power
    /// AP: Area Power
    /// BP: Building Power
    /// </summary>
    /// 

    public Button uiButton;
    public enum TYPE
    {
        STATS,
        AREA,
        BUILDING
    }

    public TYPE type;

    public Hero hero;

    private void Start()
    {
        uiButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        switch (type)
        {
            case TYPE.STATS:
                OnStatsPowerClicked();
                break;
            case TYPE.AREA:
                OnAreaPowerClicked();
                break;
            case TYPE.BUILDING:
                OnBuildingPowerClicked();
                break;
        }
    }

    public virtual void OnStatsPowerClicked()
    {

    }

    public virtual void OnAreaPowerClicked()
    {

    }

    public virtual void OnBuildingPowerClicked()
    {

    }
}
