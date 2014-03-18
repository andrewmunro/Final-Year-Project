using System;
using System.IO;

using MediBook.Client.Core.Config;
using MediBook.Client.Core.Database;

namespace MediBook.Client.Core.Components.Database
{
    public class DatabaseComponent : ComponentBase
    {
        public SQLiteConnection DatabaseConnection { get; private set; }

        public DatabaseComponent(AppCore core)
            : base(core)
        {
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, Configuration.DBName);
            this.DatabaseConnection = new SQLiteConnection(path);
        }
    }
}
