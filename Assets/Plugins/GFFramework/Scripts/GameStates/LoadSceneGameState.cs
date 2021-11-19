using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates;
using GFFramework.Scenes;
using GFFramework.UI;

using UnityEngine;

namespace GameStates.GameStates
{
    /// <summary>
    /// This the state that handles the load of a new scene and the dispose of the previous one.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/LoadSceneState")]
    public class LoadSceneGameState : BaseGameState
    {
        [SerializeField]
        private SceneKey scene;

        [SerializeField]
        private bool cleanObjectsPool = true;

        private ISceneProvider SceneProv;
        private IPoolProvider poolProv;
        private IUIProvider UIProv;

        #region Setup/Unsetup methods

        protected override void OnSetProviders(IGetProvidersRegister reg)
        {
            SceneProv = reg.SceneProv;
            poolProv = reg.PoolProv;
            UIProv = reg.UIProv;
        }

        protected override void OnPostSetup()
        {
            UIProv.ShowLoadPanel();

            CleanSceneRefs();
            SceneProv.LoadScene(scene, OnSceneLoaded);
        }

        protected override void OnPreUnsetup()
        {
            UIProv.HideLoadPanel();
        }

        #endregion

        public override void Update()
        {

        }

        #region Load Scene methods

        private void CleanSceneRefs()
        {
            UIProv.CleanSceneScreens();
            CleanObjectsPool();
        }

        private void CleanObjectsPool()
        {
            if (cleanObjectsPool && poolProv != null)
            {
                poolProv.DestroyPoolsMembers();
            }
        }

        private void OnSceneLoaded()
        {
            SceneInfo sceneInfo = SceneInfo.SceneRef;
            RegisterSceneRefs(sceneInfo);

            LoadNextGameState();
        }

        private void RegisterSceneRefs(SceneInfo sceneInfo)
        {
            if (sceneInfo)
            {
                UIProv.RegisterSceneScreens(sceneInfo.SceneScreens);
            }
        }

        #endregion
    }
}