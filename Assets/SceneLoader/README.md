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
// シーンローダーの取得
var sceneLoader = container.Resolve<SceneLoader>();

// シーン遷移の実行
await sceneLoader.LoadSceneAsync(
    new SceneLoaderCommand
    {
        SceneName = "GameScene",
        LoadSceneMode = LoadSceneMode.Single,
        WaitForFade = true
    },
    cancellationToken
);
```

### フェードエフェクトのカスタマイズ

```csharp
// フェードアウト
await fader.FadeAsync(
    new FadeCommand
    {
        Duration = 0.5f,
        StartColor = Color.clear,
        EndColor = Color.black,
        StartStrength = 0,
        EndStrength = 1
    },
    cancellationToken
);
```

## 設定

### フェードマテリアルの設定

1. プロジェクト内に新しいマテリアルを作成
2. シェーダーを「FadeShader」に設定
3. SceneLoaderLifetimeScope にマテリアルを登録

```csharp
public class SceneLoaderLifetimeScope : LifetimeScope
{
    [SerializeField] private Material fadeMaterial;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(new FadeDIArgs { FadeMaterial = fadeMaterial });
        builder.Register<Fader>(Lifetime.Singleton);
        builder.Register<SceneLoader>(Lifetime.Singleton);
    }
}
```

## ライセンス

このプロジェクトは MIT ライセンスの下で公開されています。詳細は[LICENSE](LICENSE)ファイルを参照してください。

## 作者

- Yumineko
- GitHub: [yumineko-game](https://github.com/yumineko-game)
