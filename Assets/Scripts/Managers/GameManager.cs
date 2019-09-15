using DoubleMMPrjc.Timer;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IOnCountdownEnd, IObservable<Difficulty>
{
    private static GameManager Instance;

    private Tower tower;
    private GameObject humanPlayer;
    private GameObject hairPlayer;

    [SerializeField] private float easyTimer = 45f;
    [SerializeField] private float mediumTimer = 45f;
    [SerializeField] private float hardTimer = 45f;

    [SerializeField] private float easySpeed = 0.3f;
    [SerializeField] private float mediumSpeed = 0.5f;
    [SerializeField] private float hardSpeed = 0.7f;

    private LinkedList<IObserver<Difficulty>> difficultyObservers = new LinkedList<IObserver<Difficulty>>();

    private Difficulty currentDifficulty = Difficulty.EASY;
    private float currentGameSpeed;
    private long gameTimerId;
    private Bounds cameraXBound, cameraYBound;

    private void Awake()
    {
        if (Instance != null) {
            throw new ColdCry.Exception.SingletonException( "There can be only one object of GameManager on scene!" );
        }
        Instance = this;
    }

    private void Start()
    {
        float cameraX = Camera.main.orthographicSize * Screen.width / Screen.height;
        float cameraY = Camera.main.orthographicSize;

        cameraXBound = new Bounds( -cameraX, cameraX );
        cameraYBound = new Bounds( -cameraY, cameraY );

        ColdCry.Graphic.Graphics.LoadGraphics();
        tower = TowerFactory.GetInstance();
        humanPlayer = PlayersFactory.GetHumanPlayer();
        hairPlayer = PlayersFactory.GetHairPlayer();

        hairPlayer.GetComponent<HairMove>().Tower = tower;

        Vector3 firstPlatformPos = tower.GetLowestPlatform(1).transform.position;
        humanPlayer.transform.position = firstPlatformPos + new Vector3( 0, 0.25f );

        // Timers initialization
        Instance.gameTimerId = TimerManager.Start( easyTimer, this );
    }

    public void OnCountdownEnd(long id, float overtime)
    {
        if (id == gameTimerId) {
            switch (CurrentDifficulty) {
                case Difficulty.EASY:
                    CurrentDifficulty = Difficulty.MEDIUM;
                    TimerManager.Reset( id, mediumTimer );
                    break;
                case Difficulty.MEDIUM:
                    CurrentDifficulty = Difficulty.HARD;
                    TimerManager.Reset( id, hardTimer );
                    break;
                case Difficulty.HARD:
                    // TODO END GAME!!
                    break;
                default:
                    throw new ColdCry.Exception.MissingTypeException( "Missing implementation of difficulty: " + CurrentDifficulty );
            }

            foreach (IObserver<Difficulty> observer in Instance.difficultyObservers) {
                observer.Notice( CurrentDifficulty );
            }
        }
    }

    /*
     *  Zwraca szybkość gry (poruszania się tła)
     */
    public static float GetGameSpaeed(Difficulty difficulty)
    {
        switch (difficulty) {
            case Difficulty.EASY:
                return Instance.easySpeed;
            case Difficulty.MEDIUM:
                return Instance.mediumSpeed;
            case Difficulty.HARD:
                return Instance.hardSpeed;
            default:
                throw new ColdCry.Exception.MissingTypeException( "Missing implementation of difficulty: " + difficulty );
        }
    }

    public static void Subscribe_(IObserver<Difficulty> observer)
    {
        Instance.Subscribe( observer );
    }

    public static void Unsubscribe_(IObserver<Difficulty> observer)
    {
        Instance.Unsubscribe( observer );
    }

    public void Subscribe(IObserver<Difficulty> observer)
    {
        difficultyObservers.AddLast( observer );
    }

    public void Unsubscribe(IObserver<Difficulty> observer)
    {
        difficultyObservers.Remove( observer );
    }

    public static Difficulty CurrentDifficulty { get => Instance.currentDifficulty; set => Instance.currentDifficulty = value; }
    public static Bounds CameraXBound { get => Instance.cameraXBound; set => Instance.cameraXBound = value; }
    public static Bounds CameraYBound { get => Instance.cameraYBound; set => Instance.cameraYBound = value; }
    public static Tower Tower { get => Instance.tower; }
    public static GameObject HumanPlayer { get => Instance.humanPlayer; }
    public static GameObject HairPlayer { get => Instance.hairPlayer; }
}
