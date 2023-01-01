using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //This manager is responsible for handling messages between game and UI
    //All other UI managers will be connected to this manager

    public static UIManager Instance = null;

    public GameObject tilePlacementMenu;

    [Header("Click out controllers")]
    public GameObject[] clickOuts;

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
        TurnMenuOff();
    }

    public void TurnMenuOn()
    {
        tilePlacementMenu.SetActive(true);
       // foreach (GameObject co in clickOuts) co.SetActive(true);
    }

    public void TurnMenuOff()
    {
        tilePlacementMenu.SetActive(false);
        TileManager.Instance.SetCurrentTile(null);
       // foreach (GameObject co in clickOuts) co.SetActive(false);
    }

    //Build Towers Section

    public void BuildTargetTower()
    {
        BuildingManager.Instance.SetTargetTower();
        BuildingManager.Instance.BuildItem();
        TurnMenuOff();
    }

    public void BuildAreaTower()
    {
        BuildingManager.Instance.SetAreaTower();
        BuildingManager.Instance.BuildItem();
        TurnMenuOff();
    }

    //Temporary scene restart button

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
