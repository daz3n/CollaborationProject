using System.Collections.Generic;
using UnityEngine;

public class GameplayAbilitySystem : MonoBehaviour
{
    public List<SkillBase> skillList = new List<SkillBase>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i].OnStart(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SkillBase skill in skillList)
        {
            skill.OnUpdate(this);
        }
    }
}
