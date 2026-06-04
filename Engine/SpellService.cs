using SpellBrigadeCalculator.Engine.Spells;

namespace SpellBrigadeCalculator.Engine;

public class SpellService
{
    private readonly List<SpellBase> _spells =
    [
        new AetherBeam(),
        new ArcaneBroadsword(),
        new AstralOrbs(),
        new AuroraWings(),
        new BellMarch(),
        new CardinalRunes(),
        new ChthonicCharge(),
        new FallingStars(),
        new HexBomb(),
        new ImpishHavoc(),
        new MagicMissile(),
        new Moonerang(),
        new NecroWhirl(),
        new PhantomBlades(),
        new RockyRoad(),
        new ScepterMesh(),
        new SolarPulse(),
        new SpearBarrage(),
        new SwirlSickle(),
        new TalonSlash(),
        new VengefulSprout()
    ];

    public IReadOnlyList<SpellBase> GetAllSpells() => _spells.OrderBy(x => x.Name).ToList();

    public SpellBase? GetSpellByName(string name) =>
        _spells.FirstOrDefault(s => s.Name == name);
}
