using Core.Data;
using RED.Services.PropertyServices.Interfaces;
using RED.Services.PropertyServices.Services;
using RED.Services.ServiceBase.Interfaces;
using RED.Services.UserServices.Interfaces;
using RED.Services.UserServices.Services;

namespace RED.Services.ServiceBase.Services;

public class ServiceWrapper: IServiceWrapper
{
    private readonly RedDbContext _context;
    private IUserService _user;
    private IPropertyService _property;
    
    public ServiceWrapper(RedDbContext context)
    {
        _context = context;
    }

    public IUserService User
    {
        get
        {
            if (_user == null)
            {
                _user = new UserService(_context);
            }

            return _user;
        }
    }

    public IPropertyService Property
    {
        get
        {
            if (_property == null)
            {
                _property = new PropertyService(_context);
            }

            return _property;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
    
}