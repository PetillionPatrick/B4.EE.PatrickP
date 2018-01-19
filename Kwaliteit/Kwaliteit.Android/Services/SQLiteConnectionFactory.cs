using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using System;
using System.IO;
using Xamarin.Forms;
using Kwaliteit.Domain.Services.SQLite;

[assembly: Dependency(typeof(Kwaliteit.Droid.Services.SQLiteConnectionFactory))]

namespace Kwaliteit.Droid.Services
{
    internal class SQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        public SQLiteConnection CreateConnection(string databaseFileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = Path.Combine(path, databaseFileName);

            return new SQLiteConnection(
                new SQLitePlatformAndroid(),
                path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite,
                storeDateTimeAsTicks: false
            );
        }
    }
}