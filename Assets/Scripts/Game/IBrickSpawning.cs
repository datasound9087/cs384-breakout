
/*
 * Interface for defining brick spawn rules.
*/
public interface IBrickSpawning
{
    // Called for every position where a brick could be spawned
    // Should a brick be placed at this position
    public bool OnPlace(int x, int y);

    // Called for every placed brick
    // Additional brick initialisation goes here
    public void OnBrickInitialise(int x, int y, Brick brick);
}