using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates;
using GFFramework.PlayerControllers;
using GFFramework.Pools;
using GFFramework.Scenes;
using GFFramework.UI;

using System;
using System.Collections;
using UnityEngine;

namespace Game.GameStates
{
    /// <summary>
    /// This state handles the load of a new scene and the dispose of the previous one.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/LoadSceneState")]
    public class LoadSceneGameState : BaseGameState
    {
        [SerializeField]
        private SceneKey scene;

        protected IGameStateProvider gameStateProv;
        private IPlayerProvider playerProv;
        private ISceneProvider SceneProv;
        private IPoolProvider poolProv;
        private IUIProvider UIProv;

        LoadScreen loadScreen;

        public override void Setup()
        {
            gameStateProv = Reg.GameStateProv;
            playerProv = Reg.PlayerProv;
            SceneProv = Reg.SceneProv;
            poolProv = Reg.PoolProv;
            UIProv = Reg.UIProv;

            loadScreen = UIProv.ShowLoadScreen(true);
            loadScreen.Setup();

            CleanSceneRefs();

            SceneProv.LoadScene(scene, OnSceneLoaded);
        }

        private void CleanSceneRefs()
        {
            UIProv.CleanSceneScreens();

            if (playerProv != null)
            {
                playerProv.CleanPlayerCharacter();
            }

            if (poolProv != null)
            {
                poolProv.DestroyPoolsMembers();
            }
        }

        public override void Unsetup()
        {
            loadScreen.Unsetup();
            loadScreen = UIProv.ShowLoadScreen(false);
        }

        public override void Update()
        {

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
    }
}