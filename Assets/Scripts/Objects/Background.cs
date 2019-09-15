using UnityEngine;

public class Background : MonoBehaviour
{
    private SpriteRenderer[] backgrounds;

    private void Awake()
    {
        backgrounds = new SpriteRenderer[3];
        for (var i = 0; i < backgrounds.Length; i++) {
            backgrounds[i] = transform.GetChild( i ).GetComponent<SpriteRenderer>(); 
        }
    }

    public void MoveBackgroundUp()
    {
        Vector3 oldPos = backgrounds[0].transform.position;
        backgrounds[0].transform.position = new Vector3( oldPos.x, GetHighestBackgroundY() + GetSpriteHeigth()  );

        SpriteRenderer[] newSet = new SpriteRenderer[backgrounds.Length];
        newSet[0] = backgrounds[1];
        newSet[1] = backgrounds[2];
        newSet[2] = backgrounds[0];

        backgrounds = newSet;
    }

    public float GetSpriteWidth()
    {
        return backgrounds[0].bounds.size.x;
    }

    public float GetSpriteHeigth()
    {
        return backgrounds[0].bounds.size.y;
    }

    public float GetLowestBackgroundY()
    {
        return backgrounds[0].transform.position.y;
    }

    public float GetMiddleBackgroundY()
    {
        return backgrounds[1].transform.position.y;
    }

    public float GetHighestBackgroundY()
    {
        return backgrounds[2].transform.position.y;
    }
}
