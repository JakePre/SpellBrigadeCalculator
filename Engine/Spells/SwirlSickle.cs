namespace SpellBrigadeCalculator.Engine.Spells;

public record SwirlSickle : SpellBase
{
    public override string Name => "Swirl Sickle";
    public override double BaseDamage => 140;
    public override int BaseCooldownMs => 5000;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/c/cd/Swirl_sickle.png/120px-Swirl_sickle.png";
}
