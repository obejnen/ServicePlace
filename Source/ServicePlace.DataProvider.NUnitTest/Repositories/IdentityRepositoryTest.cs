using System;
using System.Linq;
using NUnit.Framework;
using ServicePlace.Common;
using ServicePlace.Model.DataModels;
using ServicePlace.DataInitializer;

namespace ServicePlace.DataProvider.NUnitTest.Repositories
{
    [TestFixture]
    public class IdentityRepositoryTest
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
        public void Create_CreatedUser_Success()
        {
            var expected = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "TestUser",
                Email = "TestEmail@email.com",
                PasswordHash = "12_password",
                Avatar = _initializer.ImageRepository.GetAll().FirstOrDefault(),
                Profile = new Profile
                {
                    Name = "Test profile name"
                }
            };
            _initializer.IdentityRepository.Create(expected);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.IdentityRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_UserNotValid_Null()
        {
            var expected = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "username with space",
                Email = "not valid email",
                PasswordHash = "not valid password",
                Avatar = null,
                Profile = null,
            };

            _initializer.IdentityRepository.Create(expected);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.IdentityRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.IsNull(actual);
        }

        [Test]
        public void Update_UpdatedUser_Success()
        {
            var expected = _initializer.IdentityRepository.GetAll().FirstOrDefault();
            expected.UserName = "newUserName";
            expected.Email = "newEmail@email.com";
            
            _initializer.IdentityRepository.Update(expected);
            _initializer.CommitProvider.CommitChanges();

            var actual = _initializer.IdentityRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UserNotValid_UserNotUpdated()
        {
            var expected = _initializer.IdentityRepository.GetAll().First();
            var userToUpdate = _initializer.IdentityRepository.GetAll().First();
            userToUpdate.UserName = "not valid username";
            userToUpdate.Email = "not valid email";
            _initializer.IdentityRepository.Update(userToUpdate);
            _initializer.CommitProvider.CommitChanges();

            var actual = _initializer.IdentityRepository.GetBy(x => x.Id == userToUpdate.Id).FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Delete_DeletedUser_Success()
        {
            var userToDelete = _initializer.IdentityRepository.GetAll().FirstOrDefault();
            _initializer.IdentityRepository.Delete(userToDelete);
            _initializer.CommitProvider.CommitChanges();
            foreach (var actual in _initializer.IdentityRepository.GetAll())
                Assert.AreNotEqual(userToDelete, actual);
        }

        [Test]
        public void Delete_UserNotFound_Exception()
        {
            var user = new User();
            Assert.That(() => _initializer.IdentityRepository.Delete(user), Throws.Exception);
            
        }

        [Test]
        public void Authenticate_UserClaimsIdentity_Success()
        {
            var actual = _initializer.IdentityRepository.Authenticate("Username1", "PasswordHash1");
            Assert.IsNotNull(actual);
        }

        [Test]
        public void Authenticate_UserDataNotValid_Null()
        {
            var actual = _initializer.IdentityRepository.Authenticate("NotExistingUserName", "AndPassword");
            Assert.IsNull(actual);
        }

        [Test]
        public void IsInRole_UserInRole_True()
        {
            var user = _initializer.IdentityRepository.GetAll().FirstOrDefault();
            _initializer.IdentityRepository.AddToRole(user.Id, Constants.UserRoleName);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.IdentityRepository.IsInRole(user.Id, Constants.UserRoleName);
            Assert.IsTrue(actual);
        }

        [Test]
        public void IsInRole_UserNotInRole_False()
        {
            var user = _initializer.IdentityRepository.GetAll().FirstOrDefault();
            var actual = _initializer.IdentityRepository.IsInRole(user.Id, Constants.UserRoleName);
            Assert.IsFalse(actual);
        }

        [Test]
        public void AddToRole_UserAddedToRole_Success()
        {
            var user = _initializer.IdentityRepository.GetAll().FirstOrDefault();
            _initializer.IdentityRepository.AddToRole(user.Id, Constants.UserRoleName);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.IdentityRepository.GetBy(x => x.Id == user.Id).FirstOrDefault();
            Assert.IsTrue(_initializer.IdentityRepository.IsInRole(actual.Id, Constants.UserRoleName));
        }

        [Test]
        public void AddToRole_UserNotFound_Exception()
        {
            Assert.That(() => _initializer.IdentityRepository.AddToRole("not existing userId", Constants.UserRoleName), Throws.Exception);
        }

        [Test]
        public void AddToRole_RoleNotFound_Exception()
        {
            var user = _initializer.IdentityRepository.GetAll().FirstOrDefault();
            Assert.That(() => _initializer.IdentityRepository.AddToRole(user.Id, "not existing role"), Throws.Exception);
        }

        [Test]
        public void RemoveFromRole_UserRemovedFromRole_Success()
        {
            var user = _initializer.IdentityRepository.GetAll().FirstOrDefault();
            _initializer.IdentityRepository.AddToRole(user.Id, Constants.UserRoleName);
            _initializer.CommitProvider.CommitChanges();
            _initializer.IdentityRepository.RemoveFromRole(user.Id, Constants.UserRoleName);
            _initializer.CommitProvider.CommitChanges();
            Assert.IsFalse(_initializer.IdentityRepository.IsInRole(user.Id, Constants.UserRoleName));
        }

        [Test]
        public void RemoveFromRole_UserNotFound_Exception()
        {
            Assert.That(() => _initializer.IdentityRepository.RemoveFromRole("not existing userId", Constants.UserRoleName), Throws.Exception);
        }

        [Test]
        public void CreateRole_CreatedRole_Success()
        {
            var expected = new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = "TestRole",
            };

            _initializer.IdentityRepository.CreateRole(expected);
            _initializer.CommitProvider.CommitChanges();

            var actual = _initializer.Context.Set<Role>().SingleOrDefault(x => x.Name == expected.Name);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateRole_RoleNotValid_Null()
        {
            var role = new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = ""
            };

            _initializer.IdentityRepository.CreateRole(role);
            _initializer.CommitProvider.CommitChanges();

            var actual = _initializer.Context.Set<Role>().SingleOrDefault(x => x.Id == role.Id);

            Assert.IsNull(actual);
        }
    }
}