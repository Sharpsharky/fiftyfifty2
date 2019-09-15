
using System.Collections;
using UnityEngine;


/*Use it to generate new event in game
 * Type new function and call it from einumerator as a new case
 * 
 * */

public class RandomEvents : MonoBehaviour, IObserver<Difficulty>
{
    public float easyPigeon = 3.8f;
    public float mediumPigeon = 2.8f;
    public float hardPigeon = 1.4f;

    public float easyChopper = 5.9f;
    public float mediumChopper = 4.2f;
    public float hardChopper = 3.7f;

    private float pigeonTimer;
    private float chopperTimer;

    public void Start()
    {
        GameManager.Subscribe_( this );
        pigeonTimer = easyPigeon;
        chopperTimer = easyChopper;
        StartCoroutine( DrawPigeonEvent() );
        StartCoroutine( DrawChopperEvent() );
    }

    public void Notice(Difficulty difficulty)
    {
        switch (difficulty) {
            case Difficulty.EASY:
                pigeonTimer = easyPigeon;
                chopperTimer = easyChopper;
                break;
            case Difficulty.MEDIUM:
                pigeonTimer = mediumPigeon;
                chopperTimer = mediumChopper;
                break;
            case Difficulty.HARD:
                pigeonTimer = hardPigeon;
                chopperTimer = hardChopper;
                break;
        }
    }

    public IEnumerator DrawPigeonEvent() //mainn function that draws new event and does it
    {
        while (true) {
            yield return new WaitForSeconds( pigeonTimer );
            Pigeon pigeon = PigeonFactory.GetInstance();
            pigeon.StartMoving();
        }
    }

    public IEnumerator DrawChopperEvent() //mainn function that draws new event and does it
    {
        while (true) {
            yield return new WaitForSeconds( pigeonTimer );
            Chopper chopper = ChopperFactory.GetInstance();
            chopper.StartMoving();
        }
    }
}