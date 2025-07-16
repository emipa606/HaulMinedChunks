# GitHub Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose
This mod project aims to enhance RimWorld's mining and resource management mechanics by introducing automation and efficiency improvements to deep drilling and quarrying operations. It provides functionalities for automating resource gathering, better management of mined materials, and integrates seamlessly with existing game systems to improve player experience in resource management.

## Key Features and Systems
- **Automated Deep Drilling**: Automates the process of extracting resources using deep drills, reducing micro-management for players.
- **Enhanced Quarry System**: Adds additional functionality and integration to quarry systems for more efficient chunk generation.
- **Better Hauling Logic**: Implements advanced hauling techniques for mined chunks, ensuring resources are effectively managed and stored.
- **Roof Management**: Introduces features around roof building and management to protect mining operations from weather effects.
- **Integration with Vanilla Expanded Framework**: Ensures compatibility and extended features when used in conjunction with the Vanilla Expanded Framework mod suite.

## Coding Patterns and Conventions
- **Consistent Naming Conventions**: Classes and methods are named using PascalCase, matching standard C# conventions.
- **Static Classes for Utilities**: Utility functions, such as those used for placing items, are implemented using static classes to improve modularity and reuse.
- **Internal and Public Class Access Modifiers**: Used strategically to prevent unintended access outside the intended scope. E.g., `internal class CompDeepDrill_TryProducePortion`.
- **Clear Modularity**: Each functionality is encapsulated within its own class, ensuring clear separation of concern.

## XML Integration
- Although specific XML content is not provided, you will need XML patching for defining new items, updating existing defs, and setting up initial conditions for new mechanics.
- Ensure that any XML manipulations are done using RimWorld’s XML patching system, which allows for safe modifications without directly altering original game files.

## Harmony Patching
- **Harmony**: Used extensively for runtime method patching, ensuring modifications are applied without altering the core game codebase directly.
- **Patch Classes**: Typically, patches like `AncientMiningIndustryPatch` and `VanillaExpandedFrameworkPatch` are organized into separate classes for clarity.
- **Prefix and Postfix Methods**: Used in Harmony patches for modifying game behavior before and after methods are executed.

## Suggestions for Copilot
- Leverage GitHub Copilot for generating repetitive or boilerplate code, especially in Harmony patches or XML setup.
- Use Copilot to draft XML patches, suggesting elements based on existing patterns in comparable mods.
- Auto-generate comments for new methods explaining their purpose and usage, improving code readability.
- Generate unit tests for testing deep drill functionality or haul logic using Copilot by describing the expected behavior in the comments.
- Use Copilot for creating outlines or dummy implementations for new features before filling in with detailed logic, aiding in rapid prototyping.

By following these guidelines, development can proceed in a structured, consistent manner with optimal use of automation features provided by tools like GitHub Copilot.
