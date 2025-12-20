using SpellBrigadeCalculator.Engine.Spells;

namespace SpellBrigadeCalculator.Engine;

public class SpellService
{
    private readonly List<SpellBase> _spells =
    [
        new MagicMissile(),
        new AstralOrbs(),
        new Moonerang(),
        new RockyRoad(),
        new SolarPulse(),
        new ArcaneBroadsword(),
        new PhantomBlades(),
        new RuneBurst(),
        new FallingStars(),
        new AetherBeam(),
        new AuroraWings(),
        new VengefulSprout(),
        new NecroWhirl(),
        new HexBomb(),
        new ScepterMesh(),
        new ChthonicCharge(),
        new TalonSlash()
    ];

    public IReadOnlyList<SpellBase> GetAllSpells() => _spells;

    public SpellBase? GetSpellByName(string name) =>
        _spells.FirstOrDefault(s => s.Name == name);
}
