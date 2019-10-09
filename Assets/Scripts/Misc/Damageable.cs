using UnityEngine;
using System.Collections;

// Provides logic and functionality for an object that is damagable.
// Any entities which are damageable should derive from this class.
// When the health value reaches 0, the object is disabled in an
// explosion.
public class Damageable : MonoBehaviour 
{
	public float maxHealth = 1f;			// The starting and max health of the object.
	public Statusbar healthBar;				// The healthbar, if any

	protected float health;


	virtual protected void Awake()
	{
		health = maxHealth;
		if (healthBar == null)
			healthBar = GetComponentInChildren<Statusbar>();
	}

	protected float Health
	{
		get { return health; }
	}

	public void Heal(float val)
	{
		health = Mathf.Min(health + val, maxHealth);
		if (healthBar != null)
			healthBar.Percent = health / maxHealth;
	}

	public void TakeDamage(float damage)
	{
		health -= damage;

		if (healthBar != null)
			healthBar.Percent = health / maxHealth;

		if (health <= 0.001f)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		ParticleManager.EmitExplosion(100, transform.position);
		SoundManager.PlayExplosion(transform.position);
		gameObject.SetActive(false);
	}
}
