using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;
using DevExpress.Xpo;
using DevExpress.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using System.Reflection;
using System.Drawing;
using DevExpress.Web.Mvc;

namespace WebModule.MCNV.Models
{
   public static class Helper
   {
      /// <summary>
      /// Add a column to a collection and return that collection
      /// </summary>
      public static FilterControlColumnCollection AddColumn(this FilterControlColumnCollection collection, string field, string name, FilterControlColumnType type = FilterControlColumnType.Default)
      {
         collection.Add(new FilterControlColumn()
         {
            PropertyName = field,
            DisplayName = name,
            ColumnType = type
         });
         return collection;
      }

      /// <summary>
      /// Add a specific column and return collection
      /// </summary>
      public static FilterControlColumnCollection AddColumn(this FilterControlColumnCollection collection, Func<FilterControlColumn> creator)
      {
         collection.Add(creator());
         return collection;
      }

      /// <summary>
      /// Add a group column to a collection and return the collection of created column
      /// </summary>
      public static FilterControlColumnCollection AddComplexColumn(this FilterControlColumnCollection collection, string field, string name)
      {
         var col = new FilterControlComplexTypeColumn()
         {
            PropertyName = field,
            DisplayName = name,
         };
         collection.Add(col);
         return col.Columns;
      }

      private static int LockedId = 0;
      public static MvcHtmlString LockedText<T>(this HtmlHelper<T> html, string caption, string value, bool multiline = false)
      {
         LockedId++;
         if (!multiline)
            return html.DevExpress().TextBox(st =>
            {
               st.Name = "LockedText" + LockedId;
               st.Width = Unit.Percentage(100);
               st.Properties.Caption = caption;
               st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
               st.Text = value;
               st.Enabled = false;
            }).GetHtml();
         else
            return html.DevExpress().Memo(st =>
            {
               st.Name = "LockedText" + LockedId;
               st.Width = Unit.Percentage(100);
               st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
               st.Properties.Caption = caption;
               st.Text = value;
               st.Enabled = false;
            }).GetHtml();
      }

      public static MvcHtmlString LockedText<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool multiline = false)
      {
         var caption = GetCaptionExpression(expression);
         var value = expression.Compile().Invoke(html.ViewData.Model);
         var s = "";
         if (value != null)
         {
            if (value is DateTime)
               s = Convert.ToDateTime(value).ToShortDateString();
            else
               s = value.ToString();
         }
         return html.LockedText(caption, s, multiline);
      }

      private static string GetCaptionExpression<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
      {
         if (!(expression.Body is MemberExpression member))
            throw new ArgumentException(string.Format(
                "Expression '{0}' refers to a method, not a property.",
                expression.ToString()));

         var caption = member.Member.GetCustomAttribute<CaptionAttribute>()?.DisplayName ?? member.Member.Name;
         return Language.T(caption);
      }

      /// <summary>
      /// Query list object base on company
      /// </summary>
      /// <typeparam name="T">Company object</typeparam>
      public static IQueryable<T> QueryByCompany<T>(this Session session) where T : DataModel.CompanyObject
      {
         var db = ApplicationDbContext.Current;
         var query = from el in session.Query<T>()
                     where el.Company == db.CompanyId
                     select el;
         return query;
      }

      /// <summary>
      /// Get object by its id, also check current company
      /// </summary>
      /// <typeparam name="T">Company object</typeparam>
      public static T FetchByCompany<T>(this Session session, int? id) where T : DataModel.CompanyObject
      {
         if (id == null) return null;
         var query = from el in session.QueryByCompany<T>()
                     where el.Oid == id
                     select el;
         return query.SingleOrDefault();
      }

      /// <summary>
      /// Get factiories base on user permission
      /// </summary>
      public static IQueryable<DataModel.Facility> QueryFacility(this Session session)
      {
         var db = ApplicationDbContext.Current;
         var pm = session.FetchByCompany<DataModel.PermissionData>(db.UserModel.DataId);
         var query = session.QueryByCompany<DataModel.Facility>();
         var curId = db.UserModel.GroupId;
         switch (pm.GetDataType())
         {
            case PermissionDataType.Admin:
               break;
            case PermissionDataType.Provinces:
               query = from el in query where el.Oid == curId || pm.GetProvinceIds().Contains(el.Province.Oid) select el;
               break;
            case PermissionDataType.Area:
               var provinceIds = from el in session.QueryByCompany<DataModel.Area>()
                                 where el.Oid == pm.GetAreaId()
                                 from p in el.Provinces
                                 select p.Oid;
               query = from el in query where el.Oid == curId || provinceIds.Contains(el.Province.Oid) select el;
               break;
            default:
               query = from el in query where el.Oid == curId select el;
               break;
         }
         return query;
      }

      /// <summary>
      /// Fetch single Facility object base on user permission
      /// </summary>
      public static DataModel.Facility FetchFacility(this Session session, int? id)
      {
         if (id == null) return null;
         return (from el in session.QueryFacility() where el.Oid == id select el).SingleOrDefault();
      }


      /// <summary>
      /// Get Medicals base on user permission
      /// </summary>
      public static IQueryable<DataModel.Medical> QueryMedical(this Session session)
      {
         var db = ApplicationDbContext.Current;
         var query = session.QueryByCompany<DataModel.Medical>();
         var pm = session.FetchByCompany<DataModel.PermissionData>(db.UserModel.DataId);
         switch (pm.GetDataType())
         {
            case PermissionDataType.Admin:
               break;
            case PermissionDataType.Owner:
               var shared = from el in session.QueryByCompany<DataModel.SharedData>()
                            where el.Accept && el.UserRequest.Oid == db.UserId
                            join m in session.QueryByCompany<DataModel.Medical>()
                            on new { Facility = el.FacilityReceive.Oid, Patient = el.Patient.Oid }
                            equals new { Facility = m.Facility.Oid, Patient = m.Patient.Oid }
                            select m.Oid;

               query = from el in query where el.UserCreate == db.UserId || shared.Contains(el.Oid) select el;
               break;
            case PermissionDataType.Facility:
            case PermissionDataType.Provinces:
            case PermissionDataType.Area:
               var facilityIds = from el in session.QueryFacility() select el.Oid;
               var sharedbyFacility = from el in session.QueryByCompany<DataModel.SharedData>()
                            where el.Accept && facilityIds.Contains(el.FacilityRequest.Oid)
                            join m in session.QueryByCompany<DataModel.Medical>()
                            on new { Facility = el.FacilityReceive.Oid, Patient = el.Patient.Oid }
                            equals new { Facility = m.Facility.Oid, Patient = m.Patient.Oid }
                            select m.Oid;

               query = from el in query where facilityIds.Contains(el.Facility.Oid) || sharedbyFacility.Contains(el.Oid) 
                       select el;
               break;
            default:
               query = from el in query where 1 == 0 select el;
               break;
         }
         return query;
      }

      /// <summary>
      /// Fetch single Medical object base on user permission
      /// </summary>
      public static DataModel.Medical FetchMedical(this Session session, int? id)
      {
         if (id == null) return null;
         return (from el in session.QueryMedical() where el.Oid == id select el).SingleOrDefault();
      }

      /// <summary>
      /// Get Patients base on user permission
      /// </summary>
      public static IQueryable<DataModel.Patient> QueryPatient(this Session session)
      {
         var db = ApplicationDbContext.Current;
         var query = session.QueryByCompany<DataModel.Patient>();
         var pm = session.FetchByCompany<DataModel.PermissionData>(db.UserModel.DataId);
         switch (pm.GetDataType())
         {
            case PermissionDataType.Admin:
               break;
            case PermissionDataType.Owner:
               var shared = from el in db.Session.QueryByCompany<DataModel.SharedData>()
                            where el.Accept && el.UserRequest.Oid == db.UserId && el.FacilityRequest.Oid == db.UserModel.GroupId
                            select el.Patient.Oid;
               query = from el in query where el.UserCreate == db.UserId || shared.Contains(el.Oid) select el;
               break;
            case PermissionDataType.Facility:
            case PermissionDataType.Provinces:
            case PermissionDataType.Area:
               var facilityIds = from el in session.QueryFacility() select el.Oid;
               var sharedByFacility = from el in db.Session.QueryByCompany<DataModel.SharedData>()
                            where el.Accept && facilityIds.Contains(el.FacilityRequest.Oid)
                            select el.Patient.Oid;
               query = from el in query where facilityIds.Contains(el.Facility.Oid) || sharedByFacility.Contains(el.Oid)
                       select el;
               break;
            default:
               query = from el in query where 1 == 0 select el;
               break;
         }
         return query;
      }

      /// <summary>
      /// Fetch single Patient object base on user permission
      /// </summary>
      public static DataModel.Patient FetchPatient(this Session session, int? id)
      {
         if (id == null) return null;
         return (from el in session.QueryPatient() where el.Oid == id select el).SingleOrDefault();
      }

      /// <summary>
      /// Get Doctors base on user permission
      /// </summary>
      public static IQueryable<DataModel.Doctor> QueryDoctor(this Session session)
      {
         var db = ApplicationDbContext.Current;
         var query = session.QueryByCompany<DataModel.Doctor>();
         var pm = session.FetchByCompany<DataModel.PermissionData>(db.UserModel.DataId);
         switch (pm.GetDataType())
         {
            case PermissionDataType.Admin:
               break;
            case PermissionDataType.Owner:
               query = from el in query where el.UserCreate == db.UserId select el;
               break;
            case PermissionDataType.Facility:
            case PermissionDataType.Provinces:
            case PermissionDataType.Area:
               var facilityIds = from el in session.QueryFacility() select el.Oid;
               query = from el in query where facilityIds.Contains(el.Facility.Oid) select el;
               break;
            default:
               query = from el in query where 1 == 0 select el;
               break;
         }
         return query;
      }

      /// <summary>
      /// Fetch single Doctor object base on user permission
      /// </summary>
      public static DataModel.Doctor FetchDoctor(this Session session, int? id)
      {
         if (id == null) return null;
         return (from el in session.QueryDoctor() where el.Oid == id select el).SingleOrDefault();
      }

      /// <summary>
      /// Get Programs base on user permission
      /// </summary>
      public static IQueryable<DataModel.Program> QueryProgram(this Session session)
      {
         var db = ApplicationDbContext.Current;
         var query = session.QueryByCompany<DataModel.Program>();
         return query;
      }

      /// <summary>
      /// Fetch single Program object base on user permission
      /// </summary>
      public static DataModel.Program FetchProgram(this Session session, int? id)
      {
         if (id == null) return null;
         return (from el in session.QueryProgram() where el.Oid == id select el).SingleOrDefault();
      }

      /// <summary>
      /// Get SharedDatas base on user permission, facility received
      /// </summary>
      public static IQueryable<DataModel.SharedData> QuerySharedData(this Session session)
      {
         var db = ApplicationDbContext.Current;
         var query = session.QueryByCompany<DataModel.SharedData>();
         var pm = session.FetchByCompany<DataModel.PermissionData>(db.UserModel.DataId);
         switch (pm.GetDataType())
         {
            case PermissionDataType.Admin:
               break;
            case PermissionDataType.Facility:
            case PermissionDataType.Provinces:
            case PermissionDataType.Area:
               var facilityIds = from el in session.QueryFacility() select el.Oid;
               query = from el in query where facilityIds.Contains(el.FacilityReceive.Oid) select el;
               break;
            default:
               query = from el in query where 1 == 0 select el;
               break;
         }
         return query;
      }

      /// <summary>
      /// Fetch single SharedData object base on user permission, facility received
      /// </summary>
      public static DataModel.SharedData FetchSharedData(this Session session, int? id)
      {
         if (id == null) return null;
         return (from el in session.QuerySharedData() where el.Oid == id select el).SingleOrDefault();
      }

      /// <summary>
      /// Get PermissionDatas base on user permission
      /// </summary>
      public static IQueryable<DataModel.PermissionData> QueryPermissionData(this Session session)
      {
         var db = ApplicationDbContext.Current;
         var query = session.QueryByCompany<DataModel.PermissionData>();
         var pm = session.FetchByCompany<DataModel.PermissionData>(db.UserModel.DataId);
         switch (pm.GetDataType())
         {
            case PermissionDataType.Admin:
               break;
            case PermissionDataType.Owner:
               query = from el in query
                       where el.DataType == PermissionDataType.Owner
                       select el;
               break;
            case PermissionDataType.Facility:
               query = from el in query
                       where el.DataType == PermissionDataType.Owner || el.DataType == PermissionDataType.Facility
                       select el;
               break;
            case PermissionDataType.Provinces:
            case PermissionDataType.Area:
               query = from el in query
                       where el.DataType == PermissionDataType.Owner || el.DataType == PermissionDataType.Facility || el.Oid == pm.Oid
                       select el;
               break;
            default:
               query = from el in query where 1 == 0 select el;
               break;
         }
         return query;
      }

      /// <summary>
      /// Fetch single PermissionData object base on user permission
      /// </summary>
      public static DataModel.PermissionData FetchPermissionData(this Session session, int? id)
      {
         if (id == null) return null;
         return (from el in session.QueryPermissionData() where el.Oid == id select el).SingleOrDefault();
      }

      public static PermissionDataType GetDataType(this DataModel.PermissionData pm)
      {
         if (ApplicationDbContext.Current.IsInRole(WebApp.Constant.Role.Admin)) return PermissionDataType.Admin;
         if (pm == null) return PermissionDataType.None;
         return pm.DataType;
      }
      public static List<int> GetProvinceIds(this DataModel.PermissionData pm)
      {
         var res = new List<int>();
         if (pm != null && pm.DataType == PermissionDataType.Provinces && !string.IsNullOrEmpty(pm.ParameterIds))
            res.AddRange(pm.ParameterIds.Split(';').Select(s => Convert.ToInt32(s)));
         return res;
      }
      public static int GetAreaId(this DataModel.PermissionData pm)
      {
         var res = 0;
         if (pm != null && pm.DataType == PermissionDataType.Area && !string.IsNullOrEmpty(pm.ParameterIds))
            res = Convert.ToInt32(pm.ParameterIds);
         return res;
      }


      /// <summary>
      /// Default setting for combobox
      /// </summary>
      public static void DefaultSettings(this ComboBoxSettings st)
      {
         st.Width = Unit.Percentage(100);
         st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
         st.ShowModelErrors = true;
         st.Properties.ValidationSettings.Display = Display.Dynamic;
         st.Properties.AllowNull = false;
         st.Properties.IncrementalFilteringDelay = 800;
         st.Properties.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
         st.Properties.DropDownStyle = DropDownStyle.DropDownList;
         st.Properties.SettingsAdaptivity.Mode = DropDownEditAdaptivityMode.OnWindowInnerWidth;
         st.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.Never;
         st.Properties.EnableClientSideAPI = true;
      }

      public static void DefaultSettings(this TextBoxSettings st)
      {
         st.Width = Unit.Percentage(100);
         st.Properties.CaptionSettings.Position = EditorCaptionPosition.Top;
         st.ShowModelErrors = true;
         st.Properties.ValidationSettings.Display = Display.Dynamic;
         st.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
         st.Properties.EnableClientSideAPI = true;
         st.AutoCompleteType = AutoCompleteType.Disabled;
      }
   }
}