namespace FLUX.Configuration.Windsor.Lifestyle
{
    public interface ICache<TKey, TValue>
    {
        TValue GetItem(TKey key);

        void SetItem(TKey key, TValue value);
    }
}