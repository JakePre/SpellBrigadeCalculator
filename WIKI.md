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

---

## 4. Elements & Status Effects

Spells can be infused with elements. When a spell hits a target, it delegates the status effect application to the elements engine. The damage calculations, duration, tick rates, and scaling behavior for all 20 elements (5 Core, 5 Mastered, and 10 Hybrid) are detailed below.

### Core Elements (Single Infusion)

Core elements are applied when a spell has a single element infusion:

| Element | Status Effect | Key Stat | Value | Formula & Behavior |
| :--- | :--- | :--- | :--- | :--- |
| **Fire** | **Burn** | `BurnDamageFactor`<br>`NumberOfBurns`<br>`SecondsBetweenBurns` | `0.33` ($33\%$)<br>`3`<br>`0.5` sec | Deals periodic damage over time.<br>$$\text{Tick Damage} = \text{Base Damage} \times 0.33$$<br>$$\text{Total Burn Damage} = \text{Tick Damage} \times 3 = 0.99 \times \text{Base Damage}$$<br>Total duration is $1.5$ sec. Burn ticks can critically strike. |
| **Ice** | **Chill** | `ChillDuration`<br>`ChillDamageFactor` | `4.0` sec<br>`0.001` ($0.1\%$) | Slows enemy movement speed. Speed slow factor clamps target speed between $0.2\times$ and $0.72\times$. Deals minor $0.1\%$ tick damage. |
| **Acid** | **Poison** | `AcidDuration`<br>`ResistanceReduction` | `3.0` sec<br>`0.5` ($50\%$) | Reduces the target's elemental resistance, causing them to take increased damage:<br>$$\text{Target Resistance} = \text{Base Resistance} - 0.5$$<br>$$\text{Modified Damage} = \text{Raw Damage} \times (1 - \text{Target Resistance})$$<br>*(At $0\%$ base resistance, this represents a $1.5\times$ damage multiplier.)* |
| **Lightning** | **Chain** | `LightningDamageFactor`<br>`NumberOfLightningRicochets` | `0.5` ($50\%$)<br>`2` bounces | Lightning chains to nearby targets.<br>$$\text{Chain Hit Damage} = \text{Base Damage} \times 0.5$$<br>Hits up to 3 targets total (1 initial + 2 chains), representing up to $2.0\times$ total damage output. |
| **Dark** | **Corruption** | `CorruptionDamageFactor`<br>`CorruptionElementBehaviour` | `1.5` ($150\%$)<br>`1` (`UseNoElement`) | Applies corruption value. Upon reaching $1.0$ corruption, target state shifts to `AttackOtherEnemies` (charmed/mind-controlled). Deals explosion damage when triggered:<br>$$\text{Corruption Damage} = \text{Base Damage} \times 1.5$$ |

---

### Mastered Elements (Double Infusion)

Mastered elements are applied when a spell is infused with the same element twice:

| Element | Status Effect | Key Stat | Value | Formula & Behavior |
| :--- | :--- | :--- | :--- | :--- |
| **Fire Double** | **Enhanced Burn** | `BurnDamageFactor`<br>`NumberOfBurns`<br>`SecondsBetweenBurns` | `0.45` ($45\%$)<br>`5`<br>`0.5` sec | Deals higher periodic damage over a longer duration.<br>$$\text{Tick Damage} = \text{Base Damage} \times 0.45$$<br>$$\text{Total Burn Damage} = \text{Tick Damage} \times 5 = 2.25 \times \text{Base Damage}$$<br>Total duration is $2.5$ sec. Burn ticks can critically strike. |
| **Ice Double** | **Freeze** | `ChillDuration`<br>`ChillDamageFactor`<br>`FreezeChanceBase`<br>`FreezeDuration`<br>`FreezeImmunityDuration` | `4.0` sec<br>`0.001` ($0.1\%$)<br>`0.4` ($40\%$)<br>`2.0` sec<br>`2.0` sec | Chills the target. Has a $40\%$ chance to completely freeze a chilled target for $2.0$ seconds. Target gains freeze immunity for $2.0$ seconds after thawing. |
| **Acid Double** | **Weaken** | `AcidDuration`<br>`ResistanceReduction`<br>`DamageReduction` | `3.0` sec<br>`0.5` ($50\%$)<br>`0.1` ($10\%$) | Reduces target resistance by $50\%$ points. Also applies Weaken, reducing the damage dealt by the afflicted enemies by $10\%$ (`DamageReduction` = $0.1$). |
| **Lightning Double**| **Double Strike** | `LightningDamageFactor`<br>`NumberOfLightningRicochets`<br>`ExtraLightningStrikeDamageFactor` | `0.5` ($50\%$)<br>`2`<br>`1.45` ($145\%$) | Chains lightning normally (up to 2 bounces). Additionally, triggers an extra lightning strike dealing $145\%$ base damage:<br>$$\text{Extra Strike Damage} = \text{Base Damage} \times 1.45$$ |
| **Dark Double** | **Chain Corruption**| `CorruptionDamageFactor`<br>`CorruptionElementBehaviour`<br>`CorruptedEnemyCorruptionDamageFactor`| `2.625` ($262.5\%$)<br>`2` (`UseDarkElement`)`1.0` ($100\%$) | Charmed corrupted target spreads corruption to nearby enemies. Deals increased corruption damage:<br>$$\text{Corruption Damage} = \text{Base Damage} \times 2.625$$<br>Corrupted target attacks or explosions deal $100\%$ base damage to nearby targets. |

---

### Hybrid Elements (Dual Combinations)

Formed by combining two different core elements:

| Combination | Key Stat | Value | Formula & Behavior |
| :--- | :--- | :--- | :--- |
| **Fire + Ice** | `BurnDamageFactor`<br>`NumberOfBurns`<br>`SecondsBetweenBurns`<br>`FreezeDamageFactor`<br>`FreezeDuration`<br>`KnockbackForce`<br>`KnockbackDuration` | `0.33` ($33\%$)<br>`3`<br>`0.5` sec<br>`0.001` ($0.1\%$)<br>`4.0` sec<br>`8.0`<br>`0.4` sec | Combines burn damage ticks ($33\%$ base damage) with a $4.0$-sec freeze. Additionally triggers a knockback explosion with a force of $8.0$ lasting $0.4$ seconds. |
| **Fire + Lightning** | `LightningDamageFactor`<br>`NumberOfLightningRicochets`<br>`BurnDamageFactor`<br>`NumberOfBurns`<br>`SecondsBetweenBurns`<br>`RicochetBurnDamageFactor` | `0.5` ($50\%$)<br>`2` bounces<br>`0.33` ($33\%$)<br>`3`<br>`0.5` sec<br>`0.25` ($25\%$) | Chains lightning normally. Bounced hits apply a burn status effect. Bounced ticks deal $25\%$ base damage per tick instead of the main $33\%$:<br>$$\text{Ricochet Tick Damage} = \text{Base Damage} \times 0.25$$ |
| **Acid + Fire** | `BurnDamageFactor`<br>`NumberOfBurns`<br>`SecondsBetweenBurns`<br>`AcidDuration`<br>`ResistanceReduction`<br>`AcidFireSplashDamageFactor`<br>`AcidFireSplashRadius` | `0.33` ($33\%$)<br>`3`<br>`0.5` sec<br>`3.0` sec<br>`0.5` ($50\%$)<br>`0.25` ($25\%$)<br>`2.0` m | Applies burn ticks and acid resistance reduction ($50\%$ points). When targets die or burn, they explode/splash acid-fire dealing $25\%$ base damage in a $2.0$-meter radius:<br>$$\text{Splash Damage} = \text{Base Damage} \times 0.25$$ |
| **Acid + Lightning** | `AcidDuration`<br>`ResistanceReduction`<br>`LightningDamageFactor`<br>`NumberOfLightningRicochets`<br>`RicochetResistanceReduction` | `3.0` sec<br>`0.5` ($50\%$)<br>`0.5` ($50\%$)<br>`2` bounces<br>`0.25` ($25\%$) | Chains lightning normally. Main hits apply $50\%$ points resistance reduction, while ricocheted chain hits apply a smaller resistance reduction of $25\%$ points (`RicochetResistanceReduction` = $0.25$). |
| **Acid + Ice** | `AcidDuration`<br>`ResistanceReduction`<br>`FreezeDamageFactor`<br>`ChillDuration`<br>`KnockbackForce`<br>`KnockbackRadius`<br>`KnockbackDuration` | `3.0` sec<br>`0.5` ($50\%$)<br>`0.0`<br>`4.0` sec<br>`8.0`<br>`2.0` m<br>`0.4` sec | Applies acid reduction ($50\%$) and chill slow ($4.0$ sec). Hits trigger a knockback wave with a force of $8.0$ and radius of $2.0$ meters lasting $0.4$ seconds. |
| **Ice + Lightning** | `LightningDamageFactor`<br>`NumberOfLightningRicochets`<br>`ChillDuration`<br>`ChillDamageFactor`<br>`RicochetChillFactor` | `0.5` ($50\%$)<br>`2` bounces<br>`4.0` sec<br>`0.001` ($0.1\%$)<br>`0.25` ($25\%$) | Chains lightning normally. Ricochet chain hits propagate a chill slow with $25\%$ effectiveness/duration compared to the main hits. |
| **Dark + Fire** | `CorruptionDamageFactor`<br>`BurnDamageFactor`<br>`NumberOfBurns`<br>`SecondsBetweenBurns`<br>`CorruptionElementBehaviour` | `1.5` ($150\%$)<br>`0.33` ($33\%$)<br>`3`<br>`0.5` sec<br>`0` (`UseCoreElement`) | Charmed targets apply core element burn ticks. Deals corruption explosion damage ($150\%$ base damage). |
| **Dark + Lightning** | `CorruptionDamageFactor`<br>`LightningDamageFactor`<br>`NumberOfLightningRicochets`<br>`CorruptionElementBehaviour`<br>`RicochetCorruptionFactor` | `1.5` ($150\%$)<br>`0.5` ($50\%$)<br>`2` bounces<br>`1` (`UseNoElement`)`0.25` ($25\%$) | Chains lightning. Bounced chain hits build up corruption at $25\%$ of the base rate (`RicochetCorruptionFactor` = $0.25$). |
| **Acid + Dark** | `CorruptionDamageFactor`<br>`AcidDuration`<br>`ResistanceReduction`<br>`CorruptionElementBehaviour` | `1.5` ($150\%$)<br>`3.0` sec<br>`0.5` ($50\%$)<br>`0` (`UseCoreElement`) | Applies acid resistance reduction ($50\%$ points) and corruption build up. Charmed targets apply/interact with core elements. |
| **Dark + Ice** | `CorruptionDamageFactor`<br>`CorruptionElementBehaviour`<br>`ChillDamageFactor`<br>`ChillDuration` | `1.5` ($150\%$)<br>`0` (`UseCoreElement`)`0.5` ($50\%$)<br>`2.0` sec | Applies corruption build up ($150\%$ damage). Charmed target attacks apply a $50\%$ slow lasting $2.0$ seconds. |

