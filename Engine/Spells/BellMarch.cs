namespace SpellBrigadeCalculator.Engine.Spells;

public record BellMarch : SpellBase
{
    public override string Name => "Bell March";
    public override double BaseDamage => 142;
    public override int BaseCooldownMs => 4000;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/a/ae/Bell_march.png/120px-Bell_march.png";

    public override int StartingCastSpeedBonus => 12;
}
