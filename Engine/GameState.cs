using SpellBrigadeCalculator.Engine.Wizards;

namespace SpellBrigadeCalculator.Engine;

public class GameState
{
    public WizardBase? Wizard { get; set; }

    public SpellSlot Spell1 { get; } = new();
    public SpellSlot Spell2 { get; } = new();
    public SpellSlot Spell3 { get; } = new();
    public SpellSlot Spell4 { get; } = new();

    public List<Upgrade> UpgradeHistory { get; } = [];

    public void Reset()
    {
        Wizard = null;
        Spell1.Spell = null;
        Spell2.Spell = null;
        Spell3.Spell = null;
        Spell4.Spell = null;
        UpgradeHistory.Clear();
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        UpgradeHistory.Add(upgrade);
    }

    public void UndoLastUpgrade()
    {
        if (UpgradeHistory.Count > 0)
        {
            UpgradeHistory.RemoveAt(UpgradeHistory.Count - 1);
        }
    }

    /// <summary>
    /// Get total spell-specific upgrades for a spell
    /// Returns damage bonus (additive %) and cooldown reduction (additive %)
    /// </summary>
    public (int damageBonus, int cooldownReduction) GetSpellUpgrades(string spellName)
    {
        int damage = 0;
        int cooldown = 0;

        foreach (var upgrade in UpgradeHistory.Where(u => u.SpellName == spellName))
        {
            damage += upgrade.IncreaseSpellDamage;
            cooldown += upgrade.ReduceSpellCooldown;
        }

        return (damage, cooldown);
    }

    /// <summary>
    /// Get total global upgrades (all additive percentages)
    /// </summary>
    public (int titansFury, int veilOfHaste, int casterPrecision, int wizardsEdge) GetGlobalUpgrades()
    {
        int titansFury = 0;
        int veilOfHaste = 0;
        int casterPrecision = 0;
        int wizardsEdge = 0;

        foreach (var upgrade in UpgradeHistory.Where(u => u.SpellName == null))
        {
            titansFury += upgrade.IncreaseSpellDamage; // Titan's Fury uses damage field for base damage %
            veilOfHaste += upgrade.ReduceSpellCooldown;
            casterPrecision += upgrade.IncreaseCriticalChance;
            wizardsEdge += upgrade.IncreaseBaseCriticalDamage;
        }

        return (titansFury, veilOfHaste, casterPrecision, wizardsEdge);
    }
}
