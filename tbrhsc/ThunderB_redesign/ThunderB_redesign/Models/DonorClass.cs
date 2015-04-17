using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Author: Wilston Dsouza
//Purpose: Mobile Development Final Project

namespace ThunderB_redesign.Models
{
    public class DonorClass
    {

        LinqDataContext objLinq = new LinqDataContext();

        public IEnumerable<donor> getDonors()
        {
            var allDonors = objLinq.donors.Select(x => x);
            return allDonors;
        }

        public donor getDonorById(int _id)
        {
            var objDonor = objLinq.donors.SingleOrDefault(x => x.dnr_id == _id);
            return objDonor;
        }

        public bool commitUpdate(int _id, donor objDonor)
        {
            using (objLinq)
            {
                var donorUpd = objLinq.donors.Single(x => x.dnr_id == _id);
                donorUpd.dnr_email = objDonor.dnr_email;
                donorUpd.dnr_name = objDonor.dnr_name;
                donorUpd.dnr_phone = objDonor.dnr_phone;
                donorUpd.dnr_apt_no = objDonor.dnr_apt_no;
                donorUpd.dnr_street = objDonor.dnr_street;
                donorUpd.dnr_city = objDonor.dnr_city;
                donorUpd.dnr_province = objDonor.dnr_province;
                donorUpd.dnr_country = objDonor.dnr_country;
                donorUpd.dnr_postal_code = objDonor.dnr_postal_code;
                objLinq.SubmitChanges();
                return true;
            }
        }

    }
}