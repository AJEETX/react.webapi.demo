using System;
using WebApi.Controllers;
using WebApi.Entities;
using WebApi.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Identity;
using WebApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Test
{ 

[TestClass]
public class UserControllerTest
{
    private Mock<ITokeniser> moqTokeniser;
    private UserDto userDto;
    private UsersController sut;
    private Mock<IUserService> moqUserService;
    private Mock<IMapper> moqMapper;

    [TestInitialize]
    public void Init()
    {
        moqTokeniser = new Mock<ITokeniser>();
        moqUserService = new Mock<IUserService>();
        moqMapper = new Mock<IMapper>();
        moqTokeniser.Setup(m => m.CreateToken(It.IsAny<string>())).Returns("token");
        sut = new UsersController(moqUserService.Object, moqMapper.Object, moqTokeniser.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        moqTokeniser = null;
        sut = null;
    }

    [TestMethod]
    public void Successful_login_return_token_with_ok_result()
    {
        //given
        userDto = new UserDto { Username = "ajeet", Password = "jbhifi" };
        var user = new User { Id = 1, Username = userDto.Username, FirstName = "ajeet", LastName = "kumar", PasswordHash = System.Text.Encoding.Unicode.GetBytes("text"), PasswordSalt = System.Text.Encoding.Unicode.GetBytes("text") };
        moqUserService.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

        //when
        IActionResult actionResult = sut.Authenticate(userDto);
        var contentResult = actionResult as OkObjectResult;

        //then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(200, contentResult.StatusCode);
    }

    [TestMethod]
    public void Missing_login_detail_return_bad_request_status_result()
    {
        //given
        userDto = new UserDto { Username = "", Password = "jbhifi" };

        //when
        IActionResult actionResult = sut.Authenticate(userDto);
        var contentResult = actionResult as BadRequestObjectResult;

        //then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(400, contentResult.StatusCode);
    }
}
}