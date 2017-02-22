using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Xunit;
using System.Collections.Generic;
using Moq;

namespace ResRoom.Client.Tests
{
    public class ManageUsersPresenterTests
    {
        public class OnInitialize_
        {
            [Fact]
            public void ShouldLoadUsers_WhenUsersNotEmpty()
            {
                var mockedUserServiceModel = new Mock<IUserServiceModel>();
                    mockedUserServiceModel.Setup(s => s.GetUsers())
                    .Returns(
                        new List<User>()
                        {
                            new User(0, "Grizzly", "Bear"),
                            new User(1, "Panda", "Bear"),
                            new User(2, "Ice", "Bear")
                        });
                    

                var mockedView =
                    (new Mock<IManageUsersView>())
                    .SetupAllProperties();

                var presenter = new ManageUsersPresenter(mockedView.Object, mockedUserServiceModel.Object);

                presenter.OnInitialize();

                presenter.ManageUsersView.Users
                    .Should()
                    .NotBeEmpty();
            }
        }
    }

    public class User
    {
        public long ID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public User(
            long id,
            string firstName,
            string lastName)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public interface IManageUsersView
    {
        List<User> Users { get; set; }
        User SelectedUser { get; set; }
    }

    public class ManageUsersPresenter
    {
        public IManageUsersView ManageUsersView { get; private set; }
        public IUserServiceModel UserServiceModel { get; private set; }

        public ManageUsersPresenter(
            IManageUsersView manageUsersView,
            IUserServiceModel userServiceModel)
        {
            if (manageUsersView == null)
            {
                throw new ArgumentNullException(nameof(manageUsersView));
            }

            ManageUsersView = manageUsersView;

            if (userServiceModel == null)
            {
                throw new ArgumentNullException(nameof(userServiceModel));
            }

            UserServiceModel = userServiceModel;
        }

        public void OnInitialize()
        {
            ManageUsersView.Users = UserServiceModel.GetUsers();
        }
    }

    public interface IUserServiceModel
    {
        List<User> GetUsers();
    }
}
