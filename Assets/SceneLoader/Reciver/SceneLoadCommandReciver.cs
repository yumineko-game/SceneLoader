using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VitalRouter;

namespace Yumineko.SceneLoader
{
    [Routes(CommandOrdering.Drop)]
    internal partial class SceneLoadCommandReciver
    {
        private readonly Stack<string> _sceneStack = new();

        public async UniTask GotoSceneAsync(GotoSceneCommand cmd, CancellationToken cancellationToken)
        {
            await SceneManager.LoadSceneAsync(cmd.SceneName, LoadSceneMode.Single).ToUniTask(cancellationToken: cancellationToken);
        }

        public async UniTask PushSceneAsync(PushSceneCommand cmd, CancellationToken cancellationToken)
        {
            await SceneManager.LoadSceneAsync(cmd.SceneName, LoadSceneMode.Additive).ToUniTask(cancellationToken: cancellationToken);
            _sceneStack.Push(cmd.SceneName);
            if (cmd.ChangeActiveScene) SceneManager.SetActiveScene(SceneManager.GetSceneByName(cmd.SceneName));
        }

        public async UniTask PopSceneAsync(PopSceneCommand cmd, CancellationToken cancellationToken)
        {
            var unloadTasks = new List<UniTask>();

            for (var i = 0; i < cmd.PopCount; i++)
            {
                if (_sceneStack.Count == 0) break;
                var sceneName = _sceneStack.Pop();
                unloadTasks.Add(SceneManager.UnloadSceneAsync(sceneName).ToUniTask(cancellationToken: cancellationToken));
            }

            await UniTask.WhenAll(unloadTasks);

            if (_sceneStack.Count > 0)
            {
                var previousSceneName = _sceneStack.Peek();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(previousSceneName));
            }
        }
    }
}