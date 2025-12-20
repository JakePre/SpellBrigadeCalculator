namespace SpellBrigadeCalculator.Engine.Wizards;

public record Ludwig : WizardBase
{
    public override string Name => "Ludwig";
    public override string SignatureSpell => "Arcane Broadsword";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/3/33/OriginalLudwigSkin.png/173px-OriginalLudwigSkin.png";

    public Ludwig()
    {
        ReduceGlobalCooldown = 8;
    }
}
