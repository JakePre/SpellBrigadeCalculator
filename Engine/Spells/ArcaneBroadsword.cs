namespace SpellBrigadeCalculator.Engine.Spells;

public record ArcaneBroadsword : SpellBase
{
    public override string Name => "Arcane Broadsword";
    public override double BaseDamage => 128;
    public override int BaseCooldownMs => 5102;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/a/a9/Arcane_broadsword.png/120px-Arcane_broadsword.png";

    public override int StartingCastSpeedBonus => 8;
}
