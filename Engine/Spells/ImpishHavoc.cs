namespace SpellBrigadeCalculator.Engine.Spells;

public record ImpishHavoc : SpellBase
{
    public override string Name => "Impish Havoc";
    public override double BaseDamage => 107.19;
    public override int BaseCooldownMs => 4701;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/e/e0/Impish_havoc.png/120px-Impish_havoc.png";
}
