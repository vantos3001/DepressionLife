# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity life-sim / tamagotchi game using **Unity 6** (6000.0.60f1). The player controls a character who walks around an apartment, manages personal stats (mood, energy, etc.), completes mini-games, and interacts with NPCs. Side-view camera, mixed 3D + 2D art. Hybrid ECS + OOP architecture with LeoECS Lite, Zenject DI.

## Build & Development

This is a Unity project — there is no CLI build command. Open in Unity Editor (version 6000.0.60f1). No automated tests or CI pipeline.

## Architecture

### High-Level Layers

- **`Assets/Scripts/Core/<Feature>/`** — game features organized by domain. Each feature folder contains its Services/, Features/ (ECS feature groups), and components.
- **`Assets/Scripts/Common/`** — shared code: Ecs/ (ECS infrastructure), EcsHelper/ (MonoEntity, MonoLink, EcsFactory, EventsBus).
- **`Assets/Scripts/Infrastructure/`** — DI setup (Installers/).
- **`Assets/Content/Data/`** — ScriptableObject configs (e.g. `GameSettings`).

Code is organized **by feature**, not by technical layer. Do not introduce extra layers.

### Key Infrastructure Files

- **`Infrastructure/Installers/ProjectInstaller.cs`** — project-level Zenject installer; binds project-wide services.
- **`Infrastructure/Installers/CoreGameInstaller.cs`** — scene-level Zenject installer; binds `SceneData`, `GameSettings`, `SystemFactory`, `GameWorldService`.
- **`Common/Ecs/Services/EcsWorldService.cs`** — abstract base for ECS world initialization; creates `EcsWorld` and `EcsSystems` for Update/FixedUpdate/LateUpdate cycles; called via Zenject `Initialize()` → `SetupSystems()` → `Init()`.
- **`Common/Ecs/Feature.cs`** — base class for ECS feature groups; each feature registers its systems in the constructor via `ISystemFactory`.
- **`Common/Ecs/ProxyFeature.cs`** — extension methods bridging `Feature` with `IEcsSystems` (`Add(Feature)`).
- **`Common/Ecs/Services/SystemFactory.cs`** — creates ECS systems via `DiContainer.Instantiate<T>()`.
- **`Common/Ecs/Services/SharedDataProvider.cs`** — holds reference to `SharedData`; resolved by services that need ECS world access.
- **`Core/Services/GameWorldService.cs`** — concrete ECS world service; registers `InputFeature` and `PlayerFeature`.

### ECS + OOP Integration Pattern

1. **Components** — data-only structs (`SomethingComponent`, `SomethingData`).
2. **Systems** — logic only, receive services/configs via Zenject constructor injection or `EcsCustomInject`.
3. **Features** — inherit from `Feature`, register system lists in constructor, grouped by domain.
4. **Scene bridge** — `Mono*` wrappers (e.g. `NavMeshAgentMonoLink`, `AnimatorMonoLink`) hold Unity references, connecting scene objects to ECS entities via `MonoEntity`.
5. **Services** — OOP layer providing gameplay/infrastructure logic, wired via Zenject.

### Shared Data Injection

Systems access `SceneData` and `GameSettings` via LeoECS-Lite's `EcsCustomInject<T>` (injected through `.Inject(_sceneData, _settings)` in `GameWorldService.SetupSystems()`). Do not resolve these via Zenject inside systems — use `EcsCustomInject`.

## Role-Based Rules

Code style rules live in the subdirectory CLAUDE.md:
- **Code tasks** → `Assets/Scripts/CLAUDE.md` (auto-loaded for any .cs file work)
