using System.Linq;
using UnityEngine;
using VitalRouter;

namespace Yumineko.SceneLoader
{
    internal static class ReciverMapper
    {
        private static SceneLoadCommandReciver _sceneLoadCommandReciver;
        private static FadeCommandReciver _fadeCommandReciver;
        private static FadeLoadSceneCommandReciver _fadeLoadSceneCommandReciver;

        static ReciverMapper()
        {
            Application.quitting += OnApplicationQuit;
        }

        private static void OnApplicationQuit()
        {
            _sceneLoadCommandReciver.UnmapRoutes();
            _fadeCommandReciver.Dispose();
            _fadeCommandReciver.UnmapRoutes();
            _fadeLoadSceneCommandReciver.UnmapRoutes();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            _sceneLoadCommandReciver = new SceneLoadCommandReciver();
            _sceneLoadCommandReciver.MapTo(Router.Default, new DropOrdering());

            var fadeDIArgs = Resources.FindObjectsOfTypeAll<FadeDIArgs>().FirstOrDefault();
            if (fadeDIArgs == null)
            {
                Debug.LogError("FadeDIArgsが見つかりません。PreloadAssetsに追加されているか確認してください");
                return;
            }

            _fadeCommandReciver = new FadeCommandReciver(fadeDIArgs);
            _fadeCommandReciver.MapTo(Router.Default, new DropOrdering());
            
            _fadeLoadSceneCommandReciver = new FadeLoadSceneCommandReciver();
            _fadeLoadSceneCommandReciver.MapTo(Router.Default, new DropOrdering());
        }
    }
}