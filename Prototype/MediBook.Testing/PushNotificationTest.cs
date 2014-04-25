using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediBook.Testing
{
    [TestClass]
    public class PushNotificationTest
    {
        [TestMethod]
        public async Task PushNotify()
        {
            new PushNotification("test", "testbody", new List<string>() { "APA91bEkzEHAPMkQiE0oYkeHIw_86RlBl6QP_egCjb6C6kv65o4yvsJJ4EVsn1M94jY_2TmVfHgGaS9cec3ygXC-VYmyRqAfSq470sWwOVxt9gYp6_VggmPoESA_9MrteDMsLJ3n3jwwJEzwqWsOv2N-2_FzXhOB41RWbVihqHVD2oOGg-n4uaw" });
        }
    }
}
