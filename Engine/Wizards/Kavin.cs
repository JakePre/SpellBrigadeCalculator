namespace SpellBrigadeCalculator.Engine.Wizards;

public record Kavin : WizardBase
{
    public override string Name => "Kavin";
    public override string SignatureSpell => "Rocky Road";
    public override string AlternateStartingSpell => "Necro Whirl";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/c/c2/DefaultKavinSkin.png/192px-DefaultKavinSkin.png";

    public Kavin()
    {
        SignatureSpellDamageBonus = 8;
    }
}
