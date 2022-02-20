
public class PowerupInfo
{    
    private string name;
    private bool onHit;
    private int onDurability;

    public PowerupInfo(BrickPowerupLocation location)
    {
        this.name = location.name;
        this.onHit = location.onHit;
        this.onDurability = location.onDurability;
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