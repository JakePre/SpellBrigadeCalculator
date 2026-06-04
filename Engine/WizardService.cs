using SpellBrigadeCalculator.Engine.Wizards;

namespace SpellBrigadeCalculator.Engine;

public class WizardService
{
    private readonly List<WizardBase> _wizards =
    [
        new Aldric(),
        new Balthazar(),
        new Bryony(),
        new Campanelli(),
        new Hatti(),
        new Kavin(),
        new Knelly(),
        new Ludwig(),
        new Maggie(),
        new MoonMage(),
        new Pipwick(),
        new Reginald(),
        new StarMage(),
        new SunMage(),
        new WizardKing()
    ];

    public IReadOnlyList<WizardBase> GetAllWizards() => _wizards.OrderBy(x => x.Name).ToList();

    public WizardBase? GetWizardByName(string name) =>
        _wizards.FirstOrDefault(w => w.Name == name);
}
