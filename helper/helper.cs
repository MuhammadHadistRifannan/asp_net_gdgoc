using Npgsql.Replication;

namespace gdgoc_aspnet;

public  static class helper
{
    public static Guid ToGuid(this string input)
    {
        Guid id; 
        if (Guid.TryParse(input , out id)) return id;
        return Guid.Empty;
    }
}
