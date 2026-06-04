namespace SpellBrigadeCalculator.Engine.Spells;

public record MagicMissile : SpellBase
{
    public override string Name => "Magic Missile";
    public override double BaseDamage => 140;
    public override int BaseCooldownMs => 2267;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/4/45/Magic_missile.png/120px-Magic_missile.png";

    public override int StartingCastSpeedBonus => 8;
}
