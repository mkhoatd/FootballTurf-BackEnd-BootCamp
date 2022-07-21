using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;
using WebApi.Persistence;
using WebApi.Repository.Authorization;
using WebApi.Repository.DTOs.Accounts;
using WebApi.Repository.Helpers;
using WebApi.Repository.Implementation;
using WebApi.Repository.Interface;
using WebApi.Test.Common;
using Xunit;

namespace WebApi.Repository.Test
{
    public class AccountRepositoryTest
    {
        public DbContextOptions<AppFootballTurfDbContext> ContextOptions { get; }
        public AppFootballTurfDbContext Context { get; set; }
        private Mock<DbSet<User>> MockDbSetAppUser { get; set; }
        private Mock<AppFootballTurfDbContext> MockDbContextMock { get; set; }
        private Mock<IOptions<AppSettings>>  MockAppSetting { get; set; }
        private Mock<IEmailService> MockEmailService { get; set; }
        private IJwtUtils JwtUtils { get; set; }
        private Mock<ILogger<AccountRepository>> MockLogger { get; set; }
        private Mock<UserManager<User>> MockUserManager { get; set; }
        private Mock<RoleManager<IdentityRole>> MockRoleManager { get; set; }
        private Mock<SignInManager<User>> MockSignInManager { get; set; }
        private AccountRepository MockAccountRepository { get; set; }
        private RefreshToken RefreshToken { get; set; }

        public AccountRepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<AppFootballTurfDbContext>()
                .UseInMemoryDatabase(databaseName: "database_account").Options;
            Context = new AppFootballTurfDbContext(ContextOptions);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            var appSettings = new AppSettings() { RefreshTokenTTL = 2, 
                Secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING",
            };
            MockAppSetting = new Mock<IOptions<AppSettings>>();
            MockAppSetting.Setup(s => s.Value).Returns(appSettings);
            JwtUtils = new JwtUtils(Context, MockAppSetting.Object);
            var refreshTokensList = new List<RefreshToken>();
            RefreshToken = JwtUtils.GenerateRefreshToken(CommonTest.TestIpAdress);
            refreshTokensList.Add(RefreshToken);
            Context.Users.Add(new User { Email = CommonTest.TestEmail, RefreshTokens = refreshTokensList, 
                ResetToken = CommonTest.ResetPasswordToken,
                ResetTokenExpires = DateTime.UtcNow.AddMinutes(20) 
            });
            Context.SaveChanges();

            MockDbSetAppUser = new Mock<DbSet<User>>();
            MockDbContextMock = new Mock<AppFootballTurfDbContext>();
            MockDbContextMock.Setup(s => s.Set<User>()).Returns(MockDbSetAppUser.Object);
            MockEmailService = new Mock<IEmailService>();
            MockLogger = new Mock<ILogger<AccountRepository>>();
            MockUserManager = CreateMockUserManager<User>();
            MockSignInManager = CreateMockSignInManager<User>();
            MockRoleManager = CreateMockRoleManager<IdentityRole>();


            MockAccountRepository = new AccountRepository(Context, MockAppSetting.Object, MockEmailService.Object,
                MockUserManager.Object, JwtUtils, MockSignInManager.Object,
                MockRoleManager.Object, MockLogger.Object);
        }

        static Mock<UserManager<TIDentityUser>> CreateMockUserManager<TIDentityUser>() where TIDentityUser : IdentityUser
        {
            return new Mock<UserManager<TIDentityUser>>(
                    new Mock<IUserStore<TIDentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    Array.Empty<IUserValidator<TIDentityUser>>(),
                    Array.Empty<IPasswordValidator<TIDentityUser>>(),
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>().Object);
        }
        static Mock<RoleManager<TIdentityRole>> CreateMockRoleManager<TIdentityRole>() where TIdentityRole : IdentityRole
        {
            return new Mock<RoleManager<TIdentityRole>>(
                    new Mock<IRoleStore<TIdentityRole>>().Object,
                    Array.Empty<IRoleValidator<TIdentityRole>>(),
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<ILogger<RoleManager<TIdentityRole>>>().Object);
        }
        static Mock<SignInManager<TIDentityUser>> CreateMockSignInManager<TIDentityUser>() where TIDentityUser : IdentityUser
        {
            return new Mock<SignInManager<TIDentityUser>>(
                new Mock<UserManager<TIDentityUser>>(new Mock<IUserStore<TIDentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    Array.Empty<IUserValidator<TIDentityUser>>(),
                    Array.Empty<IPasswordValidator<TIDentityUser>>(),
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>().Object).Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<TIDentityUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<TIDentityUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<TIDentityUser>>().Object);
        }

        [Fact] 
        public void SignUpSuccessful()
        {
            // Arrange
            IdentityResult createUser = IdentityResult.Success;
            MockUserManager.Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(createUser));

            IdentityResult addToRole = IdentityResult.Success;
            MockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(addToRole));
            
            var signUpRequest = new SignUpRequest()
            {
                Username = CommonTest.UserNameTest1,
                Email = CommonTest.TestEmail,
                Password = CommonTest.TestPassword,
            };

            // Actual
            var tag = MockAccountRepository.SignUp(signUpRequest).Result;

            // Assert
            var objectResult = new AuthenticateRequest()
            {
                Email = CommonTest.TestEmail,
                Password = CommonTest.TestPassword,
            };
            objectResult.Should().BeEquivalentTo(tag);
        }

        [Fact]
        public void AuthenticateSuccessful()
        {
            // Arrange
            var checkPasswordResult = Microsoft.AspNetCore.Identity.SignInResult.Success;
            MockSignInManager.Setup(u => u.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), false))
                .Returns(Task.FromResult(checkPasswordResult));

            var authenticateRequest = new AuthenticateRequest()
            {
                Email = CommonTest.TestEmail,
                Password = CommonTest.TestPassword,
            };

            // Actual
            var tag = MockAccountRepository.Authenticate(authenticateRequest, CommonTest.TestIpAdress);

            // Assert
            Assert.Equal("AuthenticateResponse", tag.GetType().Name);
        }

        [Fact]
        public void RefreshTokenSuccessful()
        {
            // Arrange
            var checkPasswordResult = Microsoft.AspNetCore.Identity.SignInResult.Success;
            MockSignInManager.Setup(u => u.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), false))
                .Returns(Task.FromResult(checkPasswordResult));
            // Actual
            var tag = MockAccountRepository.RefreshToken(RefreshToken.Token, CommonTest.TestIpAdress);

            // Assert
            Assert.Equal("AuthenticateResponse", tag.GetType().Name);
        }

        [Fact]
        public void VoidMethod_RevokeTokenSuccessful()
        {
            try
            {
                // Actual
                MockAccountRepository.RevokeToken(RefreshToken.Token, CommonTest.TestIpAdress);

                // Assert
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void GetAllUserSuccessful()
        {
            // Arrange
            var tag = MockAccountRepository.GetAll();

            // Actual
            var usersList = Context.Users.ToList();

            // Assert
            usersList.Should().BeEquivalentTo(tag);
        }

        [Fact]
        public void GetUserByIdSuccessful()
        {
            // Arrange
            var tag = MockAccountRepository.GetById(Context.Users.First().Id);

            // Actual
            var user = Context.Users.First();

            // Assert
            user.Should().BeEquivalentTo(tag);
        }

        [Fact]
        public void VoidMethod_InvokeForgotPasswordFeatureSuccessful()
        {
            try
            {
                // Arrange
                var forgotPasswordRequest = new ForgotPasswordRequest()
                {
                    Email = CommonTest.TestEmail
                };

                // Actual
                MockAccountRepository.ForgotPassword(forgotPasswordRequest, CommonTest.Origin);

                // Assert
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void VoidMethod_ValidateResetTokenSuccessful()
        {
            try
            {
                // Arrange
                var validateResetTokenRequest = new ValidateResetTokenRequest()
                {
                    Token = CommonTest.ResetPasswordToken
                };

                // Actual
                MockAccountRepository.ValidateResetToken(validateResetTokenRequest);

                // Assert
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void GetUserByRefreshTokenSuccessful()
        {
            // Arrange
            var tag = MockAccountRepository.GetUserByRefreshToken(RefreshToken.Token);

            // Actual
            var user = Context.Users.First();

            // Assert
            user.Should().BeEquivalentTo(tag);
        }
    }
}
