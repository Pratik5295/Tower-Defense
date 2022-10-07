using UnityEngine;

public class UIManager : MonoBehaviour
{
    //This manager is responsible for handling messages between game and UI
    //All other UI managers will be connected to this manager

    public static UIManager Instance = null;
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
}
