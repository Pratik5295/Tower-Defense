using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITowerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image image;
    public enum TOWER
    {
        SINGLE = 0,
        AREA = 1
    }

    [SerializeField] public TOWER tower;

    public bool makeTower;

    public void Awake()
    {
        image = this.GetComponent<Image>();
    }

    private void OnEnable()
    {
        image.raycastTarget = true;
    }

    private void OnDisable()
    {
        image.raycastTarget = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (data.pointerEnter == null) return;
        BuildTower();
    }

    public void BuildTower()
    {
        switch (tower)
        {
            case TOWER.SINGLE:
                UIManager.Instance.BuildTargetTower();
                break;
            case TOWER.AREA:
                UIManager.Instance.BuildAreaTower();
                break;
        }
    }
}
