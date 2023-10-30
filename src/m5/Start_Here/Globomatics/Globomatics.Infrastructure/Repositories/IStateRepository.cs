namespace Globomatics.Infrastructure.Repositories;

public interface IStateRepository
{
    public void SetValue(string key, string value);
    public string GetValue(string key);
    public void Remove(string key);
}
