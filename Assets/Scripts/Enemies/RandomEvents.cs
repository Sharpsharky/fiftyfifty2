
using DoubleMMPrjc.Timer;
using System.Collections;
using UnityEngine;


/*Use it to generate new event in game
 * Type new function and call it from einumerator as a new case
 * 
 * */

public class RandomEvents : MonoBehaviour, IObserver<Difficulty>, IOnCountdownEnd, IObserver<long>
{
    public float easyPigeon = 3.8f;
    public float mediumPigeon = 2.8f;
    public float hardPigeon = 1.4f;

    public float easyChopper = 5.9f;
    public float mediumChopper = 4.2f;
    public float hardChopper = 3.7f;

    public float firstStartPigeon = 4.5f;
    public float firstStartChoper = 11f;

    private float pigeonTimer;
    private float chopperTimer;

    private long chopperId, pigeonId;

    public void Start()
    {
        GameManager.Subscribe_( this as IObserver<long> );
        GameManager.Subscribe_( this as IObserver<Difficulty> );
        pigeonTimer = easyPigeon;
        chopperTimer = easyChopper;

        pigeonId = TimerManager.Create( firstStartPigeon, this );
        chopperId = TimerManager.Create( firstStartChoper, this );
    }

    public void Update()
    {
        TimerManager.GetRemaing( pigeonId, out float s );
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
            yield return new WaitForSeconds( chopperTimer );
            Chopper chopper = ChopperFactory.GetInstance();
            chopper.StartMoving();
        }
    }

    public void OnCountdownEnd(long id, float overtime)
    {
        if (id == pigeonId) {
            Pigeon pigeon = PigeonFactory.GetInstance();
            pigeon.StartMoving();
            TimerManager.Reset( pigeonId, pigeonTimer );
        } else if (id == chopperId) {
            Chopper chopper = ChopperFactory.GetInstance();
            chopper.StartMoving();
            TimerManager.Reset( chopperId, chopperTimer );
        }
    }

    public void Notice(long t)
    {
        if (t == 1) {
            TimerManager.Reset( pigeonId );
            TimerManager.Reset( chopperId );
        }
    }
}