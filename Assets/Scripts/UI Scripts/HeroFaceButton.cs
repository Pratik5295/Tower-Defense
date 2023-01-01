using UnityEngine.UI;
using UnityEngine;

public class HeroFaceButton : MonoBehaviour
{
    private int PARAM_STATE = Animator.StringToHash("State");
    [SerializeField] private Animator animator;

    public Toggle toggle;

    public Hero hero;

    private InputManager inputManager;

    public enum State
    {
        UNSELECTED = 0,
        SELECTED = 1
    }

    public State state;

    private void Start()
    {
        animator = GetComponent<Animator>();

        inputManager = InputManager.Instance;
        UpdateState(toggle.isOn);
    }

    public void OnToggleClicked()
    {
        if (toggle.isOn)
        {
            inputManager.HeroButtonToggle(hero);
        }
        else
        {
            inputManager.HeroButtonToggle(null);
        }
        UpdateState(toggle.isOn);
    }

    public void UpdateState(bool value)
    {
        State tempState = value ? State.SELECTED : State.UNSELECTED;

        SetState(tempState);
    }

    public void TurnToggleOff()
    {
        Debug.Log("Turning toggle off");
        inputManager.HeroButtonToggle(null);
        UpdateState(false);
        toggle.isOn = false;
    }

    public void SetState(State state)
    {
        this.state = state;

        if (animator != null && animator.gameObject.activeInHierarchy)
            animator.SetInteger(PARAM_STATE, (int)state);
    }
}
