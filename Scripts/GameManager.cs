using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public AsyncOperation Operation { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Begin()
    {
        StartCoroutine(BeginGame());
    }

    private IEnumerator BeginGame()
    {
        Operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        while (Operation.isDone == false)
            yield return null;

        PlayerManager.Instance.SpawnPlayerCharacters();
    }
}
