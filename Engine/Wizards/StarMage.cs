namespace SpellBrigadeCalculator.Engine.Wizards;

public record StarMage : WizardBase
{
    public override string Name => "Star Mage";
    public override string SignatureSpell => "Falling Stars";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/3/37/OriginalStarMageSkin.png/192px-OriginalStarMageSkin.png";

    public StarMage()
    {
        ReduceGlobalCooldown = 12;
    }
}
