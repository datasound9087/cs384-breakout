using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementProperty
{
    public string Name { get; }

    private int value;
    private int activationValue;
    private ActivationRule activationRule;
    
    public AchievementProperty(string name, int initialValue, int activationValue, ActivationRule activationRule)
    {
        this.Name = name;
        this.value = initialValue;
        this.activationValue = activationValue;
        this.activationRule = activationRule;
    }

    public bool IsActivated()
    {
        bool activated = false;
        switch (activationRule)
        {
            case ActivationRule.EQUAL_TO: activated = value == activationValue; break;
            case ActivationRule.LESS_THAN: activated = value < activationValue; break;
            case ActivationRule.LESS_EQUAL_TO: activated = value <= activationValue; break;
            case ActivationRule.GREATER_THAN: activated = value > activationValue; break;
            case ActivationRule.GREATER_EQUAL_TO: activated = value >= activationValue; break;
        }

        return activated;
    }
}
