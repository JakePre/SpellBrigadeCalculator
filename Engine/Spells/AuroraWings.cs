namespace SpellBrigadeCalculator.Engine.Spells;

public record AuroraWings : SpellBase
{
    public override string Name => "Aurora Wings";
    public override double BaseDamage => 165;
    public override int BaseCooldownMs => 2477;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/5/5e/Aurora_wings.png/120px-Aurora_wings.png";

    public override int StartingCastSpeedBonus => 12;
}
