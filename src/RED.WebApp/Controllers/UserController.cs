using AutoMapper;
using Core.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using RED.Services.Dtos;
using RED.Services.ServiceBase.Interfaces;
using RED.Services.ServiceBase.Services;

namespace RED.WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private ILogger<UserController> _logger;
    private IServiceWrapper _service;
    private IMapper _mapper;

    public UserController(ILogger<UserController> logger, IServiceWrapper serviceWrapper, IMapper mapper)
    {
        _logger = logger;
        _service = serviceWrapper;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _service.User.GetAllUsers();
            _logger.LogInformation("Users returned successfully");

            var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(usersResult);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong in GetAllUsers action: {ExMessage}", ex.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult GetUserById(int id)
    {
        try
        {
            var user = _service.User.GetUserById(id);

            {
                _logger.LogInformation("User with id {Id} returned successfully", id);
                var userResult = _mapper.Map<UserDto>(user);

                return Ok(userResult);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong in GetUserById action:  {ExMessage}", ex.Message);
            return StatusCode(500, "internal server error");
        }
    }

    [HttpGet("{email: string}")]
    public IActionResult GetUserByEmail(string email)
    {
        try
        {
            var user = _service.User.GetUserByEmail(email);

            if (user is null)
            {
                _logger.LogError("User with id {email} not found", email);
                return NotFound();
            }

            _logger.LogInformation("user with email {email} returned successfully", email);
            var userResult = _mapper.Map<UserDto>(user);

            return Ok(userResult);
        }
        catch (Exception ex)
        {
            _logger.LogError("something went wrong inside GetUserByEmail action: {ExMessage}", ex.Message);
            return StatusCode(500, "internal server error");
        }
    }

    [HttpGet("{userIds: int[]}")]
    public IActionResult GetUsersByIds(int[] userIds)
    {
        try
        {
            var users = _service.User.GetUsersByIds(userIds);

            if (userIds == null || userIds.Length == 0)
            {
                _logger.LogError("Users with {userIds} not found", userIds);
                return NotFound();
            }

            _logger.LogInformation("Users with ids {userIds} returned successfully", userIds);
            var usersResult = _mapper.Map<UserDto>(users);
            return Ok(usersResult);

        }
        
        catch (Exception ex)
        {
            _logger.LogError("something went wrong inside GetUsersByIds action: {ExMessage}", ex.Message);
            return StatusCode(500, "internal server error");
        }
    }
}