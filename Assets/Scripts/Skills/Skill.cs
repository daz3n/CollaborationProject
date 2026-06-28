using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Jump Skill")]
public class JumpSkill : SkillBase
{
    public float jumpForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnStart(GameplayAbilitySystem abilitySystem)
    {
        
    }

    // Update is called once per frame
    public override void OnUpdate(GameplayAbilitySystem abilitySystem) 
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0)
        {
            currentCooldown = maxCooldown;
            abilitySystem.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
}
