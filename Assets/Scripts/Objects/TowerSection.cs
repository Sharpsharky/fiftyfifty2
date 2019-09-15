using System.Collections.Generic;
using UnityEngine;

public class TowerSection : MonoBehaviour
{
    private LinkedList<Platform> platforms;
    private Background background;

    public void Awake()
    {
        platforms = new LinkedList<Platform>();
        background = transform.GetChild( 0 ).GetComponent<Background>();
    }

    public void Init(int index)
    {
        float width = background.GetSpriteWidth();
        float middle = width * ( index - 1 );
        float left = middle - width / 2;
        float right = middle + width / 2;

        transform.position = new Vector3( middle, 0 );
        Bounds = new Bounds( left, right );
        LastY = ( index - 1 ) * TowerFactory.SectionTick;
    }

    public Platform CreatePlatform()
    {
        Platform platform = PlatformFactory.GetPlatform( GameManager.CurrentDifficulty );

        // ustawianie pozycji
        float newX;
        if (platforms.Count == 0) {
            newX = ( Bounds.Left + Bounds.Right ) / 2f;
        } else {
            newX = UnityEngine.Random.Range( Bounds.Left + 1.5f, Bounds.Right - 1.5f );
        }

        platform.transform.parent = transform;
        platform.transform.position = new Vector3( newX, UnityEngine.Random.Range( LastY, LastY + TowerFactory.YTick ) );
        platforms.AddLast( platform );

        LastY += TowerFactory.YTick;

        return platform;
    }

    public void MoveX(float dx)
    {
        transform.position = transform.position + new Vector3( dx, 0, 0 );
        Bounds = Bounds + new Bounds( dx, dx );
    }

    public void SynchronizePositionWithPlayer()
    {
        float x = transform.position.x;
        Bounds = new Bounds( x - background.GetSpriteWidth(), x + background.GetSpriteWidth() );
    }

    public void MoveBackgroundUp()
    {
        background.MoveBackgroundUp();
    }

    public Platform RemoveLowestPlatform()
    {
        Platform platform = null;
        if (platforms.First != null) {
            platform = platforms.First.Value;
            platforms.RemoveFirst();
            PlatformFactory.BackPlatform( platform );
        }
        return platform;
    }

    public Platform GetLowestPlatform()
    {
        return platforms.First.Value;
    }

    public void RemoveAllPlatforms()
    {
        foreach (Platform platform in platforms) {
            PlatformFactory.BackPlatform( platform );
        }
        platforms.Clear();
    }

    public Bounds Bounds { get; set; }
    public float LastY { get; set; } = 0f;
    public Background Background { get => background; set => background = value; }
}

