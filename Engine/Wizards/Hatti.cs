namespace SpellBrigadeCalculator.Engine.Wizards;

public record Hatti : WizardBase
{
    public override string Name => "Hatti";
    public override string SignatureSpell => "Cardinal Runes";
    public override string AlternateStartingSpell => "Swirl Sickle";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/e/e6/OriginalHattiSkin.png/173px-OriginalHattiSkin.png";

    public Hatti()
    {
        IncreaseSpellDamage = 6;
        SignatureSpellCastSpeedBonus = 8;
    }
}
