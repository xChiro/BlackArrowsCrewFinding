namespace BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;

public static class HttpRequestDataExtension
{
    public static HttpRequestData? GetHttpRequestData(this FunctionContext functionContext)
    {
        try
        {
            var keyValuePair = functionContext.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");

            var functionBindingsFeature = keyValuePair.Value;
            var type = functionBindingsFeature.GetType();

            var inputData =
                type.GetProperties().Single(p => p.Name == "InputData").GetValue(functionBindingsFeature) as
                    IReadOnlyDictionary<string, object>;
            
            return inputData?.Values.SingleOrDefault(o => o is HttpRequestData) as HttpRequestData;
        }
        catch
        {
            return null;
        }
    }
}