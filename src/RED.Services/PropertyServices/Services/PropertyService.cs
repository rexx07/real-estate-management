using Core.Data;
using Core.Domain.Properties;
using RED.Services.PropertyServices.Interfaces;
using RED.Services.ServiceBase.Services;

namespace RED.Services.PropertyServices.Services;

public class PropertyService: ServiceBase<Property>, IPropertyService
{
    public PropertyService(RedDbContext context) : base(context)
    {
        
    }
}