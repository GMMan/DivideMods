Divide Mods Collection
======================

This is a small collection of mods for [Divide](https://store.steampowered.com/app/687270/Divide/).
They are built to be loaded using [MelonLoader v0.5.7](https://github.com/LavaGang/MelonLoader/releases/tag/v0.5.7).

* `Divide.PlayerInvincibility`: makes player invincible. Damage still applies, but health is kept
  to at least 1. Armor slot health are also kept to at least 1 if you have any equipped.
* `Divide.LOGDAlwaysVisible`: makes nodes always visible instead of only after you interact with them.
  Note that this may reduce performance.
* `Divide.PlayerMovementSpeed`: makes player movement 2.5x faster. This speed is a good balance
  that prevents you from flying off stairs most of the time compared to faster speeds. Note that
  going very fast may cause you to miss triggers or fire them out of order and cause issues, so
  be careful.
* `Divide.UnlockHashCards`: unlocks LeachFarm Extractor tool, hash extraction, supervisor mode, and
  riot mode on scene load. Also prevents hash extraction from being disabled (if you ask Eris to
  follow you, she will, but the menu will be stuck in the state where she's near the servers).
* `Divide.ARGlobalLighting`: makes AR overlay always visible. It's still a bit subdued to make them
  less in the way. You can still glance at them to make them brighter.
* `Divide.Sandbox`: miscellaneous code that can be used when exploring the scene with
  [UnityExplorer](https://github.com/sinai-dev/UnityExplorer). It doesn't do anything on its own.

If you haven't done a full playthrough of the game, only `Divide.PlayerMovementSpeed` is recommended
to be installed. The other mods are intended to make another run through the game for achievement
collection easier.

If building from source, make sure to adjust assembly references to where you've installed the game to.
