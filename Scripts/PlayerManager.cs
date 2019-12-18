using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Player[] players;

    public static PlayerManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        players = FindObjectsOfType<Player>();
    }

    public void AddPlayerToGame(Controller controller) //internal vs. public?
    {
        //find first player that doesn't have a controller assigned then assign controller
        var firstNonActivePlayer = players
            .OrderBy(t => t.PlayerNumber)
            .FirstOrDefault(t => t.HasController == false);

        firstNonActivePlayer.InitializePlayer(controller);
    }

    public void SpawnPlayerCharacters()
    {
        foreach (var player in players)
        {
            if(player.HasController && player.CharacterPrefab != null)
            {
                player.SpawnCharacter();
            }
        }
    }
}
