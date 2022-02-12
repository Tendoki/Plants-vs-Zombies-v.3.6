using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Lawn_Mower : MonoBehaviour
{
    public Vector3 movementSpeed;
    public int DamageValue;
    public GameManager gameManager;
    public bool isMistake = false;
    public AudioClip LawnMowerAudio;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    void Update()
    {
        if (!gameManager.isLose && !gameManager.isWin )
        {
            if (isMistake)
            {
                transform.Translate(movementSpeed * Time.deltaTime);
            }
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
            isMistake = true;
            this.GetComponent<AudioSource>().PlayOneShot(LawnMowerAudio);
            collision.gameObject.GetComponent<ZombieController>().ReceiveDamage(DamageValue, 0);
        }
    }
}
