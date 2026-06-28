using UnityEngine;

public class SkillBase : ScriptableObject
{
    public float maxCooldown;
    public float currentCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void OnStart(GameplayAbilitySystem abilitySystem) { }

    // Update is called once per frame
    public virtual void OnUpdate(GameplayAbilitySystem abilitySystem) { }
}