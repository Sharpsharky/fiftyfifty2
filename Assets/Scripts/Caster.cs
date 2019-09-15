using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Caster : MonoBehaviour, IObservable<Collider2D>
{

    private List<IObserver<Collider2D>> observers = new List<IObserver<Collider2D>>();

    public void Subscribe(IObserver<Collider2D> t) {
        observers.Add(t);
    }
    public void Unsubscribe(IObserver<Collider2D> t) {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);


   

        if (hit.collider == null) 
        {
            foreach (var observer in observers) {
                observer.Notice(hit.collider);
            }
        }
            //hit.collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 2, 255);
        
    }
}
