namespace SpellBrigadeCalculator.Engine.Spells;

public record VengefulSprout : SpellBase
{
    public override string Name => "Vengeful Sprout";
    public override double BaseDamage => 118.5;
    public override int BaseCooldownMs => 3750;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/6/67/Vengeful_sprout.png/120px-Vengeful_sprout.png";

    public override int StartingCastSpeedBonus => 12;
}
