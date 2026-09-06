using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_WindSound : MonoBehaviour
{
    [SerializeField] private int areSoundIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            AudioManager.instance.PlaySFX(areSoundIndex, null);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
            AudioManager.instance.StopSFXOverTime(areSoundIndex);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            AudioManager.instance.PlaySFX(areSoundIndex, null);
    }
}
