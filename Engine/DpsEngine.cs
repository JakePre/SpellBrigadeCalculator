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

        var (spellDamage, spellCastSpeed) = game.GetSpellUpgrades(spellSlot.Spell.Name);
        var (titansFury, veilOfHaste, casterPrecision, wizardsEdge) = game.GetGlobalUpgrades();
        bool isSignatureSpell = spellSlot == game.Spell1;

        return CalculateDps(
            game.Wizard,
            spellSlot.Spell,
            spellDamage,
            spellCastSpeed,
            titansFury,
            veilOfHaste,
            casterPrecision,
            wizardsEdge,
            isSignatureSpell,
            game.EnchantmentDamagePercent,
            game.EnchantmentCastSpeedPercent,
            game.EnchantmentCritChancePercent,
            game.EnchantmentCritDamagePercent);
    }

    /// <summary>
    /// Calculate DPS with all modifiers
    /// Formula:
    /// Damage = (BaseDamage × (1 + TitansFury%)) × (1 + SpellDamage%)
    /// Cooldown = BaseCooldown × (1 - GlobalCD%) ÷ (1 + CastSpeed%)
    /// DPS = Damage / Cooldown
    /// </summary>
    private static double CalculateDps(
        WizardBase wizard,
        SpellBase spell,
        int spellDamageBonus = 0,
        int spellCastSpeedBonus = 0,
        int titansFury = 0,
        int veilOfHaste = 0,
        int casterPrecision = 0,
        int wizardsEdge = 0,
        bool isSignatureSpell = false,
        int enchantmentDamage = 0,
        int enchantmentCastSpeed = 0,
        int enchantmentCritChance = 0,
        int enchantmentCritDamage = 0)
    {
        // Apply percentage damage modifier (wizard + spell upgrade + starting signature bonus + Titan's Fury + enchantment)
        // All damage modifiers are additive percentages in the game's stats engine
        double totalDamagePercent = wizard.IncreaseSpellDamage + spellDamageBonus + titansFury + wizard.TitansFury + enchantmentDamage;
        if (isSignatureSpell)
        {
            totalDamagePercent += spell.StartingDamageBonus;
        }
        double damageWithModifiers = spell.BaseDamage * (1 + totalDamagePercent / 100.0);

        // Calculate critical hit factor (including enchantment bonuses)
        // Formula: 1 + CritChance × (CritMultiplier - 1)
        double critChance = Math.Min((wizard.IncreaseCriticalChance + casterPrecision + enchantmentCritChance) / 100.0, 1.0);
        double critMultiplier = (wizard.IncreaseBaseCriticalDamage + wizardsEdge + enchantmentCritDamage) / 100.0;
        double critFactor = 1 + (critChance * (critMultiplier - 1.0));

        // Apply crit factor to get final damage per hit
        double finalDamagePerHit = damageWithModifiers * critFactor;

        // For multi-projectile spells, multiply by projectile count
        double totalDamagePerCast = finalDamagePerHit * spell.ProjectileCount;

        // Calculate effective cooldown:
        // Cooldown = BaseCooldown ÷ (1 + totalCastSpeed%)
        // Global and spell-specific cast speeds are additive percentages in the game's stats engine (FireRate)
        double baseCooldownSec = spell.BaseCooldownMs / 1000.0;

        double totalSpellCastSpeed = wizard.IncreaseCastSpeed + spellCastSpeedBonus + veilOfHaste + enchantmentCastSpeed + wizard.ReduceGlobalCooldown;
        if (isSignatureSpell)
        {
            totalSpellCastSpeed += spell.StartingCastSpeedBonus;
        }
        double effectiveCooldown = baseCooldownSec / (1 + totalSpellCastSpeed / 100.0);

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

        var (spellDamage, spellCastSpeed) = game.GetSpellUpgrades(spellSlot.Spell.Name);
        var (titansFury, veilOfHaste, casterPrecision, wizardsEdge) = game.GetGlobalUpgrades();
        bool isSignatureSpell = spellSlot == game.Spell1;

        // Add hypothetical upgrade
        if (hypotheticalUpgrade.SpellName == spellSlot.Spell.Name)
        {
            spellDamage += hypotheticalUpgrade.IncreaseSpellDamage;
            spellCastSpeed += hypotheticalUpgrade.IncreaseCastSpeed;
        }
        else if (hypotheticalUpgrade.SpellName == null)
        {
            titansFury += hypotheticalUpgrade.IncreaseSpellDamage;
            veilOfHaste += hypotheticalUpgrade.IncreaseCastSpeed;
            casterPrecision += hypotheticalUpgrade.IncreaseCriticalChance;
            wizardsEdge += hypotheticalUpgrade.IncreaseBaseCriticalDamage;
        }

        return CalculateDps(
            game.Wizard,
            spellSlot.Spell,
            spellDamage,
            spellCastSpeed,
            titansFury,
            veilOfHaste,
            casterPrecision,
            wizardsEdge,
            isSignatureSpell,
            game.EnchantmentDamagePercent,
            game.EnchantmentCastSpeedPercent,
            game.EnchantmentCritChancePercent,
            game.EnchantmentCritDamagePercent);
    }
}
