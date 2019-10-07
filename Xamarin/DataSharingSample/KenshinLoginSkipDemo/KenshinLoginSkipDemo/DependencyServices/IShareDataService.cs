namespace KenshinLoginSkipDemo.DependencyServices
{
    public interface IShareDataService
    {
        /// <summary>
        /// アプリ間共有する情報を設定・保存する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">共有情報</param>
        void SaveSharingSecureData(string key, string value);

        /// <summary>
        /// アプリ間共有する情報を取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>共有情報</returns>
        string GetSharingSecureData(string key);

        /// <summary>
        /// アプリ間共有する情報を削除する
        /// </summary>
        /// <param name="key">キー</param>
        void DeleteSharingSecureData(string key);
    }
}
