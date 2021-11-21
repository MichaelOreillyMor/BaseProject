using GFF.Enums;

using System;

namespace GFF.ScenesMan
{
    public interface ISceneProvider
    {
        public void LoadScene(SceneKey sceneKey, Action onSceneLoadCallback, bool canReloadSameScene = false);
    }
}