using System;
using System.Runtime.Serialization;

namespace UltraHyperOpenConference.Exceptions
{
   [Serializable]
   public class UserAlreadyExistsException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public UserAlreadyExistsException()
      {
      }

      public UserAlreadyExistsException(string message) : base(message)
      {
      }

      public UserAlreadyExistsException(string message, Exception inner) : base(message, inner)
      {
      }

      protected UserAlreadyExistsException(
         SerializationInfo info,
         StreamingContext context) : base(info, context)
      {
      }
   }
}