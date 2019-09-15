using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCut : MonoBehaviour
{
    public int numberOfHairSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "EnemyCut")
        {
            if (GameObject.Find("Hair").GetComponent<HairCutCooldown>().isCooldownForHairCut == false)
            {
                StartCoroutine(DestroyAfterAMoment());
            }
            
        }
    }
    IEnumerator DestroyAfterAMoment()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.Find("Hair").transform.GetChild(numberOfHairSprite).gameObject);
        gameObject.GetComponent<HingeJoint2D>().enabled = false;
        yield return null;
    }
    IEnumerator GiveCoolDownTohairCut()
    {
        GameObject.Find("Hair").GetComponent<HairCutCooldown>().isCooldownForHairCut = true;
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("Hair").GetComponent<HairCutCooldown>().isCooldownForHairCut = false;

        yield return null;
    }

}
