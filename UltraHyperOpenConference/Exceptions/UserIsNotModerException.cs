using System;
using System.Runtime.Serialization;

namespace UltraHyperOpenConference.Exceptions
{
    [Serializable]
    public class UserIsNotModerException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public UserIsNotModerException()
        {
        }

        public UserIsNotModerException(string message) : base(message)
        {
        }

        public UserIsNotModerException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UserIsNotModerException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}