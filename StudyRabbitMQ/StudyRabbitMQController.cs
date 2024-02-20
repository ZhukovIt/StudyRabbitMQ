using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using StudyRabbitMQ.Dto;
using StudyRabbitMQLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyRabbitMQ
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudyRabbitMQController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public StudyRabbitMQController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users/{id:long}")]
        public ActionResult<UserDto> GetUser(long id)
        {
            Result<User> userResult = _userRepository.Get(id);
            if (userResult.IsFailure)
                return NotFound(userResult.Error);

            UserDto dto = new UserDto() { Login = userResult.Value.Login };

            return Ok(dto);
        }

        [HttpGet("users")]
        public ActionResult<IReadOnlyList<UserDto>> GetUsers()
        {
            IReadOnlyList<User> users = _userRepository.GetAll();

            List<UserDto> dtos = users
                .Select(u => new UserDto()
                {
                    Login = u.Login
                })
                .ToList();

            return Ok(dtos);
        }

        [HttpPost("users")]
        public IActionResult CreateOrUpdateUser(CreateOrUpdateUserDto item)
        {
            Result<Email> loginResult = Email.Create(item.Login);
            if (loginResult.IsFailure)
                return BadRequest(loginResult.Error);

            if (!Guid.TryParse(item.Password, out Guid password))
                return BadRequest("Значение не является корректным Guid!");

            _userRepository.AddOrUpdate(new User(loginResult.Value, password));

            return Ok();
        }

        [HttpDelete("users/{id:long}")]
        public IActionResult DeleteUser(long id)
        {
            Result deleteUserResult = _userRepository.Delete(id);
            if (deleteUserResult.IsFailure)
                return NotFound(deleteUserResult.Error);

            return Ok();
        }
    }
}
