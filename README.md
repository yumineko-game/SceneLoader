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

### フェードシーン遷移

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

## アーキテクチャ
### Reciver
VitalRouterで発行されたコマンドを受け取り、コマンドに応じた処理を実行するクラスです。  
すべてinternalで定義されており、直接操作することはできません。

### Command
Reciverを操作するためのコマンドです。  
**非同期で実行され、完了するまでの間に新たに発行されたコマンドは無視されます。**

- `FadeLoadSceneCommand`
- `FadeCommand`
- `GotoSceneCommand`
- `PushSceneCommand`
- `PopSceneCommand`

が用意されています。

#### FadeLoadSceneCommand
フェードアウト -> シーンロード -> フェードインの順に実行するコマンドです。  
シーンのロードはGotoで固定です。

#### FadeCommand
フェードを実行するコマンドです。
開始時・終了時のの色や透過度を個別に設定できるため、このコマンドだけでフェードイン・アウトの両方を使い分けられます。

#### GotoSceneCommand
現在のシーンを上書きする形でシーンをロードします。

#### PushSceneCommand
現在のシーンに積み上げる形でシーンをロードします。  
ロードしたシーンをアクティブにするオプションを指定できます。  
主にメニューやオプションシーンなどで使用します。

#### PopSceneCommand
Pushしたシーンを指定した数だけアンロードします。

### ReciverMapper
ReciverのMapとUnmapは、static classであるReciverMapperによってアプリケーションの開始時と終了時に自動で実行されます。  
これにより、使用する際はコマンドのPublishだけで実行できるようになっています。