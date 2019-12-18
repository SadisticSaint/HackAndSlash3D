using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Image markerImage;
    [SerializeField]
    private Image lockImage;

    private UICharacterSelectionMenu menu;
    private bool initializing;
    private bool initialized;

    public bool IsLockedIn { get; private set; }
    public bool IsPlayerIn { get { return player.HasController; } }

    private void Awake()
    {
        menu = GetComponentInParent<UICharacterSelectionMenu>();
        markerImage.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (IsPlayerIn == false)
            return;

        if (!initializing)
            StartCoroutine(Initialize());

        if (!initialized)
            return;

        if (!IsLockedIn)
        {
            if (player.Controller.horizontal > 0.1)
                MoveToCharacterPanel(menu.RightPanel);
            else if (player.Controller.horizontal < -0.1)
                MoveToCharacterPanel(menu.LeftPanel);

            if (player.Controller.attackPressed)
                LockCharacter();
        }
        else
        {
            if (player.Controller.attackPressed)
            {
                menu.TryToStartGame();
            }
        }
    }

    private void LockCharacter()
    {
        IsLockedIn = true;
        lockImage.gameObject.SetActive(true);
        markerImage.gameObject.SetActive(false);
    }

    private void MoveToCharacterPanel(UICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position;
    }

    private IEnumerator Initialize()
    {
        initializing = true;
        MoveToCharacterPanel(menu.LeftPanel);
        yield return new WaitForSeconds(0.5f); //helps to make sure press isn't registered twice
        markerImage.gameObject.SetActive(true);
        initialized = true;
    }
}
