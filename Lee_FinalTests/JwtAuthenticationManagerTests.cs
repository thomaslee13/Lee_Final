using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lee_Final.Controllers;

namespace Lee_Final.Tests
{
    [TestClass()]
    public class JwtAuthenticationManagerTests
    {
        [TestMethod()]
        public void AuthenticatTest()
        {
            JwtAuthenticationManager manager = new JwtAuthenticationManager("fakekeynotlegit1111!");

            AuthenticationController.User user = new AuthenticationController.User
            {
                username = "test1",
                password = "pwd"
            };

            var ret = manager.Authenticat(user.username, user.password);
            Assert.IsNotNull(ret);
        }
    }
}