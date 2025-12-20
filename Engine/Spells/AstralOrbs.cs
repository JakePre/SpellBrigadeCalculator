namespace SpellBrigadeCalculator.Engine.Spells;

public record AstralOrbs : SpellBase
{
    public override string Name => "Astral Orbs";
    public override double BaseDamage => 96;
    public override int BaseCooldownMs => 736;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/6/6b/Astral_orbs.png/120px-Astral_orbs.png";
}
