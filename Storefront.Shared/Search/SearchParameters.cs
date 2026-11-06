using System.Reflection;

namespace Storefront.Shared.Search
{
    public abstract class SearchParameters
    {
        public string GetQueryString()
        {
            Type type = GetType();
            PropertyInfo[] props = type.GetProperties();
            string queryString = string.Empty;
            foreach (PropertyInfo prop in props) 
            {
                if (prop.PropertyType.IsValueType)
                {
                    if(!string.IsNullOrEmpty(queryString))
                    {
                        queryString += "&";
                    }
                    queryString += $"{prop.Name}={prop.GetValue(this)}";
                }
            }
            return queryString;
        }
    }
}
