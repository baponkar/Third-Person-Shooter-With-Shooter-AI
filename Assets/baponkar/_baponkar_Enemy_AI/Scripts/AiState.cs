using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
public enum AiStateId
{
    ChaseTarget,
    Death,
    Idle,
    FindWeapon,
    AttackTarget,
    Patrol,
    FindTarget,
    Sniper,
    Flee
}

public interface AiState
{
    AiStateId GetStateId();

    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent); 
}
}