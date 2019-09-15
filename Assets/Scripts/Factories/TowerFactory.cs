using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [Header( "Tower's prefabs" )]
    public Tower tower;
    public GameObject darkEffectPrefab;

    [Header( "Tower Settings" )]
    [SerializeField] private float xTick = 9.599f;
    [SerializeField] private float yTick = 2.35f;
    [SerializeField] private float sectionTick = 1.8f;
    [SerializeField] [Range( 1, 600 )] private int platformsForEachSection = 18;

    private static TowerFactory Instance;

    private void Awake()
    {
        if (Instance != null) {
            throw new ColdCry.Exception.SingletonException( "There can be only one object of TowerFactory on scene!" );
        }
        Instance = this;
    }

    // tworzy nową wieże
    public static Tower GetInstance()
    {
        Tower tower = Instantiate( Instance.tower );
        tower.DarkEffect = Instantiate( Instance.darkEffectPrefab );
        return tower;
    }

    // dystanse miedzy kolejnymi platformami
    public static float XTick { get => Instance.xTick; set => Instance.xTick = value; }
    public static float YTick { get => Instance.yTick; set => Instance.yTick = value; }
    public static float SectionTick { get => Instance.sectionTick; set => Instance.sectionTick = value; }
    public static int PlatformsForEachSection { get => Instance.platformsForEachSection; set => Instance.platformsForEachSection = value; }
}
