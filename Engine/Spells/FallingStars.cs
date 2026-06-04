namespace SpellBrigadeCalculator.Engine.Spells;

public record FallingStars : SpellBase
{
    public override string Name => "Falling Stars";
    public override double BaseDamage => 201;
    public override int BaseCooldownMs => 1145;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/9/93/Falling_stars.png/120px-Falling_stars.png";

    public override int StartingCastSpeedBonus => 12;
}
