namespace SpellBrigadeCalculator.Engine.Spells;

public record RuneBurst : SpellBase
{
    public override string Name => "Rune Burst";
    public override double BaseDamage => 118;
    public override int BaseCooldownMs => 687;
    public override int ProjectileCount => 4;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/6/69/Rune_burst.png/120px-Rune_burst.png";
}
