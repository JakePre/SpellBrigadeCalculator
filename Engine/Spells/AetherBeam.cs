namespace SpellBrigadeCalculator.Engine.Spells;

public record AetherBeam : SpellBase
{
    public override string Name => "Aether Beam";
    public override double BaseDamage => 104;
    public override int BaseCooldownMs => 526;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/0/08/Aether_beam.png/120px-Aether_beam.png";

    public override int StartingDamageBonus => 8;
    public override int StartingCastSpeedBonus => 4;
}
