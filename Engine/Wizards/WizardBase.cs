namespace SpellBrigadeCalculator.Engine.Wizards;

public abstract record WizardBase
{
    /// <summary>
    /// Wiki: "Increase Spell Damage" (percentage)
    /// </summary>
    public int IncreaseSpellDamage { get; init; } = 0;

    /// <summary>
    /// Wiki: "Titan's Fury" (Increase Base Damage) - flat damage bonus
    /// </summary>
    public int TitansFury { get; init; } = 0;

    /// <summary>
    /// Wiki: "Reduce Spell Cooldown" (percentage)
    /// </summary>
    public int ReduceSpellCooldown { get; init; } = 0;

    /// <summary>
    /// Wiki: "Reduce Global Cooldown" (Veil of Haste) (percentage)
    /// </summary>
    public int ReduceGlobalCooldown { get; init; } = 0;

    /// <summary>
    /// Wiki: "Increase Critical Chance" (percentage, base 5%)
    /// </summary>
    public int IncreaseCriticalChance { get; init; } = 5;

    /// <summary>
    /// Wiki: "Increase Base Critical Damage" (percentage, base 150%)
    /// </summary>
    public int IncreaseBaseCriticalDamage { get; init; } = 150;

    /// <summary>
    /// Wizard display name
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Signature spell name for this wizard
    /// </summary>
    public abstract string SignatureSpell { get; }

    /// <summary>
    /// Path to wizard portrait image
    /// </summary>
    public abstract string ImagePath { get; }
}
