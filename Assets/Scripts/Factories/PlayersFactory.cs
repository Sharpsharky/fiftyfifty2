using UnityEngine;

class PlayersFactory : MonoBehaviour
{
    private static PlayersFactory Instance;

    public GameObject humanPrefab;
    public GameObject hairPrefab;

    private void Awake()
    {
        if (Instance != null) {
            throw new ColdCry.Exception.SingletonException( "There can be only one object of PlayersFactory on scene!" );
        }
        Instance = this;
    }

    public static GameObject GetHumanPlayer()
    {
        GameObject player = Instantiate( Instance.humanPrefab );
        return player;
    }

    public static GameObject GetHairPlayer()
    {
        GameObject player = Instantiate( Instance.hairPrefab );
        return player;
    }
}

