using VitalRouter;

namespace Yumineko.SceneLoader
{
    public readonly struct GotoSceneCommand : ICommand
    {
        public string SceneName { get; init; }
    }
}