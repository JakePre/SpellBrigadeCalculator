namespace SpellBrigadeCalculator.Engine.Spells;

public record SolarPulse : SpellBase
{
    public override string Name => "Solar Pulse";
    public override double BaseDamage => 32;
    public override int BaseCooldownMs => 333;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/c/c5/Solar_pulse.png/120px-Solar_pulse.png";
}
