namespace SpellBrigadeCalculator.Engine.Spells;

public record ScepterMesh : SpellBase
{
    public override string Name => "Scepter Mesh";
    public override double BaseDamage => 47;
    public override int BaseCooldownMs => 12500;
    public override int ProjectileCount => 3;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/0/0a/Scepter_mesh.png/120px-Scepter_mesh.png";

    public override int StartingCastSpeedBonus => 12;
}
