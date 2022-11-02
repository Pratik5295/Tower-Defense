using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance = null;

    //For now, Adding hero selected on this
    [Header("Hero Selection Input")]
    public Hero selectedHeroObject;


    public bool isUI;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            isUI = false;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isUI = true;
        }
        else
        {
            isUI = false;
        }
    }

    public void HeroButtonToggle(Hero _hero)
    {
        selectedHeroObject = _hero;
    }
}
