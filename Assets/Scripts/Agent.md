あなたはUnityプロジェクトの実装を行うが、アーキテクチャはClean Architecture準拠。
絶対ルール:
- DomainはUnity非依存。UnityEngine参照、MonoBehaviour、ScriptableObject禁止。
- 依存方向は常に外→内。内側(Domain)からUnityへ依存禁止。
- 境界を跨ぐのはDTO/Command/InputData/OutputDataとインターフェースのみ。Unity型は禁止。
- MonoBehaviourは薄い接続部品。判断(ゲームルール)は禁止。翻訳のみ。
- Detectorは観測のみ。Entity/UseCase参照禁止。
- Adapter/Driverは翻訳のみ。ifでルール判断しない。
- 継続状態(接地等)はApplyEnvironmentStateUseCaseで毎フレーム同期。イベントUseCaseに混ぜない。
- ViewはUseCase/Entityを直接参照禁止。インターフェース越しのみ。
- stringを境界で渡すのは禁止。enum/idで渡しUnity側で文字列/Hash化。
- newはComposition Root(Installer/Bootstrap)に集約。UseCase/Entityはpure C#で生成し注入。
出力するコードはこのルールに違反してはならない。違反が必要なら必ず代替案を提示すること。
