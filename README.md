# Spell Brigade Calculator

A DPS (Damage Per Second) calculator for the game **The Spell Brigade**. This tool helps players optimize their upgrade choices by calculating the expected DPS increase for different upgrade combinations.

## Features

- Select from all available wizards with their unique signature spells and stats
- Configure up to 4 spell slots with spell-specific upgrades
- Calculate DPS impact for global upgrades:
  - **Titan's Fury** - Base damage increase
  - **Veil of Haste** - Cooldown reduction
  - **Caster Precision** - Critical chance
  - **Wizard's Edge** - Critical damage
- Track upgrade history with undo functionality
- Real-time DPS calculations based on the game's actual formulas

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Build & Run

```bash
# Clone the repository
git clone https://github.com/Tobbe1974/SpellBrigadeCalculator.git
cd SpellBrigadeCalculator

# Build
dotnet build

# Run
dotnet run
```

The application will start at `https://localhost:7092` or `http://localhost:5037`.

## DPS Formula

The calculator uses the official DPS formula from the game:

```
DPS = (BaseDmg + TitansFury) × (1 + IncSpellDmg/100) × (1 + CritChance/100 × CritMult/100)
      ─────────────────────────────────────────────────────────────────────────────────────
              BaseCDms × (1 - RedGlobalCD/100) / (1 + RedSpellCD/100) × 1/1000
```

## Resources

- [The Spell Brigade Wiki](https://thespellbrigade.wiki.gg/)
- [Steam Store Page](https://store.steampowered.com/app/2003610/The_Spell_Brigade/)

## License

MIT
