# Agent Guidelines for SportiMons Arena (U.SportsPets)

## Project Overview

**SportiMons: Arena** - Mobile sports game with creature racing (MVP 2 Weeks)
- **Platform:** Mobile (iOS/Android)
- **Engine:** Unity 6 (6000.0.67f1)
- **Language:** C# 9.0, .NET Framework 4.7.1
- **Core Concept:** Sportimons race on tracks through environments, players tap 3 action buttons (jump, hit, dash, slide)

## Build/Test Commands

**Build from Unity Editor:**
- Build via Unity menu: `File > Build Settings`
- Target: Mobile (iOS/Android)

**No CLI test runner available** - Tests via Unity Test Runner (`Window > General > Test Runner`)

**Project Files:**
- Solution: `SportsPets.sln`
- Main assembly: `Assembly-CSharp.csproj` (auto-generated, DO NOT modify)
- Editor assembly: `Assembly-CSharp-Editor.csproj`

## Architecture Overview

### Core Systems

**1. Input System** (`Scripts/Input Scripts/`)
- Event-driven component-based architecture
- `TouchManager` - Touch/mouse events: `OnTouchBegin`, `OnTouching`, `OnTouchEnd`
- `SwipeManager` - Swipe gestures: `OnSwiped`, `OnSwipedMagnitud`
- `DraggableManager` - Drag operations: `OnDraggBegin`, `OnDragging`, `OnDraggEnd`
- Interfaces: `ITouchable`, `ISwipeable`, `IDraggable`

**2. Minigame Architecture** (`Scripts/MiniGames/`)
- Pattern: Strategy + Interface Composition
- Base: `SwipeCompetitor` for swipe-based minigames
- Interfaces for competitors:
  - `ISportCreatureCompetitor` - Links to SportCreature data
  - `IPreWarmingObject` - Waits for countdown before activation
  - `IKillable`, `IPerishable`, `IFeastable` - Lifecycle hooks
  - `IScoringPartner` - Score integration
  - `IEndMatchAffiliable` - Match end cleanup
- AI per minigame (e.g., `AIMeteors`, `AIDodgeBall`)

**3. SportCreature System** (`Scripts/SportCreature.cs`)
```csharp
[CreateAssetMenu(fileName = "New Creature", menuName = "Inventory/Creature")]
public class SportCreature : ScriptableObject
{
    [Range(1,10)] public int force, luck, speed, accuracy, spirit, reflexes, endurance;
    public GameObject meshCreature;
}
```
- Stats affect AI behavior via `CalculateFeatures()`
- Mesh instantiation via `MeshCreatureConfigurator.Instance`

**4. Game Flow Singletons**
- `GameController` - Match lifecycle, timer, scoring
- `MeshCreatureConfigurator` - Creature spawning (DontDestroyOnLoad)
- `Transition` - Scene transitions (DontDestroyOnLoad)

**5. Data Persistence** (`Scripts/DataSystem.cs`)
```csharp
public static class DataSystem<T>
{
    public static void SaveMoney(int newMoney)  // PlayerPrefs
    public static int LoadMoney()
    public static void ToJsonWrapper(T arg1, string key)  // JsonUtility
    public static T FromJsonWrapper(string key)
}
```
- Keys in `KeyStorage` class (e.g., `MONEY_I`, `LASTLEVEL_I`, `THREETEAM_C`)

**6. Scoring System** (`Scripts/Scoring/`)
- `ScoringSystem` - Per-player score
- `Board` - Leaderboard sorting
- `ScoringObject` - Score triggers

## Code Style Guidelines

### General
- **Indentation:** 4 spaces (no tabs)
- **Braces:** Allman style (on new lines)
- **Line endings:** CRLF (Windows)

### Naming Conventions
- **Classes/Structs:** PascalCase (e.g., `GameController`, `MeteorsController`)
- **Methods:** PascalCase (e.g., `CalculateFeatures()`, `ActiveWarmingBehaviour()`)
- **Public fields:** camelCase (e.g., `timer`, `leaderBoard`, `colorIndex`)
- **Private fields:** camelCase with optional underscore prefix (e.g., `_levelIndex`, `countLoser`)
- **Properties:** PascalCase (e.g., `Instance`, `scoreObject`)
- **Interfaces:** PascalCase with `I` prefix (e.g., `ISportCreatureCompetitor`, `IKillable`)
- **Events:** PascalCase with `On` prefix (e.g., `OnPreGameCountEnded`, `OnMatchEnded`)
- **Constants:** PascalCase with suffix (e.g., `LASTLEVEL_I`, `SOUNDSTATE_I`)

### Unity-Specific Patterns
- Use `[SerializeField]` for Inspector-exposed private fields
- Use `[Header("Section Name")]` for organization
- Use `[Range(min, max)]` for numeric constraints
- MonoBehaviour classes: no namespace (root level)
- Utilities: use namespaces (e.g., `B_Extensions`, `MyBox`)

### Access Modifiers
- Explicitly specify access modifiers
- Private fields: mark `private` explicitly
- Unity lifecycle methods (`Awake`, `Start`, `Update`) can omit modifier

### Formatting
- Opening braces on new lines
- Single blank line between methods
- No space before colon in inheritance
- Spaces after commas, around operators
- Keep lines under 120 characters

### Best Practices
- Use `System.Action` for events (not custom delegates)
- Cache component references in `Awake()` or `Start()`
- Use `FindFirstObjectByType<T>()` and `FindObjectsByType<T>()` (Unity 6 API)
- Avoid `Find()` in `Update()`
- Subscribe to `GameController.Instance.OnMatchEnded` for cleanup
- Implement `CalculateFeatures()` to apply SportCreature stats

### File Organization
- One class per file (preferred)
- File name matches class name
- Editor scripts in `Editor/` folders
- Minigames: `Scripts/MiniGames/SpecificGameScripts/[MinigameName]/`

### Folder Structure
```
Assets/
├── Scripts/
│   ├── Input Scripts/          # Touch, Swipe, Drag managers
│   ├── InterfacesConstants/    # Shared interfaces
│   ├── ManagerGame/            # Character/creature management
│   ├── MiniGames/
│   │   ├── Generics/           # Reusable components
│   │   └── SpecificGameScripts/# Minigame implementations
│   ├── Scoring/                # Score system
│   └── Uitlities/              # GameController, Timer, etc.
├── B_Extension/                # Custom utilities
└── External Assets/            # DOTween, MyBox, TextMeshPro
```

### Error Handling
- Use `Debug.LogError()` for critical failures
- Use `Debug.LogWarning()` for non-critical issues
- Validate serialized fields in `Awake()` or `OnValidate()`

### External Dependencies
- **DOTween** (Demigiant) - Tweening animations
- **MyBox** - Utility toolkit
- **TextMeshPro** - Text rendering
- **MobileConsole** - Mobile debugging

## Domain-Specific Conventions

### Minigame Development
1. Create folder in `Scripts/MiniGames/SpecificGameScripts/[Name]/`
2. Implement player controller inheriting from `SwipeCompetitor` or MonoBehaviour
3. Implement AI controller implementing `ISportCreatureCompetitor`
4. Add interfaces as needed: `IPreWarmingObject`, `IScoringPartner`, `IKillable`
5. Subscribe to `GameController.Instance.OnPreGameCountEnded` for countdown
6. Implement `CalculateFeatures()` to apply stats (speed, reflexes, etc.)

### SportCreature Integration
```csharp
public void ConfigureCreature()
{
    athlete = MeshCreatureConfigurator.Instance.GetPlayerCreature(colorIndex);
    // Instantiate mesh, setup textures, apply stats
}
```

### Input Handling
```csharp
public class MyMinigame : MonoBehaviour, ISwipeable, IPreWarmingObject
{
    void Awake()
    {
        SwipeManager.Instance.OnSwiped += HandleSwipe;
    }
    
    public void ActiveWarmingBehaviour()
    {
        // Enable input after countdown
    }
}
```
