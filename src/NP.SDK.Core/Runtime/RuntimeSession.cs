using NP.SDK.Contracts;
using System;

namespace NP.SDK.Core.Runtime
{
    /// <summary>
    /// Represents a connected runtime session.
    /// </summary>
    public class RuntimeSession
    {
        public RuntimeSession()
        {
            Id = Guid.NewGuid();

            CreatedAt = DateTime.Now;

            LastActivity = DateTime.Now;
        }


        public Guid Id
        {
            get;
            private set;
        }


        public string Name
        {
            get;
            set;
        }


        public string Transport
        {
            get;
            set;
        }


        public DateTime CreatedAt
        {
            get;
            private set;
        }


        public DateTime LastActivity
        {
            get;
            set;
        }


        public RuntimeContext Context
        {
            get;
            set;
        }


        public bool Connected
        {
            get;
            set;
        }

        public IRuntimeClient Client
        {
            get;
            set;
        }
        
        //public IRuntimeClient Client
        //{
        //    get;
        //    private set;
        //}

        //public void AttachClient(
        //    IRuntimeClient client)
        //{
        //    Client = client;
        //}
    }
}