using VitalRouter;

namespace Yumineko.SceneLoader
{
    public readonly struct PushSceneCommand : ICommand
    {
        public string SceneName { get; init; }
        public bool ChangeActiveScene { get; init; }
    }
}