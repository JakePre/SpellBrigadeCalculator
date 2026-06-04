using SpellBrigadeCalculator.Engine.Spells;
using SpellBrigadeCalculator.Engine.Wizards;

namespace SpellBrigadeCalculator.Engine;

public class SpellDpsDetails
{
    public double BaseDamage { get; set; }
    public double DamagePerHitNormal { get; set; }
    public double DamagePerHitCrit { get; set; }
    public double DamagePerHitAverage { get; set; }
    public double TotalDamagePerCast { get; set; }
    public double EffectiveCooldown { get; set; }
    public double AttacksPerSecond { get; set; }
    public double Dps { get; set; }
    public int ProjectileCount { get; set; }
}

public static class DpsEngine
{
    /// <summary>
    /// Calculate detailed stats and DPS for a spell slot
    /// </summary>
    public static SpellDpsDetails CalculateDpsDetails(GameState game, SpellSlot spellSlot)
    {
        var details = new SpellDpsDetails();
        if (game.Wizard == null || spellSlot.Spell == null)
        {
            return details;
        }

        var (spellDamage, spellCastSpeed) = game.GetSpellUpgrades(spellSlot.Spell.Name);
        var (titansFury, veilOfHaste, casterPrecision, wizardsEdge) = game.GetGlobalUpgrades();
        bool isSignatureSpell = spellSlot == game.Spell1;

        var wizard = game.Wizard;
        var spell = spellSlot.Spell;

        details.ProjectileCount = spell.ProjectileCount;
        details.BaseDamage = spell.BaseDamage;

        // Damage percentage modifiers (additive)
        double totalDamagePercent = wizard.IncreaseSpellDamage + spellDamage + titansFury + wizard.TitansFury + game.EnchantmentDamagePercent;
        if (isSignatureSpell)
        {
            totalDamagePercent += spell.StartingDamageBonus;
        }
        details.DamagePerHitNormal = spell.BaseDamage * (1 + totalDamagePercent / 100.0);

        // Crit Chance and Multiplier
        double critChance = Math.Min((wizard.IncreaseCriticalChance + casterPrecision + game.EnchantmentCritChancePercent) / 100.0, 1.0);
        double critMultiplier = (wizard.IncreaseBaseCriticalDamage + wizardsEdge + game.EnchantmentCritDamagePercent) / 100.0;
        
        details.DamagePerHitCrit = details.DamagePerHitNormal * critMultiplier;

        double critFactor = 1 + (critChance * (critMultiplier - 1.0));
        details.DamagePerHitAverage = details.DamagePerHitNormal * critFactor;

        details.TotalDamagePerCast = details.DamagePerHitAverage * spell.ProjectileCount;

        // Effective Cooldown / Cast Speed (additive)
        double baseCooldownSec = spell.BaseCooldownMs / 1000.0;
        double totalSpellCastSpeed = wizard.IncreaseCastSpeed + spellCastSpeed + veilOfHaste + game.EnchantmentCastSpeedPercent + wizard.ReduceGlobalCooldown;
        if (isSignatureSpell)
        {
            totalSpellCastSpeed += spell.StartingCastSpeedBonus;
        }
        details.EffectiveCooldown = baseCooldownSec / (1 + totalSpellCastSpeed / 100.0);
        details.AttacksPerSecond = 1.0 / details.EffectiveCooldown;

        details.Dps = details.TotalDamagePerCast / details.EffectiveCooldown;

        return details;
    }

    /// <summary>
    /// Calculate DPS for a spell slot using game state and upgrade history
    /// </summary>
    public static double CalculateDps(GameState game, SpellSlot spellSlot)
    {
        return CalculateDpsDetails(game, spellSlot).Dps;
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
