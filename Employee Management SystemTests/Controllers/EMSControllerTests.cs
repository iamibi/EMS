using Microsoft.VisualStudio.TestTools.UnitTesting;
using Employee_Management_System.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Controllers.Tests
{
    [TestClass()]
    public class EMSControllerTests
    {
        private readonly EMSController controller = new EMSController();

        [TestMethod()]
        public void ITDepartmentViewTest()
        {
            System.Diagnostics.Debug.WriteLine("Hello");
            Assert.Fail();
        }
    }
}