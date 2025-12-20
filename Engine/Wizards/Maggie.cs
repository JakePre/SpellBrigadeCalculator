namespace SpellBrigadeCalculator.Engine.Wizards;

public record Maggie : WizardBase
{
    public override string Name => "Maggie";
    public override string SignatureSpell => "Aurora Wings";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/c/c2/Original_Maggie.png/173px-Original_Maggie.png";

    public Maggie()
    {
        ReduceGlobalCooldown = 12;
    }
}
