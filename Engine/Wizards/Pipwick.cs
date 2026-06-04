namespace SpellBrigadeCalculator.Engine.Wizards;

public record Pipwick : WizardBase
{
    public override string Name => "Pipwick";
    public override string SignatureSpell => "Impish Havoc";
    public override string AlternateStartingSpell => "Aurora Wings";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/c/c9/OriginalPipwickSkin.png/173px-OriginalPipwickSkin.png";

    public Pipwick()
    {
        IncreaseBaseCriticalDamage = 155;
        IncreaseCriticalChance = 2;
    }
}
