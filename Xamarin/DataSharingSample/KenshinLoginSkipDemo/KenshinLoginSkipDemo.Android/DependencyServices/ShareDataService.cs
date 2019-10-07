using System;
using Android.Accounts;
using KenshinLoginSkipDemo.Common;
using KenshinLoginSkipDemo.DependencyServices;

namespace KenshinLoginSkipDemo.Droid.DependencyServices
{
    public class ShareDataService : IShareDataService
    {
        AccountManager _am = AccountManager.Get(Android.App.Application.Context);
        Account _account = new Account(Constant.AndroidAccountName, Constant.AndroidAccountType);
        public void SaveSharingSecureData(string key, string value)
        {
            //トークンが空の場合は、アカウントを追加する
            if (_am.GetAccountsByType(Constant.AndroidAccountType).Length == 0)
                _am.AddAccountExplicitly(_account, null, null);
            _am.SetAuthToken(_account, Constant.AndroidAuthTokenType, value);
        }

        public string GetSharingSecureData(string key)
        {
            //このタイプのアカウントが存在しない場合、nullに戻ります。
            if (_am.GetAccountsByType(Constant.AndroidAccountType).Length == 0)
                return null;
            return _am.PeekAuthToken(_account, Constant.AndroidAuthTokenType);
        }

        public void DeleteSharingSecureData(string key)
        {
            _am.InvalidateAuthToken(Constant.AndroidAccountType, _am.PeekAuthToken(_account, Constant.AndroidAuthTokenType));
        }
    }
}
