using FLUX.DomainObjects;
using Microsoft.AspNet.Mvc;


namespace FLUX.Interfaces.Web
{
    public interface IConfigurationFormModelBuilder : IFormProcessor<ConfigurationFormModel>
    {
    }

    public interface IFormProcessor<TFormModel>
    {
        TFormModel Build();

        void Update(TFormModel formModel, Controller controller);

        void Process(ConfigurationFormModel formModel);
    }
}
