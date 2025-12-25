using System;
using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UltrakillMod.UnityWav;
using UnityEngine;

namespace UltrakillMod {
    [BepInPlugin("UltrakillMod", "UltrakillMod", "1.0.0.0")]
    public class UltrakillMod : BaseUnityPlugin {
        private static AudioClip? audioClip;
        private readonly Harmony harmony = new Harmony("UltrakillMod");

        private void Awake() {
            using Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("UltrakillMod.EmbeddedResources.ThrowCoinSound.wav")!;

            byte[] sound = new byte[stream.Length];
            if (stream.Read(sound, 0, sound.Length) != sound.Length) throw new Exception();

            MethodInfo throwCoin = AccessTools.Method(typeof(Revolver), "ThrowCoin")!;
            HarmonyMethod prefix = new HarmonyMethod(typeof(UltrakillMod), nameof(PlaySound));
            harmony.Patch(throwCoin, prefix);
            audioClip = WavUtility.ToAudioClip(sound);
        }

        private static void PlaySound(Revolver __instance) {
            FieldInfo gunAud = typeof(Revolver).GetField("gunAud", BindingFlags.NonPublic | BindingFlags.Instance)!;
            AudioSource audio = (AudioSource)gunAud.GetValue(__instance);
            AudioSource newSource = audio.gameObject.AddComponent<AudioSource>();
            newSource.volume = audio.volume;
            newSource.clip = audioClip!;
            newSource.Play();
        }
    }
}