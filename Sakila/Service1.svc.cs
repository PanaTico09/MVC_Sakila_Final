using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Sakila
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public List<Models.StaffModel> SelectStaff()
        {
            sakilaEntities1 context = new sakilaEntities1();
            var ResultList = (from l in context.staff
                              select new Models.StaffModel
                              {
                                  ID = l.staff_id,
                                  firstName = l.first_name,
                                  lastName = l.last_name
                              }).ToList();
            return ResultList.ToList();
        }

        public bool AddStaff(staff newStaff)
        {
            sakilaEntities1 context = new sakilaEntities1();
            context.staff.Add(newStaff);
            context.SaveChanges();
            return true;
        }

        public bool UpdateStaff(staff StaffUpdated)
        {
            sakilaEntities1 context = new sakilaEntities1();
            var _staff = (from s in context.staff
                          where s.staff_id == StaffUpdated.staff_id
                          select s).FirstOrDefault();
            _staff.first_name = StaffUpdated.first_name;
            context.SaveChanges();
            return true;

        }
    }
}
 