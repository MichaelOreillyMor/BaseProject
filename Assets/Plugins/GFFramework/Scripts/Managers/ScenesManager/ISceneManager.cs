using GFF.Enums;

using System;

namespace GFF.ScenesMan
{
    public interface ISceneManager
    {
        public void LoadScene(SceneKey sceneKey, Action onSceneLoadCallback, bool canReloadSameScene = false);
    }
}