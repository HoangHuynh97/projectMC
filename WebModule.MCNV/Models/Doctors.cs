using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using WebApp.Core;

namespace WebModule.MCNV.Models
{
   public class Doctors
   {
      public class Doctor
      {
         public int Id { get; set; }
         public string Name { get; set; }
         public string Specialize { get; set; }

         public Doctor() { }
         public Doctor(DataModel.Doctor model)
         {
            Id = model.Oid;
            Name = model.Name;
            Specialize = model.Specialize?.Name;
         }
      }

      public string Name { get; set; }
      public int Facility { get; set; }
      public List<Doctor> Items { get; set; }
      public int Height { get; set; }

      public Doctors(string name)
      {
         Name = name;
         Items = new List<Doctor>();
      }
      public Doctors(string name, int facility, IEnumerable<DataModel.Doctor> collection)
      {
         Name = name;
         Facility = facility;
         Items = collection.Select(it => new Doctor(it)).ToList();
      }

      public void Update(XPCollection<DataModel.Doctor> collection)
      {
         //remove
         var dels = collection.Where(it => Items.All(d => d.Id != it.Oid)).ToList();
         foreach (var d in dels) collection.Remove(d);
         //add, double check in current facility
         var adds = Items.Where(it => collection.All(d => d.Oid != it.Id)).Select(it => it.Id).ToList();
         var doctors = (from el in collection.Session.QueryByCompany<DataModel.Doctor>()
                        where adds.Contains(el.Oid) && el.Facility.Oid == Facility
                        select el).ToList();
         collection.AddRange(doctors);
      }

      public List<Doctor> GetList()
      {
         var db = ApplicationDbContext.Current;
         var query = from el in db.Session.QueryByCompany<DataModel.Doctor>()
                     where el.Facility.Oid == Facility
                     select new Doctor() { Id = el.Oid, Name = el.Name, Specialize = el.Specialize.Name };
         var list = query.ToList();
         list = list.OrderByDescending(it => Items.Any(d => d.Id == it.Id)).ThenBy(it => it.Name).ToList();
         return list;
      }

      public List<string> GetNames()
      {
         var db = ApplicationDbContext.Current;
         var ids = Items.Select(it => it.Id).ToList();
         var query = from el in db.Session.QueryByCompany<DataModel.Doctor>()
                     where el.Facility.Oid == Facility && ids.Contains(el.Oid)
                     orderby el.Name
                     select new { el.Name, Specialize = el.Specialize.Name };
         return query.Select(it => string.Format("{0} ({1})", it.Name, it.Specialize)).ToList();
      }

      public static List<Doctor> GetListByFacility(int facility)
      {
         var db = ApplicationDbContext.Current;
         var query = from el in db.Session.QueryByCompany<DataModel.Doctor>()
                     where el.Facility.Oid == facility
                     orderby el.Name
                     select new Doctor() { Id = el.Oid, Name = el.Name, Specialize = el.Specialize.Name };
         return query.ToList();
      }
   }
}