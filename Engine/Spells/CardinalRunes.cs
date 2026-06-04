namespace SpellBrigadeCalculator.Engine.Spells;

public record CardinalRunes : SpellBase
{
    public override string Name => "Cardinal Runes";
    public override double BaseDamage => 118;
    public override int BaseCooldownMs => 687;
    public override int ProjectileCount => 4;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/6/69/Rune_burst.png/120px-Rune_burst.png";

    public override int StartingCastSpeedBonus => 8;
}
