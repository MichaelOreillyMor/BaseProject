using GFF.Enums;
using GFF.GameStatesMan.GameStates;
using GFF.PoolsMan;
using GFF.RegProviders;
using GFF.ScenesMan;
using GFF.ScenesMan.Utils;
using GFF.UIsMan;

using UnityEngine;

namespace GameStates.GameStates.BaseGS
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

        protected override void OnSetProviders(IGetService serviceLocator)
        {
            SceneProv = serviceLocator.GetService<ISceneProvider>();
            poolProv = serviceLocator.GetService<IPoolProvider>();
            UIProv = serviceLocator.GetService<IUIProvider>();
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
            SceneUIScreenRefs sceneUIScreens = SceneUIScreenRefs.SceneRef;
            RegisterSceneUIScreens(sceneUIScreens);

            LoadNextGameState();
        }

        private void RegisterSceneUIScreens(SceneUIScreenRefs sceneUIScreens)
        {
            if (sceneUIScreens)
            {
                UIProv.RegisterSceneScreens(sceneUIScreens.UIScreens);
            }
        }

        #endregion
    }
}