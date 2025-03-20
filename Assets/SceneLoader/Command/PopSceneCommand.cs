using VitalRouter;

namespace Yumineko.SceneLoader
{
    public readonly struct PopSceneCommand : ICommand
    {
        public int PopCount { get; init; }
    }
}