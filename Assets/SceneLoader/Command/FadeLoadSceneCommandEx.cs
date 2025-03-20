using UnityEngine;

namespace Yumineko.SceneLoader
{
    public static class FadeLoadSceneCommandEx
    {
        public static FadeCommand CreateFadeOutCommand(this FadeLoadSceneCommand cmd)
        {
            return new FadeCommand
            {
                Duration = cmd.OutDuration,
                StartColor = Color.clear,
                EndColor = cmd.FadeColor,
                StartStrength = 0,
                EndStrength = 1
            };
        }
        
        public static FadeCommand CreateFadeInCommand(this FadeLoadSceneCommand cmd)
        {
            return new FadeCommand
            {
                Duration = cmd.InDuration,
                StartColor = cmd.FadeColor,
                EndColor = Color.clear,
                StartStrength = 1,
                EndStrength = 0
            };
        }

        public static GotoSceneCommand CreateGotoSceneCommand(this FadeLoadSceneCommand cmd)
        {
            return new GotoSceneCommand
            {
                SceneName = cmd.SceneName,
            };
        }
    }
}