
/*
 * Powerup Information required by a brick.
*/
public class PowerupInfo
{
    // The name of the powerup projectile to spawn
    private string name;

    // At what brick durability should the powerup spawn
    private bool onHit;

    // Should a powerup spawn on each durability change
    private int onDurability;

    // Copy constructor from brick JSON data
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