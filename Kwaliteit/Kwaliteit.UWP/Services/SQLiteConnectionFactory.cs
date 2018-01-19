using Kwaliteit.Domain.Services.SQLite;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.WinRT;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(Kwaliteit.UWP.Services.SQLiteConnectionFactory))]

namespace Kwaliteit.UWP.Services
{
    internal class SQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        public SQLiteConnection CreateConnection(string databaseFileName)
        {
            string path = ApplicationData.Current.LocalFolder.Path;
            path = Path.Combine(path, databaseFileName);

            return new SQLiteConnection(
                new SQLitePlatformWinRT(),
                path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite,
                storeDateTimeAsTicks: false
            );
        }
    }
}
