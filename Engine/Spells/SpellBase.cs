namespace SpellBrigadeCalculator.Engine.Spells;

public abstract record SpellBase
{
    /// <summary>
    /// Wiki: Spell name
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Wiki: "Base spell damage"
    /// </summary>
    public abstract double BaseDamage { get; }

    /// <summary>
    /// Wiki: "Base spell cooldown" (in milliseconds, e.g., ~1.886 sec = 1886)
    /// </summary>
    public abstract int BaseCooldownMs { get; }

    /// <summary>
    /// Number of projectiles per cast (for multi-projectile spells)
    /// </summary>
    public virtual int ProjectileCount => 1;

    /// <summary>
    /// Path to spell icon image
    /// </summary>
    public abstract string ImagePath { get; }
}
