using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CRUD_APIs_SqlSeverMigrations.Extension
{
    public static class GetErrorExtension
    {
        public static List<string> GetError(this ModelStateDictionary model)
        {
            var result = new List<string>();
            foreach (var s in model.Values)
                result.AddRange(s.Errors.Select(error => error.ErrorMessage));

            return result;

        }
    }
}
