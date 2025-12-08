public class XPTracker
{
    private float xpToLevel;
    private float currentXP;
    private int currentLevel;

    public XPTracker(float xpToLevel)
    {
        this.xpToLevel = xpToLevel;
    }

    
    public bool AddXP(float amount)
    {
        currentXP += amount;

        if (currentXP >= xpToLevel)
        {
            currentXP -= xpToLevel;
            currentLevel++;
            return true;
        }
        return false;
    }
}
