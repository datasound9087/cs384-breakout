
public class PowerupInfo
{    
    private string name;
    private bool onHit;
    private int onDurability;

    public PowerupInfo(PowerupProperty property)
    {
        this.name = property.name;
        this.onHit = property.onHit;
        this.onDurability = property.onDurability;
    }

    public PowerupInfo(string name, bool onHit, int onDurability)
    {
        this.name = name;
        this.onHit = onHit;
        this.onDurability = onDurability;
    }

    public string GetName()
    {
        return name;
    }

    public bool OnHit()
    {
        return onHit;
    }

    public int GetDurability()
    {
        return onDurability;
    }
}