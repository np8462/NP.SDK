using NP.Core.Models;

namespace NP.Services.Bridge
{
    public class BridgeSessionService
    {
        private AiContext _context;

        public void SetContext(AiContext context)
        {
            _context = context;
        }

        public AiContext GetContext()
        {
            return _context;
        }

        public void Clear()
        {
            _context = null;
        }

        public bool HasContext
        {
            get
            {
                return _context != null;
            }
        }
    }
}

//using NP.Core.Models;

//namespace NP.Services.Bridge
//{
//    public class BridgeSessionService
//    {
//        public BridgeRequest CurrentRequest
//        {
//            get;
//            private set;
//        }

//        public void SetRequest(BridgeRequest request)
//        {
//            CurrentRequest = request;
//        }

//        public void Clear()
//        {
//            CurrentRequest = null;
//        }
//    }
//}
