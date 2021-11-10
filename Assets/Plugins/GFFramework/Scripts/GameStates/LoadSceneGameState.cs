using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates;
using GFFramework.Scenes;
using GFFramework.UI;

using UnityEngine;

namespace GameStates.GameStates
{
    /// <summary>
    /// This the base state that handles the load of a new scene and the dispose of the previous one.
    /// It's a WIP, it doesn't allow any inheritance yet.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/LoadSceneState")]
    public class LoadSceneGameState : BaseGameState
    {
        [SerializeField]
        private SceneKey scene;

        private IPlayerProvider playerProv;
        private ISceneProvider SceneProv;
        private IPoolProvider poolProv;
        private IUIProvider UIProv;

        private LoadScreen loadScreen;

        #region Setup/Unsetup methods

        protected override void SetProviders(IGetProvidersRegister reg)
        {
            playerProv = reg.PlayerProv;
            SceneProv = reg.SceneProv;
            poolProv = reg.PoolProv;
            UIProv = reg.UIProv;
        }

        protected override void OnPostSetup()
        {
            loadScreen = UIProv.ShowLoadScreen(true);
            loadScreen.Setup();

            CleanSceneRefs();
            SceneProv.LoadScene(scene, OnSceneLoaded);
        }

        protected override void OnPreUnsetup()
        {
            loadScreen.Unsetup();
            loadScreen = UIProv.ShowLoadScreen(false);
        }

        #endregion

        public override void Update()
        {

        }

        #region Load Scene methods

        private void CleanSceneRefs()
        {
            UIProv.CleanSceneScreens();

            if (playerProv != null)
            {
                playerProv.CleanPlayerCharacter();
            }

            if (poolProv != null)
            {
                //WIP, it should be optional, and only destroy the object pooled by this Scene
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

                if (playerProv != null)
                {
                    playerProv.RegisterScenePlayerCharacter(sceneInfo.PlayerCharacter);
                }

                if (poolProv != null)
                {
                    poolProv.PreloadPools(sceneInfo.PreloadPoolMembers);
                }
            }
        }

        #endregion
    }
}