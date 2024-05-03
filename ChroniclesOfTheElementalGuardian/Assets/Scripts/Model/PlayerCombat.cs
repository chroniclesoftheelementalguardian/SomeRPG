using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat
{
    PlayerStats playerStats;
    private Skill _selectedSkill;
    private List<Skill> _skills = new List<Skill>();
    private int _currentSelectedSkillID = 0;
    private Transform transform;

    public PlayerCombat(PlayerStats playerStats, Transform transform)
    {
        this.playerStats = playerStats;
        this.transform = transform;
        RegisterEvents();
        ConstructSkills();
    }

    private void RegisterEvents()
    {
        InputReader.InputSpace += OnInputSpace;
        InputReader.InputE += OnInputE;
        InputReader.InputR += OnInputR;
        InputReader.InputTab += OnInputTab;
    }

    private void ConstructSkills()
    {
        _skills.Add(new FireSkill());
        _skills.Add(new EarthSkill());
        _skills.Add(new AirSkill());
        _skills.Add(new WaterSkill());
        _selectedSkill = _skills[_currentSelectedSkillID];
    }

    private void OnInputTab()
    {
        ChangeSelectedSkill();
    }

    private void OnInputR()
    {
        Defend();
    }

    private void OnInputE()
    {
        UseSkill();
    }

    private void OnInputSpace()
    {
        BasicAttack();
    }

    private void BasicAttack()
    {
        Debug.Log("Basic Attack");
    }

    private void Defend()
    {
        Debug.Log("Defend");
    }

    private void ChangeSelectedSkill()
    {
        _currentSelectedSkillID++;
        if(_currentSelectedSkillID >= _skills.Count){ _currentSelectedSkillID = 0; }
        _selectedSkill = _skills[_currentSelectedSkillID];
        Debug.Log($"Changed Selected Skill to {_selectedSkill}");
    }

    private void UseSkill()
    {
        _selectedSkill.Use(playerStats.AbilityPower, transform);
    }
}
