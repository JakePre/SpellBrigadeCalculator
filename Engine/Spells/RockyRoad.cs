namespace SpellBrigadeCalculator.Engine.Spells;

public record RockyRoad : SpellBase
{
    public override string Name => "Rocky Road";
    public override double BaseDamage => 33.81;
    public override int BaseCooldownMs => 500;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/f/fe/Rocky_road.png/120px-Rocky_road.png";

    public override int StartingDamageBonus => 8;
}
