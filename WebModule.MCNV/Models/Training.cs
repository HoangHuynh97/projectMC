using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using WebModule.MCNV.DataModel;

namespace WebModule.MCNV.Models
{
   public class Training
   {
      public class ListInfo
      {
         public bool ShowFilter { get; set; }
         public List<Training> GetData()
         {
            var db = ApplicationDbContext.Current;
            var ids = (from f in db.Session.QueryFacility()
                       select f.Oid).ToList();
            var programs = (from p in db.Session.QueryProgram()
                            from d in p.Doctors
                            where ids.Contains(d.Doctor.Facility.Oid)
                            select new
                            {
                               Id = d.Doctor.Oid,
                               Area = d.Doctor.Facility.Area.Name,
                               Province = d.Doctor.Facility.Province.Name,
                               Code = d.Doctor.Code,
                               Name = d.Doctor.Name,
                               Gender = d.Doctor.Gender.Name,
                               DateBirth = d.Doctor.DateBirth,
                               Phone = d.Doctor.Phone,
                               Email = d.Doctor.Email,
                               Facility = d.Doctor.Facility.Name,
                               Qualification = d.Doctor.Qualification.Name,
                               Title = d.Doctor.Title.Name,
                               Position = d.Doctor.Position.Name,
                               Specialize = d.Doctor.Specialize.Name,
                               Program = p.Name,
                               EndDate = p.DateEnd,
                            }).ToList();
            var today = DateTime.Today;
            var tmp = new Dictionary<int, Training>();
            foreach (var p in programs)
            {
               if (!tmp.ContainsKey(p.Id))
                  tmp.Add(p.Id, new Training()
                  {
                     Id = p.Id,
                     Area = p.Area,
                     Province = p.Province,
                     Code = p.Code,
                     Name = p.Name,
                     Gender = p.Gender,
                     DateBirth = p.DateBirth,
                     Phone = p.Phone,
                     Email = p.Email,
                     Facility = p.Facility,
                     Qualification = p.Qualification,
                     Title = p.Title,
                     Position = p.Position,
                     Specialize = p.Specialize,
                  });
               var t = tmp[p.Id];
               if (p.EndDate.HasValue && p.EndDate < today)
               {
                  if (string.IsNullOrEmpty(t.Completed))
                     t.Completed = p.Program;
                  else
                     t.Completed += ", " + p.Program;
               }
               else
               {
                  if (string.IsNullOrEmpty(t.Current))
                     t.Current = p.Program;
                  else
                     t.Current += ", " + p.Program;
               }

            }
            return tmp.Values.OrderBy(d => d.Facility).ThenBy(d => d.Name).ToList();
         }
      }

      public int Id { get; set; }
      public string Area { get; set; }
      public string Province { get; set; }
      public string Code { get; set; }
      public string Name { get; set; }
      public string Gender { get; set; }
      public DateTime? DateBirth { get; set; }
      public string Phone { get; set; }
      public string Email { get; set; }
      public string Facility { get; set; }
      public string Qualification { get; set; }
      public string Title { get; set; }
      public string Position { get; set; }
      public string Specialize { get; set; }
      public string Completed { get; set; }
      public string Current { get; set; }
   }
}