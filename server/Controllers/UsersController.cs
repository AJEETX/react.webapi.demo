using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Dtos;
using AutoMapper;
using WebApi.Helpers;
using WebApi.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApi.Identity;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        private ITokeniser _tokeniser;

        public UsersController(IUserService userService, IMapper mapper, ITokeniser tokeniser)
        {
            _userService = userService;
            _mapper = mapper;
            _tokeniser = tokeniser;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            try
            {
                var user = _userService.Authenticate(userDto.Username, userDto.Password);

                if (user == null)
                    return BadRequest("Username or password is incorrect");

                var Token = _tokeniser.CreateToken(user.Id.ToString());

                return Ok(new { user.Id, user.Username, user.FirstName, user.LastName, Token });
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);//shout/catch/throw/log
            }            
        }

        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            try
            {
                _userService.Create(user, userDto.Password);
                return Ok( "registered as username"+user.Username);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);//shout/catch/throw/log
            }
        }
    }
}