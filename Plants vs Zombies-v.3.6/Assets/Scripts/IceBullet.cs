using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : Bullet
{
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    void Update()
    {
        if (!gameManager.isLose && !gameManager.isWin)
        {
            transform.Translate(movementSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<ZombieController>().ReceiveDamage(DamageValue, ThermalEffect);
            Destroy(this.gameObject);
        }
    }
}
