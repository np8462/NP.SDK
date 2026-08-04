using NP.SDK.Contracts;
using NP.SDK.Core.Runtime;
using NP.SDK.Server.Clients;
using System;
using System.Collections.Generic;

namespace NP.SDK.Server.Sessions
{
    /// <summary>
    /// Manages runtime sessions.
    /// </summary>
    public class RuntimeSessionManager
    {
        private readonly Dictionary<Guid, RuntimeSession> _sessions;

        public RuntimeSessionManager()
        {
            _sessions =
                new Dictionary<Guid, RuntimeSession>();
        }

        //--------------------------------------------------
        // Events
        //--------------------------------------------------

        public event Action<RuntimeSession> SessionCreated;

        public event Action<RuntimeSession> SessionDisconnected;

        public event Action<RuntimeSession> SessionRemoved;

        //--------------------------------------------------
        // Properties
        //--------------------------------------------------

        public int Count
        {
            get
            {
                return _sessions.Count;
            }
        }

        public IEnumerable<RuntimeSession> Sessions
        {
            get
            {
                return _sessions.Values;
            }
        }

        //--------------------------------------------------
        // Create
        //--------------------------------------------------

        public RuntimeSession Create(
            IRuntimeClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }


            RuntimeSession session =
                new RuntimeSession();


            session.Name =
                client.Name;


            session.Client =
                client;

            RuntimeClient runtimeClient =
    client as RuntimeClient;

            if (runtimeClient != null)
            {
                runtimeClient.Session =
                    session;
            }

            session.Transport =
                client.Transport == null
                    ? String.Empty
                    : client.Transport.GetType().Name;



            if (client.Connected)
            {
                session.Connect();
            }



            _sessions.Add(
                session.Id,
                session);



            if (SessionCreated != null)
            {
                SessionCreated(session);
            }


            return session;
        }

        //--------------------------------------------------
        // Find
        //--------------------------------------------------

        public RuntimeSession Find(Guid id)
        {
            RuntimeSession session;

            if (_sessions.TryGetValue(
                id,
                out session))
            {
                return session;
            }

            return null;
        }

        public RuntimeSession Find(string id)
        {
            Guid sessionId;

            if (!Guid.TryParse(id, out sessionId))
            {
                return null;
            }

            return Find(sessionId);
        }

        public RuntimeSession Find(IRuntimeClient client)
        {
            if (client == null)
            {
                return null;
            }

            foreach (RuntimeSession session in _sessions.Values)
            {
                if (session.Client == client)
                {
                    return session;
                }
            }

            return null;
        }

        //--------------------------------------------------
        // Disconnect
        //--------------------------------------------------

        public bool Disconnect(
            IRuntimeClient client)
        {
            RuntimeSession session =
                Find(client);


            if(session == null)
            {
                return false;
            }


            session.Disconnect();


            if(SessionDisconnected != null)
            {
                SessionDisconnected(session);
            }


            return true;
        }

        //--------------------------------------------------
        // Remove
        //--------------------------------------------------

        public bool Remove(
            Guid id)
        {
            RuntimeSession session =
                Find(id);


            if(session == null)
            {
                return false;
            }


            session.Close();


            _sessions.Remove(id);



            if(SessionRemoved != null)
            {
                SessionRemoved(session);
            }


            return true;
        }

        //--------------------------------------------------
        // Maintenance
        //--------------------------------------------------

        public void Clear()
        {
            foreach(RuntimeSession session 
                in _sessions.Values)
            {
                session.Close();
            }


            _sessions.Clear();
        }
    }
}