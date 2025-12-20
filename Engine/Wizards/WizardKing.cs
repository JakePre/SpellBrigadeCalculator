namespace SpellBrigadeCalculator.Engine.Wizards;

public record WizardKing : WizardBase
{
    public override string Name => "Wizard King";
    public override string SignatureSpell => "Magic Missile";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/4/4a/OriginalWizardKingSkin.png/173px-OriginalWizardKingSkin.png";

    public WizardKing()
    {
        ReduceGlobalCooldown = 8;
        IncreaseCriticalChance = 15;
    }
}
