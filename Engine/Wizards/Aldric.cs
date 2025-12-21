namespace SpellBrigadeCalculator.Engine.Wizards;

public record Aldric : WizardBase
{
    public override string Name => "Aldric";
    public override string SignatureSpell => "Aether Beam";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/c/c0/OriginalAldricSkin.png/173px-OriginalAldricSkin.png";

    public Aldric()
    {
        // Signature spell bonuses (only apply to Aether Beam)
        SignatureSpellDamageBonus = 8;
        SignatureSpellCooldownReduction = 4;
    }
}
