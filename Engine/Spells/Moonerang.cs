namespace SpellBrigadeCalculator.Engine.Spells;

public record Moonerang : SpellBase
{
    public override string Name => "Moonerang";
    public override double BaseDamage => 57;
    public override int BaseCooldownMs => 1886;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/e/e8/Moonerang.png/120px-Moonerang.png";
}
