using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Player[] players;

    //find all players

    private void Awake()
    {
        players = FindObjectsOfType<Player>();
    }

    internal void AddPlayerToGame(Controller controller)
    {
        //find first player that doesn't have a controller assigned then assign controller
        var firstNonActivePlayer = players
            .OrderBy(t => t.PlayerNumber)
            .FirstOrDefault(t => t.HasController == false);

        firstNonActivePlayer.InitializePlayer(controller);
    }
}
