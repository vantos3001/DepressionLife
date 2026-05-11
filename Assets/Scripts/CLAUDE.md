# Code Rules

Loaded automatically for all .cs file work in Assets/Scripts/.

## Zenject Rules

- Prefer **constructor injection**; use **method injection** for MonoBehaviours.
- Do not manually `new` objects that belong to the DI graph.
- Do **not** use Zenject factories or signals.

## Code Style

- **No comments, no summaries** (`/// <summary>`). Code must be self-documenting.
- **Always use braces** `{ }` for control flow bodies, even single statements.
- **`var`** when type is obvious from RHS; explicit type when it improves readability.
- **Naming**: `PascalCase` for classes/methods/public properties, `_camelCase` for private fields.
- **Fields** at top of class, before methods. Use `[SerializeField]` instead of `public` for Inspector.
- **Static** only for static utility classes. No static methods on instance classes.
- **Boolean methods**: `Has*/Can*/Is*` prefixes. **Fallible methods**: `Try*` prefix.
- **Omit optional args** when value would be null.
- **No redundant validation** when caller is expected to pass valid references.

## Performance

- Avoid allocations in hot paths (Update, ECS systems, large loops).
- Cache component/transform/service references.
- Use existing pooling patterns instead of frequent Instantiate/Destroy.

## Logging

- Tests are not required.
- Use `LogError` in problematic areas with informative messages. Avoid log spam.

## Save System

All game state is stored in `GameState` (`Common/Save/GameState.cs`) and accessed via injected `GameStateProvider`.

```csharp
// Access state in a service
private GameState State => _gameStateProvider.State;
```

**`SaveStates<T>`** — dictionary-based state container keyed by config Id:
```csharp
var state = someState.GetOrCreateState(config.Id); // creates if missing
```

**Adding state to a new feature:**
1. Create `FeatureNameState.cs` (serializable class)
2. Add field to `GameState.cs`: `public FeatureNameState FeatureNameState = new FeatureNameState();`

**Key rule:** Never change or remove state field names in saved classes — breaks deserialization of existing saves.

## Enums

- Always assign explicit integer values to every enum member.
- Always include a `None = 0` as the first value.

## Zenject Lifecycle

Interfaces resolved and called automatically by Zenject when bound with `BindInterfacesTo`:

- `IInitializable.Initialize()` — called once after all bindings are resolved; use for setup logic
- `ITickable.Tick()` — called every Update
- `IFixedTickable.FixedTick()` — called every FixedUpdate
- `ILateTickable.LateTick()` — called every LateUpdate
- `IDisposable.Dispose()` — called on scene/container teardown; use for cleanup

Order within the same interface: determined by binding order in the installer.

## Feature Documentation

After completing any code task in a feature that has its own CLAUDE.md:
- Add new files to the navigation section if created.
- Update the logic description if the flow changed.
- Remove entries for deleted files.

### What belongs in a feature CLAUDE.md

**Write only non-obvious things:**
- Architectural constraints and invariants ("always call X after Y", "routing is decided only in Z")
- Non-obvious data flow
- Gotchas that would surprise a reader

**Do not write:**
- Descriptions of what each method does — the code already says that
- Sequences that are obvious from reading the file top-to-bottom

**Navigation section:** file name + one-line purpose only.

**Size limit:** if a feature CLAUDE.md exceeds ~80 lines, split large subsystems into their own `Subsystem/CLAUDE.md`.
