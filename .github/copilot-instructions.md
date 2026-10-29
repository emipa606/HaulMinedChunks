# GitHub Copilot Instructions for Haul Mined Chunks Mod

## Mod Overview and Purpose

The **Haul Mined Chunks** mod for RimWorld aims to streamline the mining process by automatically marking stone chunks as haulable when they spawn. This is particularly useful for players who wish to optimize their mining operations by ensuring that resources are efficiently gathered and transported.

### Key Features and Systems
- **Automatic Hauling**: Marks stone chunks for hauling upon their spawning.
- **Compatibility**: Works with vanilla chunks as well as chunks from various modded environments.
- **Support for Multiple Mods**: Includes support for:
  - Raise The Roof
  - Vanilla Factions Expanded - Insectoids 2
  - ABF: Artificial Beings Framework
  - Ancient Mining Industry
  - Project RimFactory Revived
  - Drone-mining across various droid-mods.
  - Chunks dug up by Vanilla Framework Expanded-based modded creatures.
  - Chunks spawned by the Deep Drill.

### Coding Patterns and Conventions
- **Mod Structure**: The mod is structured with clear separation between settings, core functionality, and compatibility patches.
- **Code Organization**: The codebase is organized into different classes:
  - **HaulMinedChunksMod**: The main mod class, handling mod settings and initialization.
  - **HaulMinedChunksSettings**: Handles mod-specific settings and data persistence.
  - **Mineable_TrySpawnYield**: Contains Harmony patches to modify chunk spawning behavior.
  - **CodeMatcherExtensions**: Provides utility methods for Harmony patching.

## XML Integration
While the **Haul Mined Chunks** mod does not have any XML files in its current implementation, it is designed to be compatible with various mods that may define their own XML definitions for chunks. Developers should ensure that any new features or integrations with other mods consider XML definitions if necessary.

## Harmony Patching
The mod primarily relies on **Harmony** for patching RimWorld's core methods to enable automatic hauling of stone chunks. Harmony patching is used to:
- Modify the behavior of the `TrySpawnYield` method in the `Mineable` class.
- Integrate with other mods by checking for specific conditions or events related to chunk spawning.

### Example Harmony Patch
csharp
public class Mineable_TrySpawnYield
{
    [HarmonyPatch(typeof(Mineable), "TrySpawnYield")]
    public static class Patch_TrySpawnYield
    {
        [HarmonyPrefix]
        public static void Prefix(Mineable __instance)
        {
            // Code to mark the chunk as haulable
        }

        [HarmonyPostfix]
        public static void Postfix(Mineable __instance)
        {
            // Additional post-processing logic
        }
    }
}


## Suggestions for Copilot
When using GitHub Copilot to contribute to or extend the **Haul Mined Chunks** mod, consider the following suggestions:
- **Focus on Harmony Patches**: Since the mod heavily relies on Harmony, focus on writing high-quality Harmony patches that are both efficient and maintainable.
- **Ensure Compatibility**: When adding support for new mods, ensure that the patches do not interfere with existing functionality and are thoroughly tested.
- **Maintain Code Organization**: Keep the codebase organized and follow the established patterns for settings, core functionality, and compatibility patches.
- **Test Thoroughly**: Always test new features or patches in a controlled environment to ensure they work as expected without causing unintended side effects.

By adhering to these guidelines and leveraging GitHub Copilot's capabilities, developers can contribute effectively to the **Haul Mined Chunks** mod and help enhance the mining experience in RimWorld.

## References
- [RimWorld Modding Wiki](https://rimworldwiki.com/wiki/Modding)
- [Harmony Documentation](https://harmony.pardeike.net/)
- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)

---

This `.github/copilot-instructions.md` file should be added to the root directory of the **Haul Mined Chunks** mod repository to provide clear guidelines for contributors using GitHub Copilot.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

