using MIS.DataAccess.Abstractions;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.DataAccess
{
    public class EFPoliceSectionRepository : EFRepository<PoliceSection>, IPoliceSectionRepository
    {
        private readonly PoliceContext context;

        public EFPoliceSectionRepository(PoliceContext context) : base (context)
        {
            this.context = context;
        }

        public IEnumerable<PoliceSection> GetAll()
        {
            return (_context.PoliceSections.ToList());
        }


        public PoliceSection GetPoliceSectionByName(string name)
        {
            return (_context.PoliceSections
                 .Where(x => x.Name.Contains(name))
                 .FirstOrDefault());

        }


        public int GetPoliceSectionNumber(string policeSectionName)
        {
            string stringValue = "";
            int parseValue = 0;
            bool flag = false;
            int p = 1;

            for (int i = 0; i < policeSectionName.Length; i++)
            {
                if (policeSectionName[i] == '0')
                {
                    stringValue += policeSectionName[i];
                    flag = true;
                }
                if (policeSectionName[i] == '1')
                {
                    stringValue += policeSectionName[i];
                    flag = true;

                }
                if (policeSectionName[i] == '2')
                {
                    stringValue += policeSectionName[i];
                    flag = true;

                }
                if (policeSectionName[i] == '3')
                {
                    stringValue += policeSectionName[i];
                    flag = true;

                }
                if (policeSectionName[i] == '4')
                {
                    stringValue += policeSectionName[i];
                    flag = true;
                }
                if (policeSectionName[i] == '5')
                {
                    stringValue += policeSectionName[i];
                    flag = true;

                }
                if (policeSectionName[i] == '6')
                {
                    stringValue += policeSectionName[i];
                    flag = true;

                }
                if (policeSectionName[i] == '7')
                {
                    stringValue += policeSectionName[i];
                    flag = true;

                }
                if (policeSectionName[i] == '8')
                {
                    stringValue += policeSectionName[i];
                    flag = true;

                }
                if (policeSectionName[i] == '9')
                {
                    stringValue += policeSectionName[i];
                    flag = true;
                }
            }

            return (Int32.Parse(stringValue));

        }

    }
}
