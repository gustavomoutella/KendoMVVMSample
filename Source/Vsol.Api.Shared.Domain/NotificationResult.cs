using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Vsol.Api.Shared.Domain
{
    public class NotificationResult
    {
        public NotificationResult()
        {
            Errors = new List<string>();
            Messages = new List<string>();

            this.IsValid = true;
        }

        public dynamic Data { get; set; }
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Messages { get; set; }

        public void Add(params NotificationResult[] validationResults)
        {
            foreach(var item in validationResults)
            {
                Errors.AddRange(item.Errors);
                Messages.AddRange(item.Messages);
            }
        }

        public void AddError(string key, string errorMessage)
        {
            Errors.Add(errorMessage);
            this.IsValid = false;
        }

        public void AddError(string key, Exception ex)
        {
            Errors.Add(ex.Message);
            this.IsValid = false;
        }
        
        public void AddError(Exception ex)
        {
            Errors.Add(ex.Message);
            this.IsValid = false;
        }
        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
            this.IsValid = false;
        }

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }

    }
}
