using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using Yumineko.SceneLoader;
using UnityEngine;
using VitalRouter;

namespace Yumineko.SceneLoader
{
    [Routes(CommandOrdering.Drop)]
    internal partial class FadeCommandReciver : IDisposable
    {
        private static readonly int PropertyFadeColor = Shader.PropertyToID("_Fade_Color");
        private static readonly int PropertyFadeStrength = Shader.PropertyToID("_Fade_Strength");
        private readonly IFadeDIArgs _fadeDIArgs;

        public FadeCommandReciver(IFadeDIArgs fadeDIArgs)
        {
            _fadeDIArgs = fadeDIArgs;
        }


        public async UniTask FadeAsync(FadeCommand cmd, CancellationToken cancellationToken)
        {
            var mat = _fadeDIArgs.FadeMaterial;
            
            mat.SetColor(PropertyFadeColor, cmd.StartColor);
            mat.SetFloat(PropertyFadeStrength, cmd.StartStrength);
            
            try
            {
                var colorHandle = LMotion
                    .Create(cmd.StartColor, cmd.EndColor, cmd.Duration)
                    .Bind(c => mat.SetColor(PropertyFadeColor, c));
                var strengthHandle = LMotion
                    .Create(cmd.StartStrength, cmd.EndStrength, cmd.Duration)
                    .Bind(s => mat.SetFloat(PropertyFadeStrength, s));
                
                await UniTask.WhenAll(colorHandle.ToUniTask(cancellationToken), strengthHandle.ToUniTask(cancellationToken));

            }
            finally
            {
                mat.SetColor(PropertyFadeColor, cmd.EndColor);
                _fadeDIArgs.FadeMaterial.SetFloat(PropertyFadeStrength, cmd.EndStrength);
            }
        }

        public void Dispose()
        {
            _fadeDIArgs.FadeMaterial.SetColor(PropertyFadeColor, Color.clear);
            _fadeDIArgs.FadeMaterial.SetFloat(PropertyFadeStrength, 0);
        }
    }
}