using System.Threading;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace Yumineko.SceneLoader
{
    [Routes(CommandOrdering.Drop)]
    internal partial class FadeLoadSceneCommandReciver
    {
        public async UniTask FadeLoadSceneAsync(FadeLoadSceneCommand cmd, CancellationToken cancellationToken)
        {
            var fadeOutCommand = cmd.CreateFadeOutCommand();
            await Router.Default.PublishAsync(fadeOutCommand, cancellationToken);
            var gotoSceneCommand = cmd.CreateGotoSceneCommand();
            await Router.Default.PublishAsync(gotoSceneCommand, cancellationToken);
            var fadeInCommand = cmd.CreateFadeInCommand();
            await Router.Default.PublishAsync(fadeInCommand, cancellationToken);
        }
    }
}