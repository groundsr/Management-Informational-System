using MSI.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.Tests
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {

        }

        public void Seed(PoliceContext policeContext)
        {
            policeContext.Database.EnsureDeleted();
            policeContext.Database.EnsureCreated();
        }
    }
}
