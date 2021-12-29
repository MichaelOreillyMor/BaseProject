using GFF.ScenesMan.Keys;
using GFF.GameStatesMan.GameStates;
using GFF.PoolsMan;
using GFF.ServiceLocators;
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

        private ISceneManager SceneMan;
        private IPoolManager poolMan;
        private IUIManager UIMan;

        #region Setup/Unsetup methods

        protected override void OnSetServices(IGetService serviceLocator)
        {
            SceneMan = serviceLocator.GetService<ISceneManager>();
            poolMan = serviceLocator.GetService<IPoolManager>();
            UIMan = serviceLocator.GetService<IUIManager>();
        }

        protected override void OnPostSetup()
        {
            UIMan.ShowLoadPanel();

            CleanSceneRefs();
            SceneMan.LoadScene(scene, OnSceneLoaded);
        }

        protected override void OnPreUnsetup()
        {
            UIMan.HideLoadPanel();
        }

        #endregion

        public override void Update()
        {

        }

        #region Load Scene methods

        private void CleanSceneRefs()
        {
            UIMan.CleanSceneScreens();
            CleanObjectsPool();
        }

        private void CleanObjectsPool()
        {
            if (cleanObjectsPool && poolMan != null)
            {
                poolMan.DestroyPoolsMembers();
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
                UIMan.RegisterSceneScreens(sceneUIScreens.UIScreens);
            }
        }

        #endregion
    }
}