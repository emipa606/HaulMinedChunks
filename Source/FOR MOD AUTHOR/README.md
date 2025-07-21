# Proposed Fixes and Enhancements for Haul Mined Chunks

Hi Mlie,

Hope you're doing well!

I've spent some time investigating and implementing fixes for a few issues with the Haul Mined Chunks mod. The goal was to make chunk auto-hauling more robust and flexible, especially concerning forbidden chunks and area restrictions.

This document outlines the issues we identified, the thought process behind the solutions, and the changes I'm proposing.

---

## 1. Summary of Issues Addressed & Solutions

### Issue A: Chunks from Vanilla Animals Expanded (Framework) Digging Not Auto-Hauling

* **Problem:** Chunks spawned by VEF animals (e.g., in `CompDigPeriodically`) weren't consistently being caught by the existing `GenPlace.TryPlaceThing_Patch`. This meant chunks dug up by these animals would often remain un-hauled.
* **Debugging Path:** We realized that VEF's internal process for spawning these specific chunks might not always fully route through the `GenPlace.TryPlaceThing` method in a way that consistently triggered our `Postfix` for auto-hauling. We observed that the `stackCount` of the newly created `Thing` was being set within `CompDigPeriodically.CompTickInterval`, which indicated a good point for interception.
* **Solution:** Implemented a **Transpiler patch** on `VEF.AnimalBehaviours.CompDigPeriodically.CompTickInterval`. This advanced patch directly modifies the low-level instructions of the original method to inject a call to `HaulMinedChunks.MarkIfNeeded` immediately after the newly dug `Thing` is instantiated and its `stackCount` is set. This ensures that VEF-spawned chunks are explicitly passed to our hauling logic.

### Issue B: Forbidden Chunks Not Being Auto-Hauled

* **Problem:** Chunks, particularly those from deep drilling or specific modded sources, can sometimes spawn *forbidden* to the player's faction by default. The `HaulMinedChunks` mod wasn't explicitly un-forbidding these, meaning pawns would ignore them even if other conditions were met.
* **Debugging Path:** This was identified by observing chunks remaining un-hauled despite passing initial type/filth checks. Reviewing the `MarkIfNeeded` logic, we confirmed there was no explicit step to un-forbid newly spawned chunks.
* **Solution:** Added a crucial line `thing.SetForbidden(false);` within the `HaulMinedChunks.MarkIfNeeded` method. This explicitly sets the chunk to `unforbidden` for the player's faction before attempting to add a haul designation, ensuring pawns will consider it.

### Issue C: Limited Area Filtering Logic (No "OR" condition, No "Unrestricted" Custom Area Bypass)

* **Problem:** The previous area filtering logic (within `MarkIfNeeded`) either implied an "AND" condition for multiple area types or didn't clearly support an "OR" behavior (haul if in Home *or* in Custom Area). Additionally, there was no straightforward way to use a "Custom Area" setting to simply allow hauling from *anywhere* (an "unrestricted" mode).
* **Debugging Path:** We iterated on the `passedAreaChecks` boolean logic within `MarkIfNeeded` to implement the desired "OR" behavior, ensuring that if *any* active area condition is met, the chunk passes. We also discussed and implemented the specific "Unrestricted" keyword bypass for the custom area.
* **Solution:** Reworked the area checking in `HaulMinedChunks.MarkIfNeeded` to use an "OR" logic. Chunks will now be marked for hauling if:
    * No area limits are enabled (both `LimitToHomeArea` and `LimitToCustomArea` are off).
    * **OR** `LimitToHomeArea` is enabled AND the chunk is within the Home Area.
    * **OR** `LimitToCustomArea` is enabled AND (the `CustomAreaName` is set to "Unrestricted" OR the chunk is within the specified named custom area).

---

## 2. Files Provided

Attached/included are the updated source code files, provided in two versions for clarity:

1.  **Clean/Release Versions (.cs files):**
    * `HaulMinedChunks.cs`
    * `GenPlace_TryPlaceThing_Patch.cs`
    * `CompDigPeriodically_CompTickInterval.cs`
    These files contain the final, production-ready code with all debug logs and comments removed for a clean build.

2.  **Debug/Development Versions (.txt files, for reference):**
    * `HaulMinedChunks_DebugVersion.txt`
    * `GenPlace_TryPlaceThing_Patch_DebugVersion.txt`
    * `CompDigPeriodically_CompTickInterval_DebugVersion.txt`
    These files contain the exact same functional code as the release versions but include detailed `Log.Message` calls and inline comments. These should be invaluable if you need to trace the execution flow, understand the logic, or debug any future issues.

---

## 3. Testing Notes

I've performed testing on my end covering all these scenarios, and the changes appear to be working as intended. Chunks are now auto-hauled correctly from VEF animal digging, forbidden chunks are un-forbidden and hauled, and the area filtering provides the expected flexibility.

---

## 4. Post Notes

Our core strategy relies on patching **GenPlace.TryPlaceThing** because it serves as a remarkably universal point for intercepting *any* `Thing` that gets placed onto the map. While items like mined chunks originate from methods such as **Mineable.TrySpawnYield** (which, as you noted, is a `void` method in Core game assemblies, meaning it performs an action without directly returning a value) or other internal spawn logic like **Mineable.SplitAndSpawnChunk**, these methods often, if not always, ultimately call `GenPlace.TryPlaceThing` to perform the actual placement of the item into the game world.

Crucially, some of these original internal methods, like `Mineable.SplitAndSpawnChunk`, might contain their own specific filtering (e.g., checks for existing items, stack limits, or map conditions). By intercepting at the more general `GenPlace.TryPlaceThing` stage, we effectively bypass the need to replicate or understand those complex, internal filters within *our* mod's patching logic. This makes `GenPlace.TryPlaceThing` an ideal "gate" for our primary `Postfix` patch, as it provides direct access to the `Thing` being placed and a clear `bool __result` indicating placement success, allowing **MarkIfNeeded** to apply a unified, comprehensive set of rules.

For specific scenarios like the VEF animal digging (as detailed in Issue A), where chunks might be spawned through alternative pathways or bypass `GenPlace.TryPlaceThing` in a way that's hard for a simple `Postfix` to catch, we implemented a targeted **Transpiler** patch on `CompDigPeriodically.CompTickInterval`. This ensures comprehensive coverage for all chunk spawning methods we identified, making the mod robust across various origins.

All my modifications are located within the `1.6` folder, as older versions of the mod were reportedly working without these specific issues. I haven't been able to extensively test these changes on older RimWorld versions, but you can do so when possible.

---

Please let me know if you have any questions or require further assistance with these changes!

Best regards,
ZenitiPlay