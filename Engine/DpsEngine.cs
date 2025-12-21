using SpellBrigadeCalculator.Engine.Spells;
using SpellBrigadeCalculator.Engine.Wizards;

namespace SpellBrigadeCalculator.Engine;

public static class DpsEngine
{
    /// <summary>
    /// Calculate DPS for a spell slot using game state and upgrade history
    /// </summary>
    public static double CalculateDps(GameState game, SpellSlot spellSlot)
    {
        if (game.Wizard == null || spellSlot.Spell == null)
        {
            return 0;
        }

        var (spellDamage, spellCooldown) = game.GetSpellUpgrades(spellSlot.Spell.Name);
        var (titansFury, veilOfHaste, casterPrecision, wizardsEdge) = game.GetGlobalUpgrades();
        bool isSignatureSpell = spellSlot.Spell.Name == game.Wizard.SignatureSpell;

        return CalculateDps(
            game.Wizard,
            spellSlot.Spell,
            spellDamage,
            spellCooldown,
            titansFury,
            veilOfHaste,
            casterPrecision,
            wizardsEdge,
            isSignatureSpell);
    }

    /// <summary>
    /// Calculate DPS with all modifiers
    /// Formula:
    /// Damage = (BaseDamage × (1 + TitansFury%)) × (1 + SpellDamage%)
    /// Cooldown = BaseCooldown × (1 - GlobalCD%) ÷ (1 + SpellCD%)
    /// DPS = Damage / Cooldown
    /// </summary>
    private static double CalculateDps(
        WizardBase wizard,
        SpellBase spell,
        int spellDamageBonus = 0,
        int spellCooldownReduction = 0,
        int titansFury = 0,
        int veilOfHaste = 0,
        int casterPrecision = 0,
        int wizardsEdge = 0,
        bool isSignatureSpell = false)
    {
        // Calculate base damage with Titan's Fury (percentage of base, added as flat)
        // Titan's Fury: BaseDamage × (1 + TitansFury%)
        double totalTitansFury = wizard.TitansFury + titansFury;
        double totalBaseDamage = spell.BaseDamage * (1 + totalTitansFury / 100.0);

        // Apply percentage damage modifier (wizard + spell upgrade + signature bonus if applicable)
        double totalDamagePercent = wizard.IncreaseSpellDamage + spellDamageBonus;
        if (isSignatureSpell)
        {
            totalDamagePercent += wizard.SignatureSpellDamageBonus;
        }
        double damageWithModifiers = totalBaseDamage * (1 + totalDamagePercent / 100.0);

        // Calculate critical hit factor (crit chance capped at 100%)
        double critChance = Math.Min((wizard.IncreaseCriticalChance + casterPrecision) / 100.0, 1.0);
        double critMultiplier = (wizard.IncreaseBaseCriticalDamage + wizardsEdge) / 100.0;
        double critFactor = 1 + (critChance * (critMultiplier - 1));

        // Apply crit factor to get final damage per hit
        double finalDamagePerHit = damageWithModifiers * critFactor;

        // For multi-projectile spells, multiply by projectile count
        double totalDamagePerCast = finalDamagePerHit * spell.ProjectileCount;

        // Calculate effective cooldown:
        // Cooldown = BaseCooldown × (1 - GlobalCD%) ÷ (1 + SpellCD%)
        double baseCooldownSec = spell.BaseCooldownMs / 1000.0;

        // Global CD reduction (Veil of Haste + wizard base) - additive, applied as multiplier
        double totalGlobalCdReduction = wizard.ReduceGlobalCooldown + veilOfHaste;
        double globalCdMultiplier = Math.Max(1 - totalGlobalCdReduction / 100.0, 0.1);

        // Spell CD reduction (spell upgrades + wizard base + signature bonus if applicable) - additive, applied as divisor
        double totalSpellCdReduction = wizard.ReduceSpellCooldown + spellCooldownReduction;
        if (isSignatureSpell)
        {
            totalSpellCdReduction += wizard.SignatureSpellCooldownReduction;
        }
        double spellCdDivisor = 1 + totalSpellCdReduction / 100.0;

        double effectiveCooldown = baseCooldownSec * globalCdMultiplier / spellCdDivisor;

        // DPS = Damage per cast / Cooldown between casts
        return totalDamagePerCast / effectiveCooldown;
    }

    /// <summary>
    /// Calculate what DPS would be with a hypothetical additional upgrade
    /// </summary>
    public static double CalculateDpsWithUpgrade(GameState game, SpellSlot spellSlot, Upgrade hypotheticalUpgrade)
    {
        if (game.Wizard == null || spellSlot.Spell == null)
        {
            return 0;
        }

        var (spellDamage, spellCooldown) = game.GetSpellUpgrades(spellSlot.Spell.Name);
        var (titansFury, veilOfHaste, casterPrecision, wizardsEdge) = game.GetGlobalUpgrades();
        bool isSignatureSpell = spellSlot.Spell.Name == game.Wizard.SignatureSpell;

        // Add hypothetical upgrade
        if (hypotheticalUpgrade.SpellName == spellSlot.Spell.Name)
        {
            spellDamage += hypotheticalUpgrade.IncreaseSpellDamage;
            spellCooldown += hypotheticalUpgrade.ReduceSpellCooldown;
        }
        else if (hypotheticalUpgrade.SpellName == null)
        {
            titansFury += hypotheticalUpgrade.IncreaseSpellDamage;
            veilOfHaste += hypotheticalUpgrade.ReduceSpellCooldown;
            casterPrecision += hypotheticalUpgrade.IncreaseCriticalChance;
            wizardsEdge += hypotheticalUpgrade.IncreaseBaseCriticalDamage;
        }

        return CalculateDps(
            game.Wizard,
            spellSlot.Spell,
            spellDamage,
            spellCooldown,
            titansFury,
            veilOfHaste,
            casterPrecision,
            wizardsEdge,
            isSignatureSpell);
    }
}
