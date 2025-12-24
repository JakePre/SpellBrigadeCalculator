namespace SpellBrigadeCalculator.Engine;

public record Upgrade
{
    public required string Name { get; init; }

    /// <summary>
    /// The spell this upgrade targets. Null means it's a global upgrade.
    /// </summary>
    public string? SpellName { get; init; }

    public int IncreaseSpellDamage { get; init; } = 0;
    public int IncreaseCastSpeed { get; init; } = 0;
    public int IncreaseCriticalChance { get; init; } = 0;
    public int IncreaseBaseCriticalDamage { get; init; } = 0;

    public string DisplayText
    {
        get
        {
            var target = SpellName ?? "Global";
            if (IncreaseSpellDamage > 0)
            {
                return $"{target} - Increase Spell Damage: {IncreaseSpellDamage}%";
            }

            if (IncreaseCastSpeed > 0)
            {
                return $"{target} - Increase Cast Speed: {IncreaseCastSpeed}%";
            }

            if (IncreaseCriticalChance > 0)
            {
                return $"{target} - Increase Critical Chance: {IncreaseCriticalChance}%";
            }

            if (IncreaseBaseCriticalDamage > 0)
            {
                return $"{target} - Increase Critical Damage: {IncreaseBaseCriticalDamage}%";
            }

            return $"{target} - Unknown Upgrade";
        }
    }
}
