namespace SpellBrigadeCalculator.Engine.Wizards;

public record Reginald : WizardBase
{
    public override string Name => "Reginald";
    public override string SignatureSpell => "Astral Orbs";
    public override string AlternateStartingSpell => "Bell March";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/5/5b/Original_Reginald.png/173px-Original_Reginald.png";

    public Reginald()
    {
        SignatureSpellCastSpeedBonus = 12;
    }
}
