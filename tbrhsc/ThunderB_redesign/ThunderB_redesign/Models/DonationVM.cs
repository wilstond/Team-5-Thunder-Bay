using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Author: Wilston Dsouza
//Purpose: Mobile Development Final Project

namespace ThunderB_redesign.Models
{
    
    public class DonationVM
    {
        LinqDataContext objDataContext = new LinqDataContext();

        public int dnr_id {get; set; }

        [DisplayName("Donor Email")]
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string dnr_email { get; set; }
        
        [DisplayName("Donor Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string dnr_name { get; set; }

        [DisplayName("Donor Phone")]
        [Required(ErrorMessage="Please enter your phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter a valid phone number")]
        public string dnr_phone { get; set; }


        [DisplayName("Donor Apartment Number")]
        [Required(ErrorMessage = "Please enter your apartment number")]
        public string dnr_apt_no { get; set; }


        [DisplayName("Donor street name")]
        [Required(ErrorMessage = "Please enter your street name")]
        public string dnr_street { get; set; }


        [DisplayName("Donor City")]
        [Required(ErrorMessage = "Please enter your city")]
        public string dnr_city { get; set; }

        [DisplayName("Donor Province")]
        [Required(ErrorMessage = "Please enter your province")]
        public string dnr_province { get; set; }


        [DisplayName("Donor Country")]
        [Required(ErrorMessage = "Please enter your country")]
        public string dnr_country { get; set; }


        [DisplayName("Donor Postal Code")]
        [Required(ErrorMessage = "Please enter your postal code")]
        [RegularExpression(@"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage = "Please enter a valid postal code")]
        public string dnr_postal_code { get; set; }
        

        public int dtn_id {get; set;}
        public int dtn_dnr_id {get; set;}
        [DisplayName("Donor Amount")]
        [Required(ErrorMessage = "Please enter the amount")]
        [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$", ErrorMessage = "Enter a valid amount")]
        public decimal dtn_amount { get; set; }

        public System.DateTime dtn_date { get; set; }

        public IEnumerable<donation> getAllDonations()
        {
            var allDonations = objDataContext.donations.Select(x => x);
            return allDonations;
        }

        public IEnumerable<donation> getDonationsByDonorID(int id)
        {
            var allDonations = objDataContext.donations.Where(x => x.dtn_dnr_id == id);
            return allDonations;
        }

        public IEnumerable<donor> getAllDonors()
        {
            var allDonors = objDataContext.donors.Select(x => x);
            return allDonors;
        }

        public donor getDonorDetailsByID(int id)
        {
            var donorDetails = objDataContext.donors.SingleOrDefault(x => x.dnr_id == id);
            return donorDetails;
        }

        public donor getDonorDetailsByEmail(string email)
        {
            var donorDetails = objDataContext.donors.SingleOrDefault(x => x.dnr_email == email);
            return donorDetails;
        }

        public List<DonationVM> getAllDonationDetails()
        {
            var allDonations = getAllDonations();
            List<DonationVM> allDonationsWithDetails = new List<DonationVM>();
            
            foreach(var dtn in allDonations) 
            {
                var donorInfo = getDonorDetailsByID(dtn.dtn_dnr_id);
                DonationVM donationWithDonor = new DonationVM();
                donationWithDonor.dtn_id = dtn.dtn_id;
                donationWithDonor.dtn_dnr_id = dtn.dtn_dnr_id;
                donationWithDonor.dtn_amount = dtn.dtn_amount;
                donationWithDonor.dtn_date = dtn.dtn_date;
                donationWithDonor.dnr_id = donorInfo.dnr_id;
                donationWithDonor.dnr_name = donorInfo.dnr_name;
                donationWithDonor.dnr_email = donorInfo.dnr_email;
                donationWithDonor.dnr_phone = donorInfo.dnr_phone;
                donationWithDonor.dnr_apt_no = donorInfo.dnr_apt_no;
                donationWithDonor.dnr_street = donorInfo.dnr_street;
                donationWithDonor.dnr_city = donorInfo.dnr_city;
                donationWithDonor.dnr_province = donorInfo.dnr_province;
                donationWithDonor.dnr_country = donorInfo.dnr_country;
                donationWithDonor.dnr_postal_code = donorInfo.dnr_postal_code;
                
                allDonationsWithDetails.Add(donationWithDonor);

            }

            return allDonationsWithDetails;

        }

        public bool addDonationInfo()
        {
            donor donorInfo = new donor();
            donorInfo = getDonorDetailsByEmail(dnr_email);
            if (donorInfo == null)
            {
                donorInfo = new donor();
                donorInfo.dnr_email = dnr_email;
                donorInfo.dnr_name = dnr_name;
                donorInfo.dnr_phone = dnr_phone;
                donorInfo.dnr_apt_no = dnr_apt_no;
                donorInfo.dnr_street = dnr_street;
                donorInfo.dnr_city = dnr_city;
                donorInfo.dnr_province = dnr_province;
                donorInfo.dnr_country = dnr_country;
                donorInfo.dnr_postal_code = dnr_postal_code;
                commitInsertDonor(donorInfo);
            }

            donation donationInfo = new donation();
            donationInfo.dtn_dnr_id = donorInfo.dnr_id;
            donationInfo.dtn_amount = dtn_amount;
            donationInfo.dtn_date = DateTime.Now;
            commitInsertDonation(donationInfo);

            return true;
        }

        public bool commitInsertDonation(donation objDonation) 
        {
            objDataContext = new LinqDataContext();

            using (objDataContext) 
            {
                objDataContext.donations.InsertOnSubmit(objDonation);
                objDataContext.SubmitChanges();
                return true;
            }
        }

        public bool commitInsertDonor(donor objDonor)
        {
            objDataContext = new LinqDataContext();

            using (objDataContext)
            {
                objDataContext.donors.InsertOnSubmit(objDonor);
                objDataContext.SubmitChanges();
                return true;
            }
        }

    }

}