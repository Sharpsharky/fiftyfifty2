using Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DoubleMMPrjc.Timer;

public class Life : MonoBehaviour, IOnCountdownEnd
{
    private List<GameObject> hearts = new List<GameObject>();
    public int maxHP = 3;
    private int hp;
    private bool immortal = false;
    private long immortalId;

    PlayerMove pm;
    PlayerGrab pg;
    PInput pi;
    private SpriteRenderer spriteRenderer;
    private int alphaDir = -1;

    private void Start()
    {
        pm = GetComponent<PlayerMove>();
        pg = GetComponent<PlayerGrab>();
        pi = GetComponent<PInput>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject panel = GameObject.Find( "Panel" );

        for (var i = 0; i < panel.transform.childCount; i++) {
            hearts.Add( panel.transform.GetChild(i).gameObject);
        }
        hp = maxHP;

        immortalId = TimerManager.Create( 2.25f, this );
    }

    public void TakeDamage(int dmg)
    {
        Debug.Log( "Current HP " + hp + " - dmg " + dmg + " = " + ( hp - dmg ) );
        if (( hp - dmg ) > 0) {
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
        Debug.Log( "He died!" );
        StartCoroutine( SceneReload() );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( !immortal &&  collision.tag.Equals( "Pigeon" )) {
            TakeDamage( 1 );
            immortal = true;
            TimerManager.Reset( immortalId );
            spriteRenderer.color = new Color( 255, 144, 188 );
        }
    }

    IEnumerator SceneReload()
    {
        pm.enabled = false;
        pg.enabled = false;
        pi.enabled = false;

        yield return new WaitForSeconds( 1f );

        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );

        yield return null;
    }

    public void OnCountdownEnd(long id, float overtime)
    {
        if ( id == immortalId ) {
            immortal = false;
            Color a = spriteRenderer.color;
            a.a = 1f;
            spriteRenderer.color = Color.white;
        }
    }
    
}
