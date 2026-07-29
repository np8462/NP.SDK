using System;
using NP.SDK.Core.Runtime;

namespace NP.SDK.Server.Context
{
    /// <summary>
    /// Manages the current runtime context.
    /// </summary>
    public class RuntimeContextService
    {
        private RuntimeContext _context;

        /// <summary>
        /// Raised whenever the runtime context changes.
        /// </summary>
        public event Action<RuntimeContext> ContextChanged;

        /// <summary>
        /// Gets the current runtime context.
        /// </summary>
        public RuntimeContext Current
        {
            get
            {
                return _context;
            }
        }

        /// <summary>
        /// Returns true if a context exists.
        /// </summary>
        public bool HasContext
        {
            get
            {
                return _context != null;
            }
        }

        /// <summary>
        /// Sets the current runtime context.
        /// </summary>
        public void Set(RuntimeContext context)
        {
            _context = context;

            OnContextChanged();
        }

        /// <summary>
        /// Creates a new empty runtime context.
        /// </summary>
        public RuntimeContext Create()
        {
            _context = new RuntimeContext();

            OnContextChanged();

            return _context;
        }

        /// <summary>
        /// Clears the current runtime context.
        /// </summary>
        public void Clear()
        {
            _context = null;

            OnContextChanged();
        }

        /// <summary>
        /// Gets the current runtime context.
        /// </summary>
        public RuntimeContext Get()
        {
            return _context;
        }

        protected virtual void OnContextChanged()
        {
            if (ContextChanged != null)
            {
                ContextChanged(_context);
            }
        }
    }
}