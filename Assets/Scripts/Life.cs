using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Inputs;
public class Life : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    public int maxHP = 3;
    private int hp;

    PlayerMove pm;
    PlayerGrab pg;
    PInput pi;

    private void Start() {
        pm = GetComponent<PlayerMove>();
        pg = GetComponent<PlayerGrab>();
        pi = GetComponent<PInput>();

        hp = maxHP;
    }
    public void TakeDamage(int dmg) 
    {
        Debug.Log("Current HP "+ hp + " - dmg " + dmg + " = " + (hp-dmg));
        if ((hp - dmg) > 0) {
            var tempColor = hearts[hp - 1].GetComponent<Image>().color;
            tempColor.a = 0;
            hearts[hp - 1].GetComponent<Image>().color = tempColor;
            hp -= dmg;
        } else {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("He died!");
        StartCoroutine(SceneReload());
    }

    IEnumerator SceneReload()
    {
        pm.enabled = false;
        pg.enabled = false;
        pi.enabled = false;


        yield return new WaitForSeconds(1f);


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return null;
    }


}
