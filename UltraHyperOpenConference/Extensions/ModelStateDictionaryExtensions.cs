using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UltraHyperOpenConference.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static string ErrorsToString(this ModelStateDictionary modelStateDictionary)
        {
            StringBuilder stringBuilder = new();

            foreach (var propertyError in modelStateDictionary)
            {
                stringBuilder.AppendLine(propertyError.Key);

                foreach (var error in propertyError.Value.Errors)
                {
                    stringBuilder.AppendLine($" - {propertyError.Key}");
                }
            }
            
            return stringBuilder.ToString();
        }
    }
}