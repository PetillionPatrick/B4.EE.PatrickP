using Kwaliteit.Domain.Models;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Kwaliteit.Domain.Services.SQLite
{
    /// <summary>
    /// Base class for ALL "SQLite" implementations of a service
    /// </summary>
    public abstract class SQLiteServiceBase
    {
        protected readonly SQLiteConnection connection;

        public SQLiteServiceBase()
        {
            //get the platform-specific SQLiteConnection
            var connectionFactory = DependencyService.Get<ISQLiteConnectionFactory>();
            connection = connectionFactory.CreateConnection("kwaliteitDatas.db3");

            connection.DropTable<Beuk>();
            connection.DropTable<Machine>();
            connection.DropTable<User>();
            connection.DropTable<Afkeuren>();
            connection.DropTable<Operator>();
            connection.DropTable<Order>();
            connection.DropTable<Status>();
            connection.DropTable<Unit>();



            //create tables (if not existing)
            //connection.CreateTable<Beuk>();
            //connection.CreateTable<Machine>();
            //connection.CreateTable<User>();
            //connection.CreateTable<Afkeur>();
            //connection.CreateTable<Operator>();
            //connection.CreateTable<Order>();
            //connection.CreateTable<Status>();
            //connection.CreateTable<Unit>();

        }
    }
}
