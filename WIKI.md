# The Spell Brigade - Game Mechanics Wiki

This wiki documents the core mechanics, formulas, and math of **The Spell Brigade**, verified directly from the game's assembly (`GameAssembly.dll` and IL2CPP metadata).

---

## 1. Damage & DPS Calculations

All stats in the game scale through a structured stats engine. The main categories of calculations are detailed below:

### Additive Damage scaling (PercentAdd)
The game uses a `StatCalculationType.PercentAdd` calculation for all percentage-based damage increases. This means all percentage modifiers stack **additively** rather than multiplicatively.

$$\text{Normal Damage} = \text{Base Damage} \times \left(1 + \frac{\text{Sum of Damage Modifiers}}{100}\right)$$

Where **Sum of Damage Modifiers** is the sum of:
*   **Wizard Passive**: e.g., Aldric's $+10\%$ spell damage
*   **Spell Damage Upgrades**: Shop upgrades chosen on level-up
*   **Titan's Fury**: Global base damage card
*   **Signature Spell Bonus**: Starting signature spell damage bonus (if applicable)
*   **Enchantment Damage**: Additional enchantment bonuses

> [!NOTE]
> Since these stack additively, if you have $+10\%$ spell damage from passive, $+15\%$ from a spell upgrade, and $+20\%$ from Titan's Fury, the total damage increase is exactly $+45\%$ (a multiplier of $1.45\times$).

---

### Critical Hits & Multipliers
Critical hits scale based on your Critical Chance and Critical Damage.
*   **Critical Chance**: Capped at $100\%$ ($1.0$).
*   **Critical Damage Multiplier**: Calculates as:
    $$\text{Crit Multiplier} = \frac{\text{Base Critical Damage} + \text{Wizard's Edge} + \text{Enchantment Crit Damage}}{100}$$
    *(Note: Base Critical Damage is $150\%$, yielding a baseline multiplier of $1.5\times$.)*
*   **Average Damage Factor (Crit Factor)**: The expected average damage multiplier over time:
    $$\text{Crit Factor} = 1 + \text{Crit Chance} \times (\text{Crit Multiplier} - 1.0)$$
*   **Total Expected Damage Per Cast**:
    $$\text{Total Damage Per Cast} = \text{Normal Damage} \times \text{Crit Factor} \times \text{Projectile Count}$$

---

### Cast Speed & Cooldown
Similar to damage, global and spell-specific cast speed/cooldown reduction modifiers stack **additively** in the stats engine under the `FireRate` category:

$$\text{Effective Cooldown (sec)} = \frac{\text{Base Cooldown (ms)}}{1000} \div \left(1 + \frac{\text{Total Cast Speed}}{100}\right)$$

Where **Total Cast Speed** is the sum of:
*   Wizard passive cast speed
*   Spell cast speed upgrades
*   **Veil of Haste** (Global CD card)
*   Enchantment cast speed bonuses
*   Signature spell cast speed bonuses (if applicable)

**Attacks per Second (APS)** is calculated as:
$$\text{Attacks Per Second} = \frac{1}{\text{Effective Cooldown}}$$

**Final Spell DPS**:
$$\text{DPS} = \frac{\text{Total Damage Per Cast}}{\text{Effective Cooldown}}$$

---

## 2. Armor & Damage Reduction

The player's survivability math is split into flat armor reductions and percentage-based defensive upgrades.

### Flat Armor Reduction
Incoming damage from enemies is reduced by a flat subtraction based on the player's Armor value.

$$\text{Damage After Armor} = \max(0.0, \text{Incoming Damage} - \text{Player Armor})$$

*   The player's flat armor value is fetched from player stats using `stats.GetCharacterValue(StatType.Armor)` (enum index `14`).
*   **Example**: If an enemy strike deals $15$ damage and you have $6$ Armor, you take exactly $9$ damage. If you have $20$ Armor, you take $0$ damage.

---

## 3. Luck & Rarity Upgrade Odds

Luck affects the probabilities of rolling higher-tier card upgrades in the level-up store.

### Baseline Rarity Odds
At $0$ Luck, the baseline probabilities for card rarity pools are:
*   **Common**: $71\%$
*   **Rare**: $25\%$
*   **Epic**: $4\%$
*   **Legendary**: $0\%$

### Luck Scaling Formula
Each point of Luck ($+1$) shifts the card selection probabilities by shifting the weight of each rarity pool:
*   **Common**: Decreases by **$-1.2\%$** per point of Luck
*   **Rare**: Increases by **$+0.5\%$** per point of Luck
*   **Epic**: Increases by **$+0.4\%$** per point of Luck
*   **Legendary**: Increases by **$+0.3\%$** per point of Luck

$$\begin{aligned}
P(\text{Common}) &= \max(0\%, 71\% - 1.2\% \times \text{Luck}) \\
P(\text{Rare}) &= \max(0\%, 25\% + 0.5\% \times \text{Luck}) \\
P(\text{Epic}) &= \max(0\%, 4\% + 0.4\% \times \text{Luck}) \\
P(\text{Legendary}) &= \max(0\%, 0\% + 0.3\% \times \text{Luck})
\end{aligned}$$

> [!TIP]
> Since the sum of shifts $(-1.2\% + 0.5\% + 0.4\% + 0.3\%)$ is exactly $0\%$, the total probability always sums to $100\%$ for any Luck value, maintaining mathematical consistency.
> For example, at $20$ Luck:
> *   $P(\text{Common}) = 71\% - 24\% = 47\%$
> *   $P(\text{Rare}) = 25\% + 10\% = 35\%$
> *   $P(\text{Epic}) = 4\% + 8\% = 12\%$
> *   $P(\text{Legendary}) = 0\% + 6\% = 6\%$

### Luck Soft Cap
*   The game restricts Luck upgrades in the shop based on a threshold.
*   Once a player reaches **$25$ Luck or higher**, Luck upgrades are excluded from the level-up store card pool.
*   Thus, the effective **soft cap** for Luck upgrades is **$24$ Luck**. Once you hit $25$, you can no longer draft new Luck card upgrades from the store.
