using System;
using System.IO;

using MediBook.Client.Core.Database;
using MediBook.Shared.Config;

namespace MediBook.Client.Core.Components.Database
{
    public class DatabaseComponent : ComponentBase
    {
        public SQLiteConnection DatabaseConnection { get; private set; }

        public DatabaseComponent(AppCore core)
            : base(core)
        {
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, ConfigurationManager.AppSettings["DBName"]);
            this.DatabaseConnection = new SQLiteConnection(path);
        }
    }
}
