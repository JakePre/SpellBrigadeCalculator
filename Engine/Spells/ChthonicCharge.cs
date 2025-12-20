namespace SpellBrigadeCalculator.Engine.Spells;

public record ChthonicCharge : SpellBase
{
    public override string Name => "Chthonic Charge";
    public override double BaseDamage => 134;
    public override int BaseCooldownMs => 1670;
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/c/cb/Chthonic_charge.png/120px-Chthonic_charge.png";
}
