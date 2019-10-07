using System;
using Foundation;
using KenshinLoginSkipDemo.DependencyServices;
using Security;

namespace KenshinLoginSkipDemo.iOS.DependencyServices
{
    public class ShareDataService : IShareDataService
    {
        /// <summary>
        /// 共有keychainグループ名称
        /// </summary>
        private const string ShareKeychainGroupName = "9MN72CG488.caradaShareKeychain";

        /// <summary>
        /// アプリ間共有する情報を削除する
        /// </summary>
        /// <param name="key">キー</param>
        public void DeleteSharingSecureData(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var record = ExistingRecordForKey(key);
                if (!string.IsNullOrEmpty(GetSharingSecureData(key)))
                    RemoveRecord(record);
            }
        }

        /// <summary>
        /// アプリ間共有する情報を取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>共有情報</returns>
        public string GetSharingSecureData(string key)
        {
            var record = ExistingRecordForKey(key);
            var match = SecKeyChain.QueryAsRecord(record, out var resultCode);

            if (resultCode == SecStatusCode.Success)
            {
                return NSString.FromData(match.ValueData, NSStringEncoding.UTF8).ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// アプリ間共有する情報を設定・保存する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">共有情報</param>
        public void SaveSharingSecureData(string key, string value)
        {
            var record = ExistingRecordForKey(key);
            if (string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrEmpty(GetSharingSecureData(key)))
                    RemoveRecord(record);

                return;
            }

            if (!string.IsNullOrEmpty(GetSharingSecureData(key)))
                RemoveRecord(record);

            var result = SecKeyChain.Add(CreateRecordForNewKeyValue(key, value));
            if (result != SecStatusCode.Success)
            {
                Console.WriteLine("Error occured when save data into keychain");
            }
        }

        /// <summary>
        /// 指定する情報が存在するかどうかをチェックする
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>存在するかどうか</returns>
        private SecRecord ExistingRecordForKey(string key)
        {
            return new SecRecord(SecKind.GenericPassword)
            {
                Account = key,
                Label = key,
                AccessGroup = ShareKeychainGroupName,
                Accessible = SecAccessible.WhenUnlocked
            };
        }

        /// <summary>
        /// 指定するキーで情報を新規作成する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        /// <returns>処理結果</returns>
        private SecRecord CreateRecordForNewKeyValue(string key, string value)
        {
            return new SecRecord(SecKind.GenericPassword)
            {
                Account = key,
                Label = key,
                ValueData = NSData.FromString(value, NSStringEncoding.UTF8),
                AccessGroup = ShareKeychainGroupName,
                Accessible = SecAccessible.WhenUnlocked
            };
        }

        /// <summary>
        /// 指定する情報を削除する
        /// </summary>
        /// <param name="record">指定する情報</param>
        /// <returns>処理結果</returns>
        private bool RemoveRecord(SecRecord record)
        {
            var result = SecKeyChain.Remove(record);
            if (result != SecStatusCode.Success)
            {
                Console.WriteLine("Error occured when remove data from keychain");
                return false;
            }

            return true;
        }
    }
}
