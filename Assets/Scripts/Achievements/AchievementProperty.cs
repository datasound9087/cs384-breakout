using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementProperty
{
    public string Name { get; }
    public int Value { get; set; }
    public bool PersistsAccrossLevels { get; }
    private int initialValue;
    private int activationValue;
    private ActivationRule activationRule;
    
    public AchievementProperty(string name, int initialValue, int activationValue, ActivationRule activationRule, bool persistsAccrossLevels)
    {
        this.Name = name;
        this.initialValue = initialValue;
        this.Value = initialValue;
        this.activationValue = activationValue;
        this.activationRule = activationRule;
        this.PersistsAccrossLevels = persistsAccrossLevels;
    }

    public bool IsActivated()
    {
        bool activated = false;
        switch (activationRule)
        {
            case ActivationRule.EQUAL_TO: activated = Value == activationValue; break;
            case ActivationRule.LESS_THAN: activated = Value < activationValue; break;
            case ActivationRule.LESS_EQUAL_TO: activated = Value <= activationValue; break;
            case ActivationRule.GREATER_THAN: activated = Value > activationValue; break;
            case ActivationRule.GREATER_EQUAL_TO: activated = Value >= activationValue; break;
        }

        return activated;
    }

    public void Reset()
    {
        Value = initialValue;
    }
}
