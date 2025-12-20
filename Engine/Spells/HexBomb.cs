namespace SpellBrigadeCalculator.Engine.Spells;

public record HexBomb : SpellBase
{
    public override string Name => "Hex Bomb";
    public override double BaseDamage => 149;
    public override int BaseCooldownMs => 3636;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/5/5d/Hex_bomb.png/120px-Hex_bomb.png";
}
