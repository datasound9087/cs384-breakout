using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementProperty
{
    public string Name { get; }
    public bool PersistsAccrossLevels { get; }
    private int value;
    private int initialValue;
    private int activationValue;
    private ActivationRule activationRule;
    
    public AchievementProperty(string name, int initialValue, int activationValue, ActivationRule activationRule, bool persistsAccrossLevels)
    {
        this.Name = name;
        this.initialValue = initialValue;
        this.value = initialValue;
        this.activationValue = activationValue;
        this.activationRule = activationRule;
        this.PersistsAccrossLevels = persistsAccrossLevels;
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

    public void Reset()
    {
        value = initialValue;
    }

    public int Value
    {
        get
        {
            return value;
        }

        set
        {
            // Only update if not achieved - prevents values being updated forever
            if (!IsActivated())
            {
                this.value = value;
            }
        }
    }

    public bool IsBoolean()
    {
        return initialValue == 0 && activationValue == 1;
    }

    public string GetProgress()
    {
        return "" + value + "/" + activationValue;
    }
}
