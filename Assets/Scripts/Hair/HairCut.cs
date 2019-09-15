using System.Collections;
using UnityEngine;

public class HairCut : MonoBehaviour
{
    public int numberOfHairSprite;
    private GameObject mainHair;
    private HairCutCooldown cutCooldown;

    // Start is called before the first frame update
    void Start()
    {
        mainHair = GameObject.FindWithTag( "MainHair" );
        cutCooldown = mainHair.GetComponent<HairCutCooldown>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyCut") {
            if (cutCooldown.isCooldownForHairCut == false) {
                StartCoroutine( DestroyAfterAMoment() );
            }

        }
    }

    IEnumerator DestroyAfterAMoment()
    {
        yield return new WaitForSeconds( 0.5f );
        Destroy( mainHair.transform.GetChild( numberOfHairSprite ).gameObject );
        gameObject.GetComponent<HingeJoint2D>().enabled = false;
        yield return null;
    }

    IEnumerator GiveCoolDownTohairCut()
    {
        cutCooldown.isCooldownForHairCut = true;
        yield return new WaitForSeconds( 1.5f );
        cutCooldown.isCooldownForHairCut = false;
        yield return null;
    }

}
