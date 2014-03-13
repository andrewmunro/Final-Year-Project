using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MediBook.Client.Core.Components;
using MediBook.Client.Core.Components.Account;
using MediBook.Client.Core.Components.Appointment;
using MediBook.Client.Core.Database;

namespace MediBook.Client.Core
{
    public class AppCore
    {
        public SQLiteConnection DatabaseConnection { get; private set; }

        public List<ComponentBase> Components;

        private const string DBName = "MediBookDB.db3";

        public AppCore()
        {
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, DBName);
            this.DatabaseConnection = new SQLiteConnection(path);

            this.Components = new List<ComponentBase>();
            this.Components.Add(new AccountComponent(this));
            this.Components.Add(new AppointmentComponent(this));
        }

        public T GetComponent<T>()
        {
            return this.Components.OfType<T>().SingleOrDefault();
        }
    }
}
