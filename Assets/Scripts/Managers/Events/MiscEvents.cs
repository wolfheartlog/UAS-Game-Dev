using System;

public class MiscEvents
{
    public event Action onLeavesCollected;
    public void LeavesCollected() 
    {
        if (onLeavesCollected != null) 
        {
            onLeavesCollected();
        }
    }

    public event Action onGarbageCollected;
    public void GarbageCollected() 
    {
        if (onGarbageCollected != null) 
        {
            onGarbageCollected();
        }
    }
}
