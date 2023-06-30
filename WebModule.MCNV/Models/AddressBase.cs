using WebApp.Core;
using System.Linq;
using DevExpress.Xpo;

namespace WebModule.MCNV.Models
{
   public class AddressBase
   {
      [Caption("Tỉnh/Thành phố"), Template("Province"), Required]
      public int? Province { get; set; }
      [Caption("Quận/Huyện"), Template("District"), Required]
      public int? District { get; set; }
      [Caption("Phường/Xã"), Template("Ward"), Required(OnlyIf = "WardRequired")]
      public int? Ward { get; set; }
      [Caption("Số nhà, tên đường...")]
      public string Address { get; set; }

      public bool WardRequired()
      {
         var db = ApplicationDbContext.Current;
         var query = from el in db.Session.Query<DataModel.Ward>()
                     where el.District.Oid == District
                     select el;
         return query.Count() > 0;
      }
   }
}