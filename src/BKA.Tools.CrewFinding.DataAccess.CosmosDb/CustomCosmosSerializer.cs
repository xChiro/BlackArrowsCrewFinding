using System.Text.Json;

namespace BKA.Tools.CrewFinding.Azure.DataBase;

public class CustomCosmosSerializer : CosmosSerializer
{
    private readonly JsonSerializerOptions _options;
    
    public CustomCosmosSerializer(JsonSerializerOptions options)
    {
        _options = options;
    }
    
    public override T FromStream<T>(Stream stream)
    {   
        var response = JsonSerializer.Deserialize<T>(stream, _options);
        stream.Close();
        
        return response!;
    }

    public override Stream ToStream<T>(T input)
    {
        return new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(input, _options));
    }
}