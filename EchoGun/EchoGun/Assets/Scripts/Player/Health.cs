using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Script for objects which are damageable and have health. Should be on the root of objects.
/// </summary>
public class Health : MonoBehaviour {

    public delegate void OnDamage(float amount);
    public delegate void OnHeal(float amount);

    public delegate void HealthChanged();

    public delegate void OnDeath();

    public event OnDamage onDamage = delegate { };
    public event OnDamage onHeal = delegate { };
    public event HealthChanged onHealthChanged = delegate { };
    public event OnDeath onDeath = delegate { };

    [SerializeField]
    protected float health = 100f;     // current health
    public float HealthValue { get { return health; } }

    void Start() {
        Assert.IsTrue(transform.parent == null); //ensure we are on the root gameobject.
    }

    /// <summary>
    /// Returns the amount by which the health was actually changed.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float SetHealth(float value) {
        float healthBefore = health;
        health = value;
        if (health > maxHealth) {
            health = maxHealth;
        }

        onHealthChanged();

        if (health <= 0) {
            onDeath();
        }

        return healthBefore - health;
    }

    // explicitely set health and max health
    public void SetHealth(float newHealth, float max) {
        health = newHealth;
        maxHealth = max;
        if(health > maxHealth) {
            health = maxHealth;
        }
        onHealthChanged();
        if (health <= 0) {
            Debug.Log("Death");
            onDeath();
        }
    }

    protected float maxHealth;
    public float healthPercent { get { return health / maxHealth; } }

    //private HealthBar bar;

    void Awake() {
        maxHealth = health;
    }

    public virtual float Damage(float amount) {
        Assert.IsTrue(amount >= 0);
        onDamage(amount);
        return SetHealth(health - amount);
    }

    public virtual float Heal(float amount) {
        Assert.IsTrue(amount >= 0);
        onHeal(amount);
        return SetHealth(health + amount);
    }

    public void Kill() {
        SetHealth(0);
    }
}
