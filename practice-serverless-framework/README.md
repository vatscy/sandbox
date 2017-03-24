# practice-serverless-framework

Amazon API Gateway + AWS Lambdaで構築するServerless APIを管理するフレームワークを試すだけ

## Serverless Framework

- 公式サイト
  - https://serverless.com/
- GitHub
  - https://github.com/serverless/serverless

## Apex

- 公式サイト
  - http://apex.run/
- GitHub
  - https://github.com/apex/apex
- 参考
  - ApexでAWS Lambdaファンクションを管理する
    - http://dev.classmethod.jp/cloud/aws/how-to-manage-aws-lambda-functions-with-apex/

### インストール

- macOS, Linuxなど

```
curl https://raw.githubusercontent.com/apex/apex/master/install.sh | sh
```

- Windows
  - 下記ページにインストーラ有り
    - https://github.com/apex/apex/releases

### 基本コマンド

- バージョン確認

```
apex version
```

- 最新化

```
apex upgrade
```

### 前提

- AWS CLIが必要
  - `brew install awscli` とか
- `aws configure` してAWSの設定(アクセスキーとか)を済ませておくこと
