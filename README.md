# SRC: Descent Protocol

**A 2D top-down wave-survival action game prototype built in Unity as an engineering thesis project.**

The player controls an android fighting increasingly difficult waves of enemies. During each run, upgrades allow the player to improve their build and adapt to the growing challenge.

The project focuses on **modular game architecture, extensible gameplay systems, and maintainable C# code**.

## Features

- **Wave-based combat** — survive increasingly difficult enemy encounters.
- **Modular weapon and attack systems** — support interchangeable attack behaviors.
- **Configurable enemies** — separate movement and attack logic for flexible enemy behavior.
- **Projectile pooling** — reuse projectile instances to reduce repeated object creation.
- **Upgrade system** — improve player stats and build during a run.
- **Data-driven configuration** — use Unity ScriptableObjects to separate configuration data from gameplay logic.

## Architecture & Engineering

The project explores practical software engineering principles in game development:

- **Modularity and separation of responsibilities** — organize scripts, configuration assets, and prefabs by gameplay system.
- **Event-driven communication** — reduce direct dependencies between gameplay components.
- **Data-driven design** — use ScriptableObjects for configurable game data.
- **Object pooling and factory-based creation** — manage frequently spawned gameplay objects.
- **SOLID, DRY, and KISS** — guide code organization, reuse, and maintainability.

## Tech Stack

- **Language:** C#
- **Engine:** Unity 6
- **Data configuration:** ScriptableObjects
- **Version control:** Git, GitHub

## Project Status

This project is a functional unpolished game prototype developed for an engineering thesis. Its primary goal was to investigate and demonstrate architectural approaches to building extensible gameplay systems, rather than deliver a finished commercial product.
