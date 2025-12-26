using SpellBrigadeCalculator.Engine.Wizards;

namespace SpellBrigadeCalculator.Engine;

public class WizardService
{
    private readonly List<WizardBase> _wizards =
    [
        new Reginald(),
        new MoonMage(),
        new Kavin(),
        new SunMage(),
        new Ludwig(),
        new Campanelli(),
        new Hatti(),
        new WizardKing(),
        new StarMage(),
        new Aldric(),
        new Maggie(),
        new Bryony()
    ];

    public IReadOnlyList<WizardBase> GetAllWizards() => _wizards.OrderBy(x => x.Name).ToList();

    public WizardBase? GetWizardByName(string name) =>
        _wizards.FirstOrDefault(w => w.Name == name);
}
