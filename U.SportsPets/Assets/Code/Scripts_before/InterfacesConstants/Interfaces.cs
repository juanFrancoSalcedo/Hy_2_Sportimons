using UnityEngine;
using System;

public interface IScoringPartner
{
    void AddScore();
    ScoringObject scoreObject { get; set; }
    PoolScoreBooster poolScore { get; set; }
}

public interface IPreWarmingObject
{
    void ActiveWarmingBehaviour();
}

public interface ISportCreatureCompetitor
{
    int colorIndex { get; set; }
    SportCreature athlete { get; set; }
    CharacterAnimationManager animatorControl { get; set; }
    void CalculateFeatures();
    void ConfigureCreature();
}

public interface IRespawneableCompetitor
{
     bool respawnByMistake { get; set; }
     void SetRespawnPosition(Vector3 spawnPoint);
}

public interface IKillable
{
    void Dead();
}

public interface ITutoriable
{
    void PlusMission();
    int countMissions { get; set; }
}

public interface IEndMatchAffiliable
{
    void EndMatchDelegate();
}

public interface IPerishable
{
    Action OnMyGameEnded { get; set; }
}

public interface IFeastable
{
    void Celebrate();
}

