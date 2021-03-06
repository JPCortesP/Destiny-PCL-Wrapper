﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Not for use. Are Public just because JSON.NET Requires it.
/// </summary>
namespace DestinyPCL.InternalTypes
{
    public class PrimaryStat
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class Item
    {
        public object itemHash { get; set; }
        public string itemId { get; set; }
        public int quantity { get; set; }
        public int damageType { get; set; }
        public object damageTypeHash { get; set; }
        public bool isGridComplete { get; set; }
        public int transferStatus { get; set; }
        public int state { get; set; }
        public int characterIndex { get; set; }
        public string bucketHash { get; set; }
        public PrimaryStat primaryStat { get; set; }
    }

    public class STATDEFENSE
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATINTELLECT
    {
        public int statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATDISCIPLINE
    {
        public int statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATSTRENGTH
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATLIGHT
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATARMOR
    {
        public int statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATAGILITY
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATRECOVERY
    {
        public int statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATOPTICS
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATATTACKSPEED
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATDAMAGEREDUCTION
    {
        public int statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATATTACKEFFICIENCY
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class STATATTACKENERGY
    {
        public int statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class Stats
    {
        public STATDEFENSE STAT_DEFENSE { get; set; }
        public STATINTELLECT STAT_INTELLECT { get; set; }
        public STATDISCIPLINE STAT_DISCIPLINE { get; set; }
        public STATSTRENGTH STAT_STRENGTH { get; set; }
        public STATLIGHT STAT_LIGHT { get; set; }
        public STATARMOR STAT_ARMOR { get; set; }
        public STATAGILITY STAT_AGILITY { get; set; }
        public STATRECOVERY STAT_RECOVERY { get; set; }
        public STATOPTICS STAT_OPTICS { get; set; }
        public STATATTACKSPEED STAT_ATTACK_SPEED { get; set; }
        public STATDAMAGEREDUCTION STAT_DAMAGE_REDUCTION { get; set; }
        public STATATTACKEFFICIENCY STAT_ATTACK_EFFICIENCY { get; set; }
        public STATATTACKENERGY STAT_ATTACK_ENERGY { get; set; }
    }

    public class Customization
    {
        public object personality { get; set; }
        public long face { get; set; }
        public object skinColor { get; set; }
        public long lipColor { get; set; }
        public long eyeColor { get; set; }
        public long hairColor { get; set; }
        public object featureColor { get; set; }
        public object decalColor { get; set; }
        public bool wearHelmet { get; set; }
        public long hairIndex { get; set; }
        public long featureIndex { get; set; }
        public long decalIndex { get; set; }
    }

    public class Equipment
    {
        public object itemHash { get; set; }
        public List<object> dyes { get; set; }
    }

    public class PeerView
    {
        public List<Equipment> equipment { get; set; }
    }

    public class CharacterBase
    {
        public string membershipId { get; set; }
        public int membershipType { get; set; }
        public string characterId { get; set; }
        public string dateLastPlayed { get; set; }
        public string minutesPlayedThisSession { get; set; }
        public string minutesPlayedTotal { get; set; }
        public int powerLevel { get; set; }
        public object raceHash { get; set; }
        public object genderHash { get; set; }
        public object classHash { get; set; }
        public long currentActivityHash { get; set; }
        public int lastCompletedStoryHash { get; set; }
        public Stats stats { get; set; }
        public Customization customization { get; set; }
        public int grimoireScore { get; set; }
        public PeerView peerView { get; set; }
        public int genderType { get; set; }
        public int classType { get; set; }
        public long buildStatGroupHash { get; set; }
    }

    public class LevelProgression
    {
        public int dailyProgress { get; set; }
        public int weeklyProgress { get; set; }
        public int currentProgress { get; set; }
        public int level { get; set; }
        public int step { get; set; }
        public int progressToNextLevel { get; set; }
        public int nextLevelAt { get; set; }
        public int progressionHash { get; set; }
    }

    public class Character
    {
        public CharacterBase characterBase { get; set; }
        public LevelProgression levelProgression { get; set; }
        public string emblemPath { get; set; }
        public string backgroundPath { get; set; }
        public long emblemHash { get; set; }
        public int characterLevel { get; set; }
        public int baseCharacterLevel { get; set; }
        public bool isPrestigeLevel { get; set; }
        public double percentToNextLevel { get; set; }
    }

    public class Data
    {
        public List<Item> items { get; set; }
        public List<Character> characters { get; set; }
    }

    public partial class Response
    {
        public Data data { get; set; }
    }

    public class MessageData
    {
    }

    public class PlayerResultRootObject
    {
        public Response Response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public MessageData MessageData { get; set; }
    }
}
