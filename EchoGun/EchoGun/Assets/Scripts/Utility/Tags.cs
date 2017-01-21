﻿using UnityEngine;
using System.Collections;


// for the autocomplete!

public static class Tags {

    [System.Serializable]
    public class TagsMask {
        [SerializeField]
        protected int mask = 0;
        public int Mask { get { return mask; } }
    }

    public const string Player = "Player";

    public static class Input
    {
        
    }

    public static class LayerNumbers
    {
    }

    public static class Layers
    {
        public const string Default = "Default";
        public const string Player = "Player";
        public const string Enemy = "Enemy";
    }

    public class SortingLayers
    {
        public const string Overlay = "Overlay";
    }

    public static class PlayerPrefKeys
    {
    }

    public static class Options
    {
        public const string SoundLevel = "SoundLevel";
        public const string MusicLevel = "MusicLevel";
    }

    public static class ShaderParams
    {
        public static int color = Shader.PropertyToID("_Color");
        public static int emission = Shader.PropertyToID("_EmissionColor");
        public static int cutoff = Shader.PropertyToID("_Cutoff");
        public static int noiseStrength = Shader.PropertyToID("_NoiseStrength");
        public static int effectTexture = Shader.PropertyToID("_EffectTex");
        public static int rangeMin = Shader.PropertyToID("_RangeMin");
        public static int rangeMax = Shader.PropertyToID("_RangeMax");
        public static int imageStrength = Shader.PropertyToID("_ImageStrength");
        public static int alpha = Shader.PropertyToID("_MainTexAlpha");

        public const int maxGlobalPingCount = 10;
        public static string globalPingCount = "_GLOBAL_PING_COUNT";
        public static string globalPingPos = "_GLOBAL_PING_POS";
        public static string globalTimeSincePing = "_GLOBAL_TIME_SINCE_PING";
        public static string globalPingRange = "_GLOBAL_PING_RANGE";
    }

    public static class Scenes
    {
        public const string CharacterSelect = "CharacterSelect";
        public const string PlayerRegistration = "PlayerRegistration";
        public const string BossSelect = "BossSelect";
        public const string DefeatPopup = "DefeatPopup";
        public const string VictoryPopup = "VictoryPopup";
        public const string MainMenu = "MainMenu";
        public const string Options = "Options";
    }
}