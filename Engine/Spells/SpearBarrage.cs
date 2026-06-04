namespace SpellBrigadeCalculator.Engine.Spells;

public record SpearBarrage : SpellBase
{
    public override string Name => "Spear Barrage";
    public override double BaseDamage => 61;
    public override int BaseCooldownMs => 1874;
    public override int ProjectileCount => 4;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/4/4b/Spear_barrage.png/120px-Spear_barrage.png";

    public override int StartingCastSpeedBonus => 12;
}
