# SDRSharpPlugins

SDR# のプラグイン集

## SDRSharp.Dial

Arduinoでダイヤル作って回すやつ  
Arduino側は [こちら](https://github.com/M-nohira/SDR-Dial)からどうぞ。  
5000円渡して作ってもらった。  
![スクショ](https://gyazo.ingen084.net/data/5f7c87302166c7cb96060ab0a99bd331.png)  
ダイヤルは4つ  
設定を保存する機能は未実装です。

`sdrsharp-x86/SDRSharpPlugins` 内に展開されることを想定しています。

### 通信規格

COMポート使用  
ボーレート: `4800`

#### 回転受信

`0b_XXXX_0BAA`

- `A` 2bitで0～3のダイヤルの番号
- `B` 1で時計回り
- `0` は未使用
- `X` は前述4bitを反転したもの チェック用

#### LED点灯･消灯

`0b_XXXX_4321`

- `1`~`4` ライトが点灯してるかのフラグ
  - 想定としてはダイヤルと同じ4つで `0` が左
- `X` 前述4bitを反転したもの

接続時に1秒だけ全LEDが点灯します。

