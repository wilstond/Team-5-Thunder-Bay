using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ThunderB_redesign.Models;

namespace ThunderB_redesign.Models
{
    public class AppointmentLinqClass
    {
        LinqDataContext db = new LinqDataContext();

        public doctor getDoctorByUserId(int user_id)
        {
            var selDoctor = db.doctors.Where(x => x.user_id == user_id).SingleOrDefault();
            return selDoctor;

        }

        public IQueryable<appointment> getAllAppointments()
        {
                var AllAppointments = db.appointments.Select(x => x);
                return AllAppointments;
        }

        public IQueryable<appointment> getAppointmentsbyDr(int _dr_id)
        {
            
                var ApptByDoctor = db.appointments.Where(x => x.dr_id == _dr_id).Select(x => x);
                return ApptByDoctor;
        }

        public appointment getAppointmentById(int _apt_id)
        {
                var selAppt = db.appointments.Where(x => x.apt_id == _apt_id).SingleOrDefault();
                return selAppt;
        }

        public bool commitInsert(appointment _apt)
        {
                db.appointments.InsertOnSubmit(_apt);
                db.SubmitChanges();
                return true;
        }

        public bool commitUpdate(int _apt_id, int _dr_id, DateTime _date_req,
            DateTime _date_book, string _time_book, string _pat_name, string _pat_phone, string _pat_email, 
            string _pat_addr, string _pat_ohp, string _fam_dr_name, string _fam_dr_phone, 
            string _app_status)
        {
                var selAppt = getAppointmentById(_apt_id);
                selAppt.dr_id = _dr_id;
                selAppt.date_req = _date_req;
                selAppt.date_book = _date_book;
                selAppt.time_book = _time_book;
                selAppt.pat_name = _pat_name;
                selAppt.pat_phone = _pat_phone;
                selAppt.pat_email = _pat_email;
                selAppt.pat_address = _pat_addr;
                selAppt.pat_ohip = _pat_ohp;
                selAppt.fam_dr_name = _fam_dr_name;
                selAppt.fan_dr_phone = _fam_dr_phone;
                selAppt.app_status = _app_status;

                db.SubmitChanges();
                return true;

        }

        public bool commitDelete(int _apt_id)
        {
                var delPage = getAppointmentById(_apt_id);
                db.appointments.DeleteOnSubmit(delPage);
                db.SubmitChanges();
                return true;
        }
    }

}