using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    public int attackPoints = 1;

    private bool afterSpawnTime = false;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.GetComponent<Life>() != null) {
                other.gameObject.GetComponent<Life>().TakeDamage(attackPoints);
                Debug.Log("Hit for:" + attackPoints);
            } else {
                Debug.LogError("Could not find 'Life' script on the player!");
            }
        }
    }

    private void Start() {
        StartCoroutine(WaitForAppearance());
    }
    private void Update() {
        
        if (afterSpawnTime) {
            Vector3 maxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0));
            Debug.Log(gameObject.transform.position.x + " ___ " + maxPos.x);
            if (gameObject.transform.position.x > maxPos.x + 10 || gameObject.transform.position.x < 0 - 10) {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator WaitForAppearance() 
    {
        yield return new WaitForSeconds(2);
        afterSpawnTime = true;
    }
}
