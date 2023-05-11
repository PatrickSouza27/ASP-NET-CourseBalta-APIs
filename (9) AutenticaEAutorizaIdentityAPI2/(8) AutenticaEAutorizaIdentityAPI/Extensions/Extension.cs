using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;

namespace _8__AutenticaEAutorizaIdentityAPI.Extensions
{
    public static class Extension
    {
        public static List<string> ExtensionMessage(this ModelStateDictionary model)
        {
            var result = new List<string>();
            foreach (var item in model.Values)
            {
                result.AddRange(item.Errors.Select(x => x.ErrorMessage));
            }
            return result;
        }
    }
}
