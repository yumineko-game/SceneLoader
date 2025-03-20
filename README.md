# SceneLoader

Unity 用のシーン遷移管理パッケージです。フェードエフェクト付きのシーン遷移を簡単に実装できます。

## 機能

- フェードイン/アウトを伴うシーン遷移
- VitalRouter を使用したコマンドパターンによる実装
- UniTask による非同期処理
- キャンセル可能な処理
- 依存性注入による疎結合な設計

## 必要条件

- Unity 2022.3 以降
- UniTask 2.5.0
- VitalRouter 1.0.0
- LitMotion 1.0.0

## インストール方法

1. Unity Package Manager を開きます
2. 「+」ボタンをクリックし、「Add package from git URL」を選択
3. 以下の URL を入力：
   ```
   https://github.com/yumineko-game/SceneLoader.git?path=Assets/SceneLoader
   ```

## 使用方法

### 基本的なシーン遷移

```csharp
var cmd = new FadeLoadSceneCommand
{
    SceneName = "SampleScene",
    OutDuration = 1.0f,
    InDuration = 1.0f,
    FadeColor = Color.black
};
await Router.Default.PublishAsync(cmd, cancellationToken);
```

他、`FadeCommand`, `GotoSceneCommand`, `PopSceneCommand`, `PushSceneCommand` が用意されています。
