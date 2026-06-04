namespace SpellBrigadeCalculator.Engine.Wizards;

public record Knelly : WizardBase
{
    public override string Name => "Knelly";
    public override string SignatureSpell => "Bell March";
    public override string AlternateStartingSpell => "Rocky Road";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/8/87/OriginalKnellySkin.png/173px-OriginalKnellySkin.png";

    public Knelly()
    {
        SignatureSpellCastSpeedBonus = 12;
    }
}
