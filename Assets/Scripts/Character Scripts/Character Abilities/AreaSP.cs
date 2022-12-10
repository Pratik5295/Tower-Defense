using UnityEngine;

public class AreaSP : CharPower
{
    public GameObject areaEffect;

    [SerializeField] private GameObject areaObject;
    public bool isSelected;
    private void Update()
    {
        if (!isSelected) return;
        if (areaObject == null) return;

        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "GameTile")
            {
                areaObject.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);

                if (Input.GetMouseButtonDown(0))
                {
                    OnAttackRelease();
                }
            }
        }

        
    }
    public override void OnAreaPowerClicked()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            areaObject = Instantiate(areaEffect);
        }
        else
        {
            if (areaObject == null) return;
            GameObject go = areaObject;
            Destroy(go);
        }
    }

    private void OnAttackRelease()
    {
        Debug.Log("Now it goes boom!");
        Destroy(areaObject);
        isSelected = false;
    }
}
