
namespace Extraction.Interfaces
{
    public interface IByKeyAccessor<out T>
    {
        T GetByKey(int key);
    }

    public interface ISongByKeyAccessor : IByKeyAccessor<ISong>
    {

    }

}
