namespace SpellBrigadeCalculator.Engine.Spells;

public record AetherBeam : SpellBase
{
    public override string Name => "Aether Beam";
    public override double BaseDamage => 45.08;
    public override int BaseCooldownMs => 367;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/0/08/Aether_beam.png/120px-Aether_beam.png";
}
