namespace SpellBrigadeCalculator.Engine.Wizards;

public record Hatti : WizardBase
{
    public override string Name => "Hatti";
    public override string SignatureSpell => "Rune Burst";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/e/e6/OriginalHattiSkin.png/173px-OriginalHattiSkin.png";

    public Hatti()
    {
        ReduceGlobalCooldown = 8;
        IncreaseSpellDamage = 6;
    }
}
