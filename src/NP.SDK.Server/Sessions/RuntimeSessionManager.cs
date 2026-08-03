using System;
using System.Collections.Generic;
using NP.SDK.Core.Runtime;
using NP.SDK.Contracts;

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

        public event Action<RuntimeSession> SessionCreated;

        public event Action<RuntimeSession> SessionRemoved;

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

            session.Transport =
                client.Transport == null
                    ? String.Empty
                    : client.Transport.GetType().Name;

            session.Connected =
                client.Connected;

            session.LastActivity =
                DateTime.Now;

            _sessions.Add(
                session.Id,
                session);

            if (SessionCreated != null)
            {
                SessionCreated(session);
            }

            return session;
        }

        public RuntimeSession Get(Guid id)
        {
            RuntimeSession session;

            if (_sessions.TryGetValue(id, out session))
            {
                return session;
            }

            return null;
        }

        public bool Remove(Guid id)
        {
            RuntimeSession session;

            if (!_sessions.TryGetValue(id, out session))
            {
                return false;
            }

            _sessions.Remove(id);

            if (SessionRemoved != null)
            {
                SessionRemoved(session);
            }

            return true;
        }

        public void Clear()
        {
            _sessions.Clear();
        }
    }
}