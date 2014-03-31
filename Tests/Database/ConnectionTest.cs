using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database;

namespace DIPS.Tests.Database
{
    [TestClass]
    public class ConnectionTest
    {
        [TestMethod]
        public void Test_ConnectionManager()
        {
            String Catalog = ConnectionManager.Catalog;
            String Data =  ConnectionManager.DataSource;
            String Security = ConnectionManager.Security;
            String Extra = ConnectionManager.Extra;
            String Conn = "Data Source = " + Data + "; Initial Catalog = " +
                            Catalog + "; Integrated Security = " + Security
                            + "; " + Extra;
            Assert.AreEqual(ConnectionManager.getConnection, Conn);
        }

        [TestMethod]
        public void Test_RebuildConnection()
        {
            String Catalog = ConnectionManager.Catalog;
            String Data =  ConnectionManager.DataSource;
            String Security = ConnectionManager.Security;
            String Extra = ConnectionManager.Extra;
            String Conn = ConnectionManager.getConnection;
            String newConn = "Data Source = MMU; Initial Catalog = Medical; Integrated Security = True; ";

            ConnectionManager.DataSource = "MMU";
            ConnectionManager.Catalog = "Medical";
            ConnectionManager.Security = "True";
            ConnectionManager.Extra = "";
            Assert.AreEqual(newConn,ConnectionManager.getConnection);

            ConnectionManager.DataSource = Data;
            ConnectionManager.Catalog = Catalog;
            ConnectionManager.Security = Security;
            ConnectionManager.Extra = Extra;
            Assert.AreEqual(Conn, ConnectionManager.getConnection);
        }
    }
}
