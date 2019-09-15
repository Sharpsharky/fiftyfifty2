using System;
using ColdCry.Utility;
using UnityEngine;

public class PigeonFactory : MonoBehaviour
{
    public Pigeon pigeon;
    public GameObject[] bodiesPrefabs;
    public GameObject[] frontWingsPrefabs;
    public GameObject[] backWingsPrefabs;
    public Shit shit;
    public int eachPart = 20;
    public float spawnOffset = 2.5f;

    public float yOffset = 1f;

    [Header( "Pigeons speed" )]
    public float minEasySpeed = 3.4f;
    public float maxEasySpeed = 5.7f;

    public float minMediumSpeed = 5.3f;
    public float maxMediumSpeed = 7.5f;

    public float minHardSpeed = 6.9f;
    public float maxHardSpeed = 8.5f;

    [Header( "Pigeon extra" )]
    public float scale = 0.35f;

    private static PigeonFactory Instance;

    private ObjectPool<Pigeon> pigeons;

    private GeneralObjectPool bodies;
    private GeneralObjectPool frontWings;
    private GeneralObjectPool backWings;

    private void Awake()
    {
        if (Instance != null) {
            throw new ColdCry.Exception.SingletonException( "There can be only one object of PigeonFactory on scene!" );
        }
        Instance = this;

        pigeons = new ObjectPool<Pigeon>( pigeon, 6, "Pigeons" );
        bodies = new GeneralObjectPool( 8, "BodyParts" );
        frontWings = new GeneralObjectPool( 8, "FrontWingsPart" );
        backWings = new GeneralObjectPool( 8, "BackWingsPart" );

        for (var i = 0; i < eachPart; i++) {
            bodies.Add( Instantiate( ColdCry.Utility.Random.FromArray( bodiesPrefabs ) ) );
            frontWings.Add( Instantiate( ColdCry.Utility.Random.FromArray( frontWingsPrefabs ) ) );
            backWings.Add( Instantiate( ColdCry.Utility.Random.FromArray( backWingsPrefabs ) ) );
        }
    }

    public static Pigeon GetInstance()
    {
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

        Pigeon pigeon = Instance.pigeons.Get();

        pigeon.transform.position = startPos;

        Transform body = Instance.bodies.GetRandom().transform;
        Transform backWing = Instance.backWings.GetRandom().transform;
        Transform frontWing = Instance.frontWings.GetRandom().transform;

        pigeon.Direction = direction;

        Vector3 oldScale = pigeon.transform.localScale;
        pigeon.transform.localScale = new Vector3( -direction * Instance.scale, oldScale.y, oldScale.z );

        body.transform.parent = pigeon.bodyJoint.transform;
        backWing.transform.parent = pigeon.backWingJoint.transform;
        frontWing.transform.parent = pigeon.frontWingJoint.transform;

        body.localScale = Vector3.one;
        backWing.localScale = Vector3.one;
        frontWing.localScale = Vector3.one;

        body.transform.localPosition = Vector3.zero;
        backWing.transform.localPosition = Vector3.zero;
        frontWing.transform.localPosition = Vector3.zero;

        pigeon.head.sprite = ColdCry.Graphic.Graphics.GetRandomHead();
        pigeon.MoveSpeed = GetPigeonSpeed( GameManager.CurrentDifficulty );

        return pigeon;
    }

    internal static void ReturnPigeon(Pigeon pigeon)
    {
        pigeon.StopMoving();
        Instance.bodies.ReturnToParent( pigeon.GetBodyPart() );
        Instance.backWings.ReturnToParent( pigeon.GetBackWingPart() );
        Instance.frontWings.ReturnToParent( pigeon.GetFrontWingPart() );
        Instance.pigeons.Return( pigeon );
    }

    public static float GetPigeonSpeed(Difficulty difficulty)
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
