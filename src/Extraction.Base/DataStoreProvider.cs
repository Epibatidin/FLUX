//using Castle.Windsor;
//using Extraction.Interfaces;

//namespace Extraction.Base
//{
//    public class DataStoreProvider : IDataStoreProvider
//    {
//        private readonly IWindsorContainer _container;

//        public DataStoreProvider(IWindsorContainer container)
//        {
//            _container = container;
//        }

//        public DataStore Current()
//        {
//            return _container.Resolve<DataStore>();
//        }
//    }
//}
