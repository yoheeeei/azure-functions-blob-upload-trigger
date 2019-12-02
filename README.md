# azure-functions-blob-upload-trigger

## ファイル名のフォーマット

以下のファイル名フォーマットを想定している

`P{撮影日(YYMMDD)}_{撮影開始時刻(MMmmss)}_{撮影終了時刻(MMmmss)}.avi`  

例: `P191202_115057_120057.avi`

## やりたいこと

1. Blobに動画をアップロードする
2. ファイル名から撮影開始時刻、撮影終了時刻を取得する
3. DBに撮影開始時刻、撮影終了時刻を書き込む
