namespace SpellBrigadeCalculator.Engine.Spells;

public record TalonSlash : SpellBase
{
    public override string Name => "Talon Slash";
    public override double BaseDamage => 296;
    public override int BaseCooldownMs => 1333;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/0/0a/Talon_slash.png/120px-Talon_slash.png";

    public override int StartingDamageBonus => 12;
}
