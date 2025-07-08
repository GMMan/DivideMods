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
* `Divide.PCInput`: adds keyboard and mouse support. See section below.
* `Divide.EnableCameraDebug`: enables debug camera movement. See section below.
* `Divide.Sandbox`: miscellaneous code that can be used when exploring the scene with
  [UnityExplorer](https://github.com/sinai-dev/UnityExplorer). It doesn't do anything on its own.

If you haven't done a full playthrough of the game, only `Divide.PlayerMovementSpeed` is recommended
to be installed. The other mods are intended to make another run through the game for achievement
collection easier.

If building from source, make sure to adjust assembly references to where you've installed the game to.

`Divide.PCInput`
----------------

This is a keyboard and mouse input implementation designed to add native PC input support. It is an
improvement over keyboard to controller rebinding solutions by making inputs relative to the game
world. The player character will walk in directions relative to the game world instead of the screen,
and targeting is relative to the character instead of the center of the screen.

### Controls

* W/A/S/D: standard character movement, Synput stick direction
* Left mouse button/Space: primary action - fire, interact with nodes, select node in SOLUS
* Right mouse button/left Ctrl key: draw weapon
* Left Shift key: sprint
* Tab key: toggle SOLUS
* F: toggle mode (supervisor mode, riot mode, exit remote control)
* Q: show health, Synput left stick click
* E: Synput right stick click

Use mouse to aim. Most aiming is of the point and click type, where the aim is in the direction of the
mouse cursor relative to the player character. In supervisor mode, the direction is relative to the
center of the screen so the camera is not moving towards the direction of your mouse as you are
trying to select a target. With weapon drawn, the camera movement is attenuated so you need to chase
things less. Note that keeping your mouse near the center of the character may result in slight
glitching due to the way the camera pans relative to the direction of your aim.

On the start screen, press either Space or the left mouse button to start. The cursor will be visible
if you start the game this way. If you use a controller to start, the cursor will not be shown. Note
that trying to use a controller after starting the game with keyboard or mouse may not work due to the
way the game polls for controllers, where the currently selected player may not be the same one as
the player that the controller is attached to.

On the graphics settings menu, left click to increment value, right click to decrement.

You can adjust key bindings by editing [`KeyBindings.cs`](Divide.PCInput/KeyBindings.cs) and recompiling.

`Divide.EnableCameraDebug`
--------------------------

This enables debug camera movement in-game. When the player does not have input control, debug camera
movement is enabled. This functionality can only be controlled with a controller. Debug camera movement
is not available for matte painting scenes.

### Controls

* Bottom action button (e.g. A on Xbox controller, cross on PS4 controller): toggle debug camera movement
* Left stick: move camera
* Right stick: rotate camera
* D-pad up/down: adjust camera movement speed
* D-pad left/right: adjust camera rotation speed
