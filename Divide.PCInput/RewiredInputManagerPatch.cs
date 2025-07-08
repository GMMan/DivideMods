using HarmonyLib;
using Rewired;
using Rewired.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(InputManager_Base), "Initialize")]
    internal static class RewiredInputManagerPatch
    {
        static void Prefix(InputManager_Base __instance)
        {
            InjectKeyboardAndMouseMap(__instance.userData);
        }

        static void InjectKeyboardAndMouseMap(UserData userData)
        {
            const int MAP_CATEGORY_ID = 0; // Default
            const int LAYOUT_ID = 0; // Default
            const int DEFAULT_CATEGORY_ID = 0; // Default
            const int BUTTON_COMBO_CATEGORY_ID = 5; // ButtonCombo

            // Add custom actions
            userData.DuplicateAction_FromButton(DEFAULT_CATEGORY_ID, ActionIds.ACTION_ID_MOVE_HORIZONTAL); // ActionIds.ACTION_ID_CUSTOM_MOVE_HORIZONTAL
            userData.DuplicateAction_FromButton(DEFAULT_CATEGORY_ID, ActionIds.ACTION_ID_MOVE_VERTICAL); // ActionIds.ACTION_ID_CUSTOM_MOVE_VERTICAL
            userData.DuplicateAction_FromButton(BUTTON_COMBO_CATEGORY_ID, ActionIds.ACTION_ID_LEFT_STICK_CLICK); // ActionIds.ACTION_ID_CUSTOM_LEFT_STICK_CLICK
            userData.DuplicateAction_FromButton(BUTTON_COMBO_CATEGORY_ID, ActionIds.ACTION_ID_RIGHT_STICK_CLICK); // ActionIds.ACTION_ID_CUSTOM_RIGHT_STICK_CLICK

            // Add keyboard map
            userData.CreateKeyboardMap(MAP_CATEGORY_ID, LAYOUT_ID);
            var kbMap = userData.GetKeyboardMap(MAP_CATEGORY_ID, LAYOUT_ID);
            kbMap.actionElementMaps.AddRange(new[]
            {
                new ActionElementMap(ActionIds.ACTION_ID_SPRINT, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_SPRINT, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_BACK_BUTTON, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_START, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_TOGGLE_SOLUS, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_TOGGLE_SOLUS, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_SWITCH_WEAPON_MODE, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_TOGGLE_MODE, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_DRAW_WEAPON, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_DRAW_WEAPON, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_AWAKE, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_UP, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_AWAKE, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_DOWN, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_AWAKE, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_LEFT, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_AWAKE, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_RIGHT, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_SWITCH_LOGD_MODE, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_TOGGLE_MODE, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_PAUSE, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_START, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_SHOW_HEALTH, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_LEFT_STICK_CLICK, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                // Primary trigger key
                new ActionElementMap(ActionIds.ACTION_ID_FIRE, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_PRIMARY_TRIGGER, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_ACTIVATE_LOGD, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_PRIMARY_TRIGGER, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_LOGD_TRIGGER, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_PRIMARY_TRIGGER, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_CLOSE_OVERLAY, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_PRIMARY_TRIGGER, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_HOLD_HANDS, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_PRIMARY_TRIGGER, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_ADD_VALUE, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_PRIMARY_TRIGGER, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_START, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_PRIMARY_TRIGGER, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                // Custom movement
                new ActionElementMap(ActionIds.ACTION_ID_CUSTOM_MOVE_VERTICAL, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_UP, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_CUSTOM_MOVE_VERTICAL, ControllerElementType.Button, Pole.Negative, KeyBindings.KEY_DOWN, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_CUSTOM_MOVE_HORIZONTAL, ControllerElementType.Button, Pole.Negative, KeyBindings.KEY_LEFT, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_CUSTOM_MOVE_HORIZONTAL, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_RIGHT, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                // Custom for Synput
                new ActionElementMap(ActionIds.ACTION_ID_CUSTOM_LEFT_STICK_CLICK, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_LEFT_STICK_CLICK, ModifierKey.None, ModifierKey.None, ModifierKey.None),
                new ActionElementMap(ActionIds.ACTION_ID_CUSTOM_RIGHT_STICK_CLICK, ControllerElementType.Button, Pole.Positive, KeyBindings.KEY_RIGHT_STICK_CLICK, ModifierKey.None, ModifierKey.None, ModifierKey.None),
            });

            // Add mouse map
            userData.CreateMouseMap(MAP_CATEGORY_ID, LAYOUT_ID);
            var mouseMap = userData.GetMouseMap(MAP_CATEGORY_ID, LAYOUT_ID);
            mouseMap.actionElementMaps.AddRange(new[]
            {
                new ActionElementMap(ActionIds.ACTION_ID_FIRE, ControllerElementType.Button, (int)KeyBindings.MOUSE_PRIMARY),
                new ActionElementMap(ActionIds.ACTION_ID_ACTIVATE_LOGD, ControllerElementType.Button, (int)KeyBindings.MOUSE_PRIMARY),
                new ActionElementMap(ActionIds.ACTION_ID_LOGD_TRIGGER, ControllerElementType.Button, (int)KeyBindings.MOUSE_PRIMARY),
                new ActionElementMap(ActionIds.ACTION_ID_CLOSE_OVERLAY, ControllerElementType.Button, (int)KeyBindings.MOUSE_PRIMARY),
                new ActionElementMap(ActionIds.ACTION_ID_HOLD_HANDS, ControllerElementType.Button, (int)KeyBindings.MOUSE_PRIMARY),
                new ActionElementMap(ActionIds.ACTION_ID_DRAW_WEAPON, ControllerElementType.Button, (int)KeyBindings.MOUSE_SECONDARY),
                // Menu stuff
                new ActionElementMap(ActionIds.ACTION_ID_START, ControllerElementType.Button, (int)KeyBindings.MOUSE_PRIMARY),
                new ActionElementMap(ActionIds.ACTION_ID_ADD_VALUE, ControllerElementType.Button, (int)KeyBindings.MOUSE_PRIMARY),
                new ActionElementMap(ActionIds.ACTION_ID_REDUCE_VALUE, ControllerElementType.Button, (int)KeyBindings.MOUSE_SECONDARY),
            });

            // Enable newly inserted maps
            FieldInfo assignMouseOnStartField = typeof(Player_Editor).GetField("_assignMouseOnStart", BindingFlags.Instance | BindingFlags.NonPublic);
            for (int i = 0; i < userData.playerCount; ++i)
            {
                var player = userData.GetPlayer(i);
                if (player.id == Consts.systemPlayerId) continue;
                player.defaultKeyboardMaps.Add(new Player_Editor.Mapping(true, MAP_CATEGORY_ID, LAYOUT_ID));
                player.defaultMouseMaps.Add(new Player_Editor.Mapping(true, MAP_CATEGORY_ID, LAYOUT_ID));
                assignMouseOnStartField.SetValue(player, true);
            }
        }
    }
}
