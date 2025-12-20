namespace SpellBrigadeCalculator.Engine.Wizards;

public record Campanelli : WizardBase
{
    public override string Name => "Campanelli";
    public override string SignatureSpell => "Phantom Blades";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/0/02/OriginalCampanelliSkin.png/208px-OriginalCampanelliSkin.png";

    public Campanelli()
    {
        ReduceGlobalCooldown = 12;
    }
}
