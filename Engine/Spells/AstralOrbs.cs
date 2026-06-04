namespace SpellBrigadeCalculator.Engine.Spells;

public record AstralOrbs : SpellBase
{
    public override string Name => "Astral Orbs";
    public override double BaseDamage => 170;
    public override int BaseCooldownMs => 869;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/6/6b/Astral_orbs.png/120px-Astral_orbs.png";

    public override int StartingCastSpeedBonus => 12;
}
