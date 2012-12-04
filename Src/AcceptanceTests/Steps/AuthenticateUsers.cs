using System.Net.Http;
using Cerdo.CNA.Lennart.Common.Infrastructure;
using Cerdo.CNA.Lennart.Web.Models.Login;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace Cerdo.CNA.Lennart.AcceptanceTests.Steps
{
    [Binding]
    public class AuthenticateUsers
    {
        private readonly IFixture _autoData;
        private readonly dynamic _db;
        private readonly Credentials _model;
        private HttpResponseMessage _response;

        public AuthenticateUsers(IDatabase database, IFixture autoData)
        {
            _autoData = autoData;
            _db = database.Open();
            _model = autoData.CreateAnonymous<Credentials>();
        }

        [Given]
        public void Given_a_user_account_exists()
        {
            _db.Users.Insert(Username: _model.Username, Password: _model.Password);
        }

        [When]
        public void When_I_login_with_valid_credentials()
        {
            _response = WebClient.PostAsJson("/api/login", _model);
        }

        [Then]
        public void Then_I_should_be_authenticated()
        {
            GetResponseContentAsString().ShouldContain("\"IsAuthenticated\":true");
        }

        [Then]
        public void Then_it_should_set_the_number_of_failed_authentication_attempts_to_zero()
        {
            GetFailedLoginAttemptsForUser().ShouldEqual(0);
        }

        [When]
        public void When_I_login_with_invalid_credentials()
        {
            _response = WebClient.PostAsJson("/api/login", _autoData.CreateAnonymous<Credentials>());
        }

        [When]
        public void When_I_login_with_an_invalid_password()
        {
            _model.Password = _autoData.CreateAnonymous<string>();
            _response = WebClient.PostAsJson("/api/login", _model);
        }

        [Then]
        public void Then_I_should_receive_an_authentication_failed_error()
        {
            GetResponseContentAsString().ShouldContain("\"IsAuthenticated\":false");
        }

        [Then]
        public void Then_it_should_increment_the_number_of_failed_authentication_attempts()
        {
            GetFailedLoginAttemptsForUser().ShouldEqual(1);
        }

        [Given]
        public void Given_I_have_made_NUMBER_failed_login_attempts(int number)
        {
            _db.Users.Insert(Username: _model.Username, Password: _model.Password, FailedLoginAttempts: number);
        }

        [Then]
        public void Then_I_should_receive_an_error_message_saying_that_I_have_NUMBER_login_attempts_left(int number)
        {
            GetResponseContentAsString().ShouldContain(string.Format("\"AvailableAttempts\":{0}", number));
        }

        [Then]
        public void Then_I_should_receive_an_error_message_saying_that_I_my_account_is_blocked()
        {
            GetResponseContentAsString().ShouldContain("\"AccountBlocked\":true");
        }

        private string GetResponseContentAsString()
        {
            return _response.Content.ReadAsStringAsync().Result;
        }

        private int GetFailedLoginAttemptsForUser()
        {
            return (int)_db.Users.FindByUsername(_model.Username).FailedLoginAttempts;
        }
    }
}
