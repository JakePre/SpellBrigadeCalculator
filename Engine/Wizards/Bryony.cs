namespace SpellBrigadeCalculator.Engine.Wizards;

public record Bryony : WizardBase
{
    public override string Name => "Bryony";
    public override string SignatureSpell => "Vengeful Sprout";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/1/1e/Original_Bryony.png/173px-Original_Bryony.png";

    public Bryony()
    {
        IncreaseSpellDamage = 12;
    }
}
