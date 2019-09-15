using ColdCry.Utility;
using UnityEngine;

public class ChopperFactory : MonoBehaviour
{
    public Chopper chopperPrefab;

    private static ChopperFactory Instance;
    private ObjectPool<Chopper> choppers;

    public float yOffset = 1f;

    [Header( "Choppers speed" )]
    public float minEasySpeed = 2.4f;
    public float maxEasySpeed = 3.7f;

    public float minMediumSpeed = 3.7f;
    public float maxMediumSpeed = 5.5f;

    public float minHardSpeed = 8.5f;
    public float maxHardSpeed = 11.5f;

    private void Awake()
    {
        if (Instance != null) {
            throw new ColdCry.Exception.SingletonException( "There can be only one object of ChopperFactory on scene!" );
        }
        Instance = this;

        choppers = new ObjectPool<Chopper>( chopperPrefab, 5, "Choppers" );
    }

    public static Chopper GetInstance()
    {
        Chopper chopper = Instance.choppers.Get();

        int direction = UnityEngine.Random.Range( 0, 2 );
        Vector3 startPos = Vector3.zero;
        if (direction == 0) {
            direction = -1;
            startPos = new Vector3(
                GameManager.CameraXBound.Right + EnemyFactory.GetSpawnOffsetX( GameManager.CurrentDifficulty ),
                UnityEngine.Random.Range( GameManager.CameraYBound.Left + Instance.yOffset, GameManager.CameraYBound.Right - Instance.yOffset ) );
        } else {
            startPos = new Vector3(
                GameManager.CameraXBound.Left - EnemyFactory.GetSpawnOffsetX( GameManager.CurrentDifficulty ),
                UnityEngine.Random.Range( GameManager.CameraYBound.Left + Instance.yOffset, GameManager.CameraYBound.Right - Instance.yOffset ) );
        }

        Vector3 oldScale = chopper.transform.localScale;
        chopper.transform.localScale = new Vector3( -direction * oldScale.x, oldScale.y, oldScale.z );

        return chopper;
    }

    public static void ReturnChopper(Chopper chopper)
    {
        Instance.choppers.ReturnToParent( chopper );
    }


    public static float GetChopperSpeed(Difficulty difficulty)
    {
        switch (difficulty) {
            case Difficulty.EASY:
                return UnityEngine.Random.Range( Instance.minEasySpeed, Instance.maxEasySpeed );
            case Difficulty.MEDIUM:
                return UnityEngine.Random.Range( Instance.minMediumSpeed, Instance.maxMediumSpeed );
            case Difficulty.HARD:
                return UnityEngine.Random.Range( Instance.minHardSpeed, Instance.maxHardSpeed );
        }
        throw new ColdCry.Exception.MissingTypeException( "Not implemented type: " + difficulty );
    }

}
