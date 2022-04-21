using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Achievement property (A thing that can be tracked about the game)
  * An achievment can be made up of one or more properties
*/
public class AchievementProperty
{
    // Name
    public string Name { get; }

    // Does this property persist across levels/player deaths
    public bool PersistsAccrossLevels { get; }

    // Current value
    private int value;

    // Initial Value
    private int initialValue;

    // At what value does this property activate
    private int activationValue;

    // Activation Rule
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

    // Is property activated based on it's value and activation rule
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
        // Does this property only have two states
        return initialValue == 0 && activationValue == 1 && activationRule == ActivationRule.EQUAL_TO;
    }

    public string GetProgress()
    {
        return "" + value + "/" + activationValue;
    }
}
