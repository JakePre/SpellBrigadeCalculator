namespace SpellBrigadeCalculator.Engine.Wizards;

public record MoonMage : WizardBase
{
    public override string Name => "Moon Mage";
    public override string SignatureSpell => "Moonerang";
    public override string AlternateStartingSpell => "Scepter Mesh";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/8/8e/DefaultMoonMageSkin.png/173px-DefaultMoonMageSkin.png";

    public MoonMage()
    {
    }
}
