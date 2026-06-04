namespace SpellBrigadeCalculator.Engine.Wizards;

public record Balthazar : WizardBase
{
    public override string Name => "Balthazar";
    public override string SignatureSpell => "Talon Slash";
    public override string AlternateStartingSpell => "Aether Beam";
    public override string ImagePath => "https://thespellbrigade.wiki.gg/images/thumb/3/3a/OriginalBalthazarSkin.png/173px-OriginalBalthazarSkin.png";

    public Balthazar()
    {
        SignatureSpellDamageBonus = 12;
    }
}
