using System;
using UnityEngine;
public class LevelEvents
{
    public event Action<string, Vector2> onLevelLoad;

    public void LevelLoad(string sceneName, Vector2 pos)
    {
        onLevelLoad?.Invoke(sceneName, pos);
    }

    public event Action onLevelUnload;

    public void LevelUnload()
    {
        onLevelUnload?.Invoke();
    }
}

