using GFFramework.Enums;

using System;

namespace GFFramework
{
    public interface ISceneProvider
    {
        public void LoadScene(SceneKey sceneKey, Action onSceneLoadCallback, bool canReloadSameScene = false);
    }
}