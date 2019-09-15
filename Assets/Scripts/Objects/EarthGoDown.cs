using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthGoDown : MonoBehaviour
{
    public bool isGoingDown = false;
    public bool startedcoroutine = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGoingDown)
        {
            transform.position = transform.position + new Vector3(0, 1.5f * (-Time.deltaTime));
        }

        if(startedcoroutine == true)
        {
            startedcoroutine = false;

            StartCoroutine(DestroyItSelf());
        }
    }

    IEnumerator DestroyItSelf()
    {
        yield return new WaitForSeconds(4);

        Destroy(gameObject);

        yield return null;
    }

}
