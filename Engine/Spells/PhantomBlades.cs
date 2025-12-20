namespace SpellBrigadeCalculator.Engine.Spells;

public record PhantomBlades : SpellBase
{
    public override string Name => "Phantom Blades";
    public override double BaseDamage => 300;
    public override int BaseCooldownMs => 2061;
    public override int ProjectileCount => 3;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/1/16/Phantom_blades.png/120px-Phantom_blades.png";
}
