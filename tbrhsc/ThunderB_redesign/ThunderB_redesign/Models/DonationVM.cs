using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThunderB_redesign.Models
{
    
    public class DonationVM
    {
        LinqDataContext objDataContext = new LinqDataContext();

        public int dnr_id {get; set; }

        [DisplayName("Donor Email")]
        [Required(ErrorMessage = "Please enter your email")]
        public string dnr_email { get; set; }
        
        [DisplayName("Donor Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string dnr_name { get; set; }

        public System.Nullable<char> _dnr_anonymous { get; set; }

        public int dtn_id {get; set;}
        public int dtn_dnr_id {get; set;}
        [DisplayName("Donor Amount")]
        [Required(ErrorMessage = "Please enter the amount")]
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
                commitInsertDonor(donorInfo);
            }

            donation donationInfo = new donation();
            donationInfo.dtn_dnr_id = donorInfo.dnr_id;
            donationInfo.dtn_amount = dtn_amount;
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