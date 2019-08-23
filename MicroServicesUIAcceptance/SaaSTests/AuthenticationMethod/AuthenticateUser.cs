using NUnit.Framework;
using OpenQA.Selenium;
using MicroservicesUIAcceptance.BaseMethods;
using System.Diagnostics.CodeAnalysis;
using log4net;
using System;
using MicroServicesUIAcceptance.SaaSTests.DataIndex;

namespace MicroservicesUIAcceptance.SaaSTests.AuthenticationMethod
{
    [ExcludeFromCodeCoverage]
    class AuthenticateUser
    {
        private string userId, userPassword;
        protected static ILog logger = LogManager.GetLogger(typeof(AuthenticateUser));

        public AuthenticateUser()
        {
            try
            {
                this.userId = TestContext.Parameters["userId"].ToString();
                this.userPassword = TestContext.Parameters["userPassword"].ToString();
            }
            catch (Exception e)
            {
                logger.Error("\nException ---\n{0}" + e.StackTrace);
                Assert.Fail(e.StackTrace);
            }
        }

        public AuthenticateUser(string userId, string password)
        {
            try{
                this.userId = userId;
                this.userPassword = password;
            }catch(Exception e){
                logger.Error("\nException ---\n{0}" + e.StackTrace);
                Assert.Fail(e.StackTrace);
            }
            
        }
        public string ReturnLoginId()
        {
            return this.userId;
        }

        public string ReturnLoginPassword()
        {
            return this.userPassword;
        }

        // Login test method for User AD 
        public void LoginUserID(SeleniumHelper seleniumHelper)
        {
            // Login Steps Here!
        }

        public void Logout(SeleniumHelper seleniumHelper)
        {
            // Logout Steps Here! 
        }
    }
}
