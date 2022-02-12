using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int DamageValue;
    public float DelayDestroy = 1f;

    private void Start()
    {
        StartCoroutine(DelayDestroyExplosion());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<ZombieController>().ReceiveBombDamage(DamageValue);
        }
    }

    public IEnumerator DelayDestroyExplosion()
    {
        yield return new WaitForSeconds(DelayDestroy);
        Destroy(this.gameObject);
    }
}
