using ColdCry.Utility;
using UnityEngine;

public class PlatformFactory : MonoBehaviour
{
    public Platform smallPlatform;
    public Platform mediumPlatform;
    public Platform bigPlatform;

    private ObjectPool<Platform> smallPool;
    private ObjectPool<Platform> mediumPool;
    private ObjectPool<Platform> bigPool;

    private static readonly string SMALL_P_NAME = "SmallPlatforms";
    private static readonly string MEDIUM_P_NAME = "MediumPlatforms";
    private static readonly string BIG_P_NAME = "BigPlatforms";

    private static readonly int SMALL_SIZE = 0;
    private static readonly int MEDIUM_SIZE = 1;
    private static readonly int BIG_SIZE = 2;

    private static PlatformFactory Instance;

    private void Awake()
    {
        if (Instance != null) {
            throw new ColdCry.Exception.SingletonException( "There can be only one object of PlatformFactory on scene!" );
        }
        Instance = this;

        smallPool = new ObjectPool<Platform>( smallPlatform, 93, SMALL_P_NAME );
        mediumPool = new ObjectPool<Platform>( mediumPlatform, 93, MEDIUM_P_NAME );
        bigPool = new ObjectPool<Platform>( bigPlatform, 93, BIG_P_NAME );

        foreach (Platform platform in smallPool) {
            platform.poolParent = smallPool.Parent.name;
        }

        foreach (Platform platform in mediumPool) {
            platform.poolParent = mediumPool.Parent.name;
        }

        foreach (Platform platform in bigPool) {
            platform.poolParent = bigPool.Parent.name;
        }

    }

    public static Platform GetPlatform(Difficulty difficulty)
    {
        Platform platform = null;
        switch (difficulty) {
            case Difficulty.HARD:
                platform = Instance.smallPool.Get();
                platform.SetSprite( ColdCry.Graphic.Graphics.GetPlatform( SMALL_SIZE ) );
                return platform;
            case Difficulty.MEDIUM:
                platform = Instance.mediumPool.Get();
                platform.SetSprite( ColdCry.Graphic.Graphics.GetPlatform( MEDIUM_SIZE ) );
                return platform;
            case Difficulty.EASY:
                platform = Instance.bigPool.Get();
                platform.SetSprite( ColdCry.Graphic.Graphics.GetPlatform( BIG_SIZE ) );
                return platform;
        }
        throw new ColdCry.Exception.MissingTypeException( "Missing implementation for difficulty: " + difficulty );
    }

    public static void BackPlatform(Platform platform)
    {
        if (SMALL_P_NAME.Equals( platform.poolParent )) {
            Instance.smallPool.Return( platform );
        } else if (MEDIUM_P_NAME.Equals( platform.poolParent )) {
            Instance.mediumPool.Return( platform );
        } else if (BIG_P_NAME.Equals( platform.poolParent )) {
            Instance.bigPool.Return( platform );
        } else {
            throw new System.Exception( "Platform doesn't belongs to any pool, platoform's parent name: " + platform.poolParent );
        }
    }

    public static void BackPlatformToParent(Platform platform)
    {
        if (SMALL_P_NAME.Equals( platform.poolParent )) {
            Instance.smallPool.ReturnToParent( platform );
        } else if (MEDIUM_P_NAME.Equals( platform.poolParent )) {
            Instance.mediumPool.ReturnToParent( platform );
        } else if (BIG_P_NAME.Equals( platform.poolParent )) {
            Instance.bigPool.ReturnToParent( platform );
        } else {
            throw new System.Exception( "Platform doesn't belongs to any pool, platoform's parent name: " + platform.poolParent );
        }
    }

}
