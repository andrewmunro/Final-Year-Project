using System;
using System.Collections.Generic;

namespace MediBook.Client.Core.Exceptions
{
    public class RegistrationException : Exception
    {
        public Dictionary<string, List<string>> ModelState { get; set; }
        public string AlreadyTaken { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string ErrorMessage
        {
            get
            {
                if (AlreadyTaken != null) return AlreadyTaken;
                if (this.UserName != null) return this.UserName;
                if (this.Password != null) return this.Password;
                if (this.ConfirmPassword != null) return this.ConfirmPassword;
                return "";
            }
        }

        public RegistrationException(Dictionary<string, List<string>> modelState)
        {
            this.ModelState = modelState;
            AlreadyTaken = this.GetModelStateValue("");
            UserName = this.GetModelStateValue("model.UserName");
            Password = this.GetModelStateValue("model.Password");
            ConfirmPassword = this.GetModelStateValue("model.ConfirmPassword");
        }

        public string GetModelStateValue(string key)
        {
            List<string> output;
            ModelState.TryGetValue(key, out output);
            return output != null ? output[0] : null;
        }
    }
}
