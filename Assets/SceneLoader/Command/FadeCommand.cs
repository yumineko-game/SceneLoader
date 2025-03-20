using UnityEngine;
using VitalRouter;

namespace Yumineko.SceneLoader
{
    public readonly struct FadeCommand : ICommand
    {
        public float Duration { get; init; }
        public Color StartColor { get;init; }
        public Color EndColor { get; init; }
        
        public float StartStrength { get; init; }
        public float EndStrength { get; init; }
    }
}