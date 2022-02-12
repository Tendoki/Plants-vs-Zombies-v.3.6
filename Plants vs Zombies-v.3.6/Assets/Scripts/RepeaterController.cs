using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class RepeaterController : PlantController
{
	public GameObject bullet;
	public List<GameObject> zombies;
	public GameObject toAttack;
	public float attackCooldownBetween;
	public float attackCooldownDefault;
	private float attackCooldown;
	private bool isBetween = true;
	private float attackTime;
	public int DamageValue;
	public List<AudioClip> damageAudio;

	private void Update()
	{
		if (!gameManager.isLose && !gameManager.isWin)
		{
			if (zombies.Count > 0)
			{
				float distance = 10f;
				foreach (GameObject zombie in zombies)
				{
					float zombieDistance = Vector3.Distance(transform.position, zombie.transform.position);
					if (zombieDistance < distance)
					{
						toAttack = zombie;
						distance = zombieDistance;
					}
				}
			}
			else
			{
				toAttack = null;
			}
			if (toAttack != null)
			{
				if (attackTime <= Time.time)
				{
					GameObject bulletInstance = Instantiate(bullet, transform);
					this.GetComponent<AudioSource>().PlayOneShot(damageAudio[Random.Range(0, damageAudio.Count)]);
					bulletInstance.GetComponent<Bullet>().DamageValue = DamageValue;
					if (isBetween)
                    {
						attackCooldown = attackCooldownBetween;
						isBetween = false;
					}
					else
                    {
						attackCooldown = attackCooldownDefault;
						isBetween = true;
					}
					attackTime = Time.time + attackCooldown;
				}
			}
		}

	}
}

