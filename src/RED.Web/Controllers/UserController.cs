using AutoMapper;
using Core.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using RED.Services.Dtos;
using RED.Services.ServiceBase.Interfaces;

namespace Red.Web.Controllers;

public class UserController: Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IServiceWrapper _service;
    private readonly IMapper _mapper;

    public UserController(ILogger<UserController> logger, IServiceWrapper serviceWrapper, IMapper mapper)
    {
        _logger = logger;
        _service = serviceWrapper;
        _mapper = mapper;
    }
    
    public IActionResult Index()
    {
        try
        {
            var users = _service.User.GetAllUsers();
            _logger.LogInformation("Users returned successfully");

            var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);
            return View(usersResult);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong in GetAllUsers action: {ExMessage}", ex.Message);
            //return StatusCode(500, "Internal Server Error");
            return NotFound("No users found");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromBody]UserDto user)
    {
        /*var users = FindAll().OrderBy(u => u.UserEmail).ToListAsync();
        
        if (!users.Result.Contains(user))
            throw new ArgumentNullException(nameof(user.Active));

        var newUser = user.UserEmail.Trim();
        EmailValidator.Validate(newUser);*/
        try
        {
            if (user is null)
            {
                _logger.LogError("User object sent fom client is null");
                return BadRequest("User object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid user object sent from client");
                return BadRequest("Invalid Model Object");
            }

            var userEntity = _mapper.Map<User>(user);

            _service.User.CreateUser(userEntity);
            _service.Save();

            var createdUser = _mapper.Map<UserDto>(userEntity);
            return CreatedAtRoute("UserById", new { id = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong with the CreateUser Action: {ex: Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    public IActionResult Details(int id)
    {
        try
        {
            var user = _service.User.GetUserById(id);

            {
                _logger.LogInformation("User with id {Id} returned successfully", id);
                var userResult = _mapper.Map<UserDto>(user);

                return View(userResult);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong in GetUserById action:  {ExMessage}", ex.Message);
            return StatusCode(500, "internal server error");
        }
    }

    public IActionResult Details(string email)
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

            return View(userResult);
        }
        catch (Exception ex)
        {
            _logger.LogError("something went wrong inside GetUserByEmail action: {ExMessage}", ex.Message);
            return StatusCode(500, "internal server error");
        }
    }
}