using NUnit.Framework;
using ServicePlace.DataInitializer;
using System.Linq;
using ServicePlace.Model.DTOModels;

namespace ServicePlace.Logic.NUnitTest.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private Initializer _initializer;

        [SetUp]
        public void Init()
        {
            _initializer = new Initializer(10);
            _initializer.InitializeDb();
        }

        [TearDown]
        public void Dispose()
        {
            _initializer.ClearDb();
        }

        [Test]
        public void Create_CreatedUser_ThrowsNoException()
        {
            var userDto = new UserDTO
            {
                UserName = "newUser",
                Email = "newUserEmail@mail.com",
                Password = "newUserPassword123",
                Name = "newUserName",
            };

            Assert.That(() => _initializer.UserService.Create(userDto), Throws.Nothing);
        }

        [Test]
        public void Create_UserNotValid_ThrowsException()
        {
            var userDto = new UserDTO();

            Assert.That(() => _initializer.UserService.Create(userDto), Throws.Exception);
        }

        [Test]
        public void Update_UpdatedUser_ThrowsNoException()
        {
            var expected = _initializer.IdentityRepository.GetAll().ToList().First();
            var userDto = new UserDTO
            {
                Id = expected.Id,
                UserName = expected.UserName,
                Name = "New name",
                Email = "newUserEmail@email.com",
                Avatar = "avatarUrl"
            };
            Assert.That(() => _initializer.UserService.Update(userDto), Throws.Nothing);
            var actual = _initializer.IdentityRepository.GetBy(x => x.Id == expected.Id).Single();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UserNotValid_ThrowsException()
        {
            var userDto = new UserDTO
            {
                Id = _initializer.IdentityRepository.GetAll().First().Id
            };

            Assert.That(() => _initializer.UserService.Update(userDto), Throws.Exception);
        }

        [Test]
        public void Delete_DeletedUser_Success()
        {
            var userDtoToDelete = new UserDTO
            {
                Id = _initializer.IdentityRepository.GetAll().First().Id
            };
            var userToDelete = _initializer.IdentityRepository.GetBy(x => x.Id == userDtoToDelete.Id).First();
            Assert.That(() => _initializer.UserService.Delete(userDtoToDelete), Throws.Nothing);
            foreach (var actual in _initializer.IdentityRepository.GetAll())
                Assert.AreNotEqual(userToDelete, actual);
        }

        [Test]
        public void Delete_UserNotFound_DoingNothing()
        {
            var userDto = new UserDTO();
            var expected = _initializer.IdentityRepository.GetAll().ToList();
            _initializer.UserService.Delete(userDto);
            var actual = _initializer.IdentityRepository.GetAll().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_Users_ListOfAllUsers()
        {
            var expected = _initializer.IdentityRepository.GetAll().ToList();
            var actual = _initializer.UserService.GetAll().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void FindByEmail_User_UserByEmail()
        {
            var email = _initializer.IdentityRepository.GetAll().Skip(2).First().Email;
            var expected = _initializer.IdentityRepository.GetBy(x => x.Email == email).Single();
            var actual = _initializer.UserService.FindByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindByEmail_EmailNotExists_Null()
        {
            var actual = _initializer.UserService.FindByEmail(null);
            Assert.IsNull(actual);
        }

        [Test]
        public void FindByUserName_User_UserByUserName()
        {
            var expected = _initializer.IdentityRepository.GetAll().First();
            var actual = _initializer.UserService.FindByUserName(expected.UserName);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindByUserName_UserNameNotFound_Null()
        {
            var actual = _initializer.UserService.FindByUserName(null);
            Assert.IsNull(actual);
        }

        [Test]
        public void CreateRole_CreatedRole_ThrowsNoException()
        {
            Assert.That(() => _initializer.UserService.CreateRole("NewRole"), Throws.Nothing);
        }

        [Test]
        public void CreateRole_RoleNameNotValid_ThrowsException()
        {
            Assert.That(() => _initializer.UserService.CreateRole(null), Throws.Exception);
        }
    }
}
