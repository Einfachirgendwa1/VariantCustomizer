using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace UltrakillMod {
    [BepInPlugin("UltrakillMod", "UltrakillMod", "1.0.0.0")]
    public class UltrakillMod : BaseUnityPlugin {
        private readonly Harmony harmony = new Harmony("UltrakillModTry2");

        private void Awake() {
            harmony.PatchAll();
            AddThrowCoinSound();
        }

        private void AddThrowCoinSound() {
            MethodInfo throwCoin = AccessTools.Method(typeof(Revolver), "ThrowCoin") ??
                                   throw new Exception("Revolver.ThrowCoin() not found!!");

            HarmonyMethod prefix = new HarmonyMethod(typeof(UltrakillMod), "ThrowCoinSound");
            harmony.Patch(throwCoin, prefix);
        }

        // ReSharper disable once UnusedMember.Local
        private static void ThrowCoinSound() {
            // ...
        }
    }
}