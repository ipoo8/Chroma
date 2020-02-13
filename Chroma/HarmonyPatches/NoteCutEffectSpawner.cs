﻿using Chroma.Beatmap.Events;
using Chroma.Settings;
using Harmony;
using UnityEngine;

namespace Chroma.HarmonyPatches
{
    [HarmonyPatch(typeof(NoteCutEffectSpawner))]
    [HarmonyPatch("SpawnNoteCutEffect")]
    internal class NoteCutEffectSpawnerSpawnNoteCutEffect
    {
        private static void Prefix(ref NoteController noteController)
        {
            if (!ColourManager.TechnicolourBlocks || ChromaConfig.TechnicolourBlocksStyle != ColourManager.TechnicolourStyle.GRADIENT)
            {
                if (ChromaNoteColourEvent.SavedNoteColours.TryGetValue(noteController, out Color c))
                {
                    ColourManager.SetNoteTypeColourOverride(noteController.noteData.noteType, c);
                }
            }
        }

        private static void Postfix(ref NoteController noteController)
        {
            ColourManager.RemoveNoteTypeColourOverride(noteController.noteData.noteType);
        }
    }
}