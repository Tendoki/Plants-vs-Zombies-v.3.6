using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlantBombController : PlantController
{
	public GameObject explosion;
	public List<GameObject> zombies;
	public GameObject toAttack;
	public float attackCooldown;
	private float attackTime = 0;
	public int DamageValue;
	public AudioClip damageAudio;
	public float DelayDestroy = 1.5f;
	public float DelayExplosion = 0.5f;
	public bool isBoom = false;
	public SpriteRenderer backgroundSprite;
	public Sprite spriteExplosion;
	private void Start()
	{
		attackTime = Time.time + attackCooldown;
		backgroundSprite = this.GetComponent<SpriteRenderer>();
		gameManager = GameManager.instance;
	}
	private void Update()
	{
		if (!gameManager.isLose && !gameManager.isWin)
		{
			if (attackTime <= Time.time && !isBoom)
			{
				isBoom = true;
				DoesNotExist = true;
				GameObject explosionInstance = Instantiate(explosion, transform);
				explosionInstance.GetComponent<Explosion>().DamageValue = DamageValue;
				this.GetComponent<AudioSource>().PlayOneShot(damageAudio);
				this.transform.parent.GetComponent<SlotsManagerCollider>().isFull = false;
				this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteExplosion;
				StartCoroutine(DelaySpriteExplosion());
				StartCoroutine(DelayDestroyBomb());
			}
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
		backgroundSprite.enabled = false;
	}
}