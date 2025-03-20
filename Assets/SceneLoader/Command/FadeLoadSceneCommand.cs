using UnityEngine;
using VitalRouter;

namespace Yumineko.SceneLoader
{
    public readonly struct FadeLoadSceneCommand : ICommand
    {
        public string SceneName { get; init; }
        public float OutDuration { get; init; }
        public float InDuration { get; init; }
        public Color FadeColor { get; init; }
    }
}