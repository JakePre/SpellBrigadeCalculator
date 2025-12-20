namespace SpellBrigadeCalculator.Engine.Spells;

public record NecroWhirl : SpellBase
{
    public override string Name => "Necro Whirl";
    public override double BaseDamage => 40;
    public override int BaseCooldownMs => 4000;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/b/bc/Necro_whirl.png/120px-Necro_whirl.png";
}
