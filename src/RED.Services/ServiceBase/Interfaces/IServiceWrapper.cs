using RED.Services.PropertyServices.Interfaces;
using RED.Services.UserServices.Interfaces;

namespace RED.Services.ServiceBase.Interfaces;

public interface IServiceWrapper
{
    IUserService User { get; }
    IPropertyService Property { get; }
    void Save();
}