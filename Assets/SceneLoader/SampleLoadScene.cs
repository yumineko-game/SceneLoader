using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using VitalRouter;

namespace Yumineko.SceneLoader
{
    internal sealed class SampleLoadScene : MonoBehaviour
    {
        [SerializeField] private float _duration = 1;
        [SerializeField] private Color _fadeColor = Color.black;
        [SerializeField] private string _loadSceneName = "SampleScene";



        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                FadeLoadAsync().Forget();
            }
        }

        private async UniTask FadeLoadAsync()
        {
            var cmd = new FadeLoadSceneCommand
            {
                SceneName = _loadSceneName,
                OutDuration = _duration,
                InDuration = _duration,
                FadeColor = _fadeColor
            };
            await Router.Default.PublishAsync(cmd);
        }
    }
}