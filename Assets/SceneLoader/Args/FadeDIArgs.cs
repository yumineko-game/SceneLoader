using UnityEngine;

namespace Yumineko.SceneLoader
{
    [CreateAssetMenu]
    internal sealed class FadeDIArgs : ScriptableObject,IFadeDIArgs
    {
        [SerializeField] private Material _fadeMaterial;
        public Material FadeMaterial => _fadeMaterial;
    }
}