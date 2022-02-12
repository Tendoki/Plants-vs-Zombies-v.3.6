using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ZombieController : MonoBehaviour
{
    public int Health;
    public int DamageValue;
    public float attackCooldown;
    public Vector3 movementSpeed;
    public bool isStopped;
    private GameManager gameManager;
    public Sprite spritelight;
    public Sprite spriteFrozen;
    public Sprite spriteOriginal;
    public Sprite spriteNow;
    public float damageDelay = 0.16f;
    public List<AudioClip> damageAudio;
    public List<AudioClip> chompAudio;
    public List<AudioClip> groanAudio;
    public AudioClip frozenAudio;
    public AudioClip gulpAudio;
    public float time;
    public float minTime;
    public float maxTime;
    public bool isEating = false;
    public bool isFrozen = false;
    public int countIceBullet;
    public SpriteRenderer backgroundSprite;
    private void Start()
    {
        gameManager = GameManager.instance;
        time = Random.Range(minTime, maxTime);
        backgroundSprite = this.GetComponent<SpriteRenderer>();
        StartCoroutine(GroanRandom());
    }
    void Update()
    {
        if (gameManager.isLose || gameManager.isWin)
        {
            StopCoroutine(GroanRandom());
        }
        if (!gameManager.isLose && !gameManager.isWin)
		{
            if (!isStopped)
            {
                transform.Translate(movementSpeed * Time.deltaTime * -1);
            }
        }
    }

    public IEnumerator GroanRandom()
    {
        this.GetComponent<AudioSource>().PlayOneShot(groanAudio[Random.Range(0, groanAudio.Count)]);
        yield return new WaitForSeconds(time);
        time = Random.Range(minTime, maxTime);
        if (gameManager.isLose || gameManager.isWin)
        {
            StopCoroutine(GroanRandom());
        }
        else
        {
            StartCoroutine(GroanRandom());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (!collision.gameObject.GetComponent<PlantController>().DoesNotExist)
            {
                if (!isEating)
                {
                    isEating = true;
                    StartCoroutine(Attack(collision));
                    isStopped = true;
                }
            }
            else
            {
                isEating = false;
                isStopped = false;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isEating = false;
            isStopped = false;
        }
    }
    IEnumerator Attack(Collider2D collision)
    {
        if (collision == null)
        {
            isStopped = false;
        }
        else
        {
            if (!collision.gameObject.GetComponent<PlantController>().DoesNotExist)
            {
                isEating = collision.gameObject.GetComponent<PlantController>().ReceiveDamage(DamageValue, isEating);
                this.GetComponent<AudioSource>().PlayOneShot(chompAudio[Random.Range(0, chompAudio.Count)]);
                if (!isEating)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(gulpAudio);
                }
                yield return new WaitForSeconds(attackCooldown);
                StartCoroutine(Attack(collision));
            }
            else
            {
                isStopped = false;
                isEating = false;
                yield return new WaitForSeconds(attackCooldown);
            }
        }
    }

    public void ReceiveDamage(int Damage, int ThermalEffect)
    {
        this.GetComponent<AudioSource>().PlayOneShot(damageAudio[Random.Range(0, damageAudio.Count)]);
        if (Health - Damage <= 0)
        {
            transform.parent.GetComponent<SpawnPoint>().zombies.Remove(this.gameObject);
            backgroundSprite.enabled = false;
            if (gameManager.zombiesLevel1 > 1)
			{
                gameManager.zombiesLevel1--;
            }
            else
			{
                gameManager.isWin = true;
			}
            Destroy(this.gameObject);
        }
        else
        {
			if (!isFrozen)
			{
                this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
            }
			if (!isFrozen && ThermalEffect == 1)
			{
                this.GetComponent<AudioSource>().PlayOneShot(frozenAudio);
            }
            StartCoroutine(DamageColor(this.gameObject.GetComponent<SpriteRenderer>(), ThermalEffect));
            Health -= Damage;
        }
    }

    public void ReceiveBombDamage(int Damage)
    {
        if (Health - Damage <= 0)
        {
            transform.parent.GetComponent<SpawnPoint>().zombies.Remove(this.gameObject);
            if (gameManager.zombiesLevel1 > 1)
            {
                gameManager.zombiesLevel1--;
            }
            else
            {
                gameManager.isWin = true;
            }
            Destroy(this.gameObject);
        }
        else
        {
            Health -= Damage;
        }
    }

    IEnumerator DamageColor(SpriteRenderer spriteRenderer, int ThermalEffectValue)
	{
        if (ThermalEffectValue == 1)
		{
            countIceBullet++;
            isFrozen = true;
            spriteRenderer.sprite = spriteFrozen;
            Vector3 movementSpeedOrig = new Vector3(0.23f, 0, 0);
            movementSpeed = new Vector3(0.12f, 0, 0);
            yield return new WaitForSeconds(10f);
            countIceBullet--;
			if (countIceBullet == 0)
			{
                isFrozen = false;
                movementSpeed = movementSpeedOrig;
                spriteRenderer.sprite = spriteOriginal;
            }
        }
		else if (ThermalEffectValue == 0)
		{
            spriteNow = spriteRenderer.sprite;
            spriteRenderer.sprite = spritelight;
            yield return new WaitForSeconds(damageDelay);
			if (isFrozen)
			{
                spriteRenderer.sprite = spriteFrozen;
            }
			else
			{
                spriteRenderer.sprite = spriteOriginal;
            }
        }
    }
}
