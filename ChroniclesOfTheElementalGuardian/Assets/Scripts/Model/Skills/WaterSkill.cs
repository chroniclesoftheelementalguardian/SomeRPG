using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSkill : Skill
{
    public override DamageType GetDamageType()
    {
        return DamageType.Water;
    }

    public override float GetManaCost()
    {
        return 25;
    }

    public override string GetSkillName()
    {
        return "Water Arrow";
    }

    public override void Use(float abilityPower, Transform userTransform)
    {
        SpawnProjectile(
            userTransform.position,
            userTransform.right,
            "Enemy",
            10,
            abilityPower
        );
    }

    protected override string GetProjectileTag()
    {
        return "WaterArrow";
    }
}
