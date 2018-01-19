using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Services.SQLite
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteConnection CreateConnection(string databaseFileName);
    }
}
