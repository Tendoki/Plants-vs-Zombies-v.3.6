using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPotatoController : PlantController
{
	public Sprite spriteHideOffLight;
	public Sprite spriteOnLight;
	public Sprite spriteOffLight;
	public Sprite spriteLight;
	public Sprite spriteNoLight;
	public AudioClip BoomAudio;
	public bool isGrowing;
	public bool isBoom;
	public SpriteRenderer backgroundSprite;
	public float DelayDestroy = 1.5f;
	public float DelayExplosion = 0.5f;
	public Sprite spriteExplosion;
	public Vector3 scale;
	public bool isPlantEating;
	public Collider2D CurrentCollision;
	private void Start()
	{
		isPlantEating = false;
		gameManager = GameManager.instance;
		isGrowing = false;
		isBoom = false;
		spriteLight = spriteHideOffLight;
		spriteNoLight = spriteHideOffLight;
		backgroundSprite = this.GetComponent<SpriteRenderer>();
		StartCoroutine(GrowPotato(this.gameObject.GetComponent<SpriteRenderer>()));
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 11 && isGrowing)
		{
			collision.gameObject.GetComponent<ZombieController>().ReceiveDamage(5000, 0);
			this.GetComponent<AudioSource>().PlayOneShot(BoomAudio);
			isBoom = true;
			this.transform.parent.GetComponent<SlotsManagerCollider>().isFull = false;
			spriteOnLight = spriteExplosion;
			spriteOffLight = spriteExplosion;
			spriteLight = spriteExplosion;
			spriteNoLight = spriteExplosion;
			spritelight = spriteExplosion;
			this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteExplosion;
			StartCoroutine(DelaySpriteExplosion());
			StartCoroutine(DelayDestroyBomb());
		}
		if (collision.gameObject.layer == 11 && !isGrowing)
        {
			isPlantEating = true;
			CurrentCollision = collision;
		}
		if (gameManager.draggingShovel != null)
		{
			if (collision.gameObject.layer == 16)
			{
				gameManager.currentPlant = this.gameObject;
			}
		}
	}

	public new bool ReceiveDamage(int Damage, bool isEating)
	{
		if (Health - Damage <= 0)
		{
			isEating = false;
			transform.parent.GetComponent<SlotsManagerCollider>().isFull = false;
			Destroy(this.gameObject);
			return isEating;
		}
		else
		{
			if (!isBoom)
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
				StartCoroutine(DamageColor(this.gameObject.GetComponent<SpriteRenderer>()));
			}
			Health -= Damage;
			return isEating;
		}
	}

	IEnumerator DamageColor(SpriteRenderer spriteRenderer)
	{
        if (!isBoom)
        {
			Sprite spriteOrig = spriteRenderer.sprite;
			spriteRenderer.sprite = spritelight;
		}
		yield return new WaitForSeconds(damageDelay);
		if (!isBoom)
        {
			spriteRenderer.sprite = spriteOrig;
		}
	}

	IEnumerator GrowPotato(SpriteRenderer spriteRenderer)
    {
		yield return new WaitForSeconds(15);
		spriteLight = spriteOnLight;
		spriteNoLight = spriteOffLight;
		isGrowing = true;
		spriteOriginal = spriteExplosion;
		spriteOrig = spriteExplosion;
		spriteHideOffLight = spriteExplosion;
        if (isPlantEating)
        {
			CurrentCollision.gameObject.GetComponent<ZombieController>().ReceiveDamage(5000, 0);
			this.GetComponent<AudioSource>().PlayOneShot(BoomAudio);
			isBoom = true;
			this.transform.parent.GetComponent<SlotsManagerCollider>().isFull = false;
			spriteOnLight = spriteExplosion;
			spriteOffLight = spriteExplosion;
			spriteLight = spriteExplosion;
			spriteNoLight = spriteExplosion;
			spritelight = spriteExplosion;
			this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteExplosion;
			StartCoroutine(DelaySpriteExplosion());
			StartCoroutine(DelayDestroyBomb());
		}
		StartCoroutine(FlashingOnPotato(spriteRenderer));
	}

	IEnumerator FlashingOnPotato(SpriteRenderer spriteRenderer)
    {
		spriteRenderer.sprite = spriteLight;
		yield return new WaitForSeconds(0.5f);
        if (!isBoom)
        {
			StartCoroutine(FlashingOffPotato(spriteRenderer));
		}
	}
	IEnumerator FlashingOffPotato(SpriteRenderer spriteRenderer)
    {
		spriteRenderer.sprite = spriteNoLight;
		yield return new WaitForSeconds(0.5f);
        if (!isBoom)
        {
			StartCoroutine(FlashingOnPotato(spriteRenderer));
		}
	}
	public IEnumerator DelayDestroyBomb()
	{
		yield return new WaitForSeconds(DelayDestroy);
		Destroy(this.gameObject);
	}

	public IEnumerator DelaySpriteExplosion()
	{
		yield return new WaitForSeconds(DelayExplosion);
		isGrowing = false;
		backgroundSprite.enabled = false;
	}
}
