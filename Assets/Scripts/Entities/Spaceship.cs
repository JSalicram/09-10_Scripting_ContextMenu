using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// RequireComponent ensures the required 
// component is present on our host GameObject:
[RequireComponent(typeof(Rigidbody2D))]
// Class to represent our space ship:
public class Spaceship : Damageable 
{
    [ContextMenuItem("Make Turn: Agile", "MakeTurnAgile")]
    [ContextMenuItem("Make Turn: Sluggish", "MakeTurnSluggish")]
    [ContextMenuItem("Make Turn: Default", "MakeTurnDefault")]
    public float maxTurnRate;        // The max rotation rate in degrees per second
    protected float defaultMaxTurnRate = 180f;

    [ContextMenuItem("Make Damage: Invulnerable", "MakeDamageInvulnerable")]
    [ContextMenuItem("Make Damage: Fragile", "MakeDamageFragile")]
    [ContextMenuItem("Make Damage: Default", "MakeDamageDefault")]
    public float standardCollisionDamage; // Basic damage to take from non-bullet collisions
    protected float defaultStandardCollisionDamage = 0.2f;

    [ContextMenuItem("Make Max Fuel: Large", "MakeFuelLarge")]
    [ContextMenuItem("Make Max Fuel: Small", "MakeFuelSmall")]
    [ContextMenuItem("Make Max Fuel: Default", "MakeFuelDefault")]
    public float maxFuel;				// The max and starting amount of fuel
    protected float defaultMaxFuel = 1f;

    [ContextMenuItem("Make Fuel Burn: None", "MakeFuelBurnNone")]
    [ContextMenuItem("Make Fuel Burn: Fast", "MakeFuelBurnFast")]
    [ContextMenuItem("Make Fuel Burn: Default", "MakeFuelBurnDefault")]
    public float fuelBurnRate;		// The amount of fuel burned per second at max throttle
    protected float defaultFuelBurnRate = 0.01f;

	protected float rotationAmount = 0;		// Amount by which the ship should rotate in the current frame
	protected Rigidbody2D rigidBody2D;		// Our ship's rigidbody
	protected SpaceshipEngine[] engines;	// Reference to our spaceship's engines
    [SerializeField]
	protected float fuel;					// Fuel the ship has
	protected bool landing = false;         // Gets set to true when we touch the landing pad

    [ContextMenu("Make All: Default")]
    protected void MakeAllDefault()
    {
        MakeTurnDefault();
        MakeDamageDefault();
        MakeFuelDefault();
        MakeFuelBurnDefault();
    }

    [ContextMenu("God Mode")]
    protected void GodMode()
    {
        MakeTurnAgile();
        MakeDamageInvulnerable();
        MakeFuelLarge();
        MakeFuelBurnNone();
    }

    [ContextMenu("Poor Mode")]
    protected void PoorMode()
    {
        MakeTurnSluggish();
        MakeDamageFragile();
        MakeFuelSmall();
        MakeFuelBurnFast();
    }

    //maxTurnMenus
    //[ContextMenu("Make Turn: Agile")]
    protected void MakeTurnAgile()
    {
        maxTurnRate = 400f;
        Debug.Log("Ship Turn: Agile");
    }
    //[ContextMenu("Make Turn: Sluggish")]
    protected void MakeTurnSluggish()
    {
        maxTurnRate = 60f;
        Debug.Log("Ship Turn: Sluggish");
    }
    //[ContextMenu("Make Turn: Default")]
    protected void MakeTurnDefault()
    {
        maxTurnRate = defaultMaxTurnRate;
        Debug.Log("Ship Turn: Default");
    }

    //DamageMenus
    //[ContextMenu("Make Damage: Invulnerable")]
    protected void MakeDamageInvulnerable()
    {
        standardCollisionDamage = 0f;
        Debug.Log("Ship Damage: Invulnerable");
    }
    //[ContextMenu("Make Damage: Fragile")]
    protected void MakeDamageFragile()
    {
        standardCollisionDamage = 0.5f;
        Debug.Log("Ship Damage: Fragile");
    }
    //[ContextMenu("Make Damage: Default")]
    protected void MakeDamageDefault()
    {
        standardCollisionDamage = defaultStandardCollisionDamage;
        Debug.Log("Ship Damage: Default");
    }

    //MaxFuelMenus
    protected void MakeFuelLarge()
    {
        maxFuel = 2f;
        Debug.Log("Ship Fuel: Large");
    }
    protected void MakeFuelSmall()
    {
        maxFuel = 0.5f;
        Debug.Log("Ship Fuel: Small");
    }
    protected void MakeFuelDefault()
    {
        maxFuel = defaultMaxFuel;
        Debug.Log("Ship Fuel: Default");
    }

    //MaxFuelBurnMenu
    protected void MakeFuelBurnNone()
    {
        fuelBurnRate = 0f;
        Debug.Log("Ship Fuel Burn: None");
    }
    protected void MakeFuelBurnFast()
    {
        fuelBurnRate = 0.1f;
        Debug.Log("Ship Fuel Burn: Fast");
    }
    protected void MakeFuelBurnDefault()
    {
        fuelBurnRate = defaultFuelBurnRate;
        Debug.Log("Ship Fuel Burn: Default");
    }

    override protected void Awake()
	{
		base.Awake();
		
		fuel = maxFuel;
		
		rigidBody2D = GetComponent<Rigidbody2D>();
		
		// Get the ship's engines:
		// (should be attached to one or more child GameObject's)
		engines = GetComponentsInChildren<SpaceshipEngine>();
	}
	
	// Use this for initialization
	void Start () 
	{
		// Tell our engines about our rigidbody so they can
		// apply forces to our ship:
		foreach (SpaceshipEngine e in engines)
			e.SetAffectedBody(rigidBody2D);
	}
	
	/// <summary>
	/// Rotates the ship by an amount that represents
	/// a percentage of the max turn-rate.
	/// Negative values result in clock-wise rotation,
	/// positive counter clock-wise.
	/// </summary>
	/// <param name="amount">Rate of turn (-1 to 1)</param>
	public void Rotate(float amount)
	{
		rotationAmount = Mathf.Clamp(amount, -1f, 1f);
	}
	
	void DoRotation()
	{
		rigidBody2D.MoveRotation(rigidBody2D.rotation + (maxTurnRate * rotationAmount * Time.deltaTime));
		
		// Reset for the next frame:
		rotationAmount = 0;
	}
	
	/// <summary>
	/// Sets the throttle of the ship's engines
	/// </summary>
	/// <value>The normalized throttle. (0-1)</value>
	public float Throttle
	{
		set
		{
			// Set the throttle to 0 if we are out of fuel:
			float val = fuel > 0 ? value : 0;
			
			foreach (SpaceshipEngine e in engines)
				e.SetThrottle(val);
		}
	}

	public void Refuel(float amount)
	{
		fuel = Mathf.Min(fuel + amount, maxFuel);
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		// If the collision was with terrain play an impact sound:
		if (col.gameObject.layer >= 8 && col.gameObject.layer <= 10)
		{
			SoundManager.PlayImpact(col.contacts[0].point);			
		}

		// Don't take damage from collisions with landing pads:
		if (col.gameObject.layer == 15)
		{
			// We are attempting to land:
			landing = true;
			return;
		}
		
		// Take our standard collision damage:
		TakeDamage(standardCollisionDamage);
		ParticleManager.EmitSparks(10, col.contacts[0].point);
	}

	void OnCollisionExit2D(Collision2D col)
	{
		// If we are leaving the landing pad, unset our landing flag:
		if (col.gameObject.layer == 15)
		{
			landing = false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
	}
	
	protected override void Die ()
	{
		base.Die ();
		
		LevelStatusLogic.PlayerDied();
	}
	
	void Update()
	{
		if (rotationAmount != 0)
			DoRotation();
		
		fuel -= engines[0].Throttle * fuelBurnRate * Time.deltaTime;

		// Check to see if we are attempting to land:
		if (landing)
		{
			// If so, check to see if we've come to rest.
			if ( Mathf.Approximately(GetComponent<Rigidbody2D>().velocity.magnitude, 0) )
			{
				LevelStatusLogic.Instance.PlayerHasLanded();
			}
		}
	}
}
