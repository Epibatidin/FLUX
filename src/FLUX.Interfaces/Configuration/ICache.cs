namespace FLUX.Interfaces.Configuration
{
    public interface ICache<TKey, TValue>
    {
        TValue GetItem(TKey key);

        void SetItem(TKey key, TValue value);
    }
}