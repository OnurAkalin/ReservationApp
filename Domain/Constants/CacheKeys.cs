namespace Domain.Constants;

public enum RedisDatabases
{
    Component = 1,
    Dashboard = 2,
}

public class CacheKeys
{
    public const string Component = "{0}:Component:{1}";
    public const string Image = "{0}:Image:{1}";
}