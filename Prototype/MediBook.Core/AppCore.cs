using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

using MediBook.Client.Core.Components;
using MediBook.Client.Core.Components.Account;
using MediBook.Client.Core.Components.Appointment;
using MediBook.Client.Core.Components.Database;

namespace MediBook.Client.Core
{
    public class AppCore
    {
        private List<ComponentBase> Components { get; set; }

        public AppCore(bool addComponents = true)
        {
            this.Components = new List<ComponentBase>();

            if (addComponents)
            {
                this.Components.Add(new AccountComponent(this));
                this.Components.Add(new AppointmentComponent(this));
                this.Components.Add(new DatabaseComponent(this));
            }
        }

        public T GetComponent<T>()
        {
            return this.Components.OfType<T>().SingleOrDefault();
        }

        public void AddComponent<T>() where T : ComponentBase
        {
            this.Components.Add(Activator.CreateInstance(typeof(T), new object[] { this }) as T);
        }
    }
}
