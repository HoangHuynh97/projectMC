using DevExpress.Data.Filtering;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Core;

namespace WebModule.MCNV.Controllers
{
   [Authorize]
   public class ComboboxController : BaseController
   {
      public ActionResult Area()
      {
         MVCxColumnComboBoxProperties cmb = new MVCxColumnComboBoxProperties()
         {
            ValueField = "Oid",
            ValueType = typeof(int)
         };
         cmb.Columns.Add("Code");
         cmb.Columns.Add("Name");
         cmb.CustomFiltering += (e) =>
         {
            string[] cols = { "Code", "Name" };
            e.FilterExpression = GroupOperator.Or(cols.Select(c =>
                  new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(c), e.Filter))
               ).ToString();
            e.CustomHighlighting = cols.ToDictionary(c => c, c => e.Filter);
         };
         cmb.BindList(Models.Area.ComboboxData());
         return GridExtensionBase.GetComboBoxCallbackResult(cmb);
      }

      public ActionResult Diagnostic(int? setId)
      {
         MVCxColumnComboBoxProperties cmb = new MVCxColumnComboBoxProperties()
         {
            ValueField = "Oid",
            ValueType = typeof(int)
         };
         cmb.Columns.Add("Name");
         cmb.BindList(Models.Diagnostic.ComboboxData());
         return GridExtensionBase.GetComboBoxCallbackResult(cmb);
      }

      public ActionResult Facility()
      {
         MVCxColumnComboBoxProperties cmb = new MVCxColumnComboBoxProperties()
         {
            ValueField = "Oid",
            ValueType = typeof(int)
         };
         cmb.Columns.Add("Oid");
         cmb.Columns.Add("Name");
         cmb.CustomFiltering += (e) =>
         {
            string[] cols = { "Oid", "Name", "Province" };
            e.FilterExpression = GroupOperator.Or(cols.Select(c =>
                  new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(c), e.Filter))
               ).ToString();
            e.CustomHighlighting = cols.ToDictionary(c => c, c => e.Filter);
         };
         cmb.BindList(Models.Facility.ComboboxData());
         return GridExtensionBase.GetComboBoxCallbackResult(cmb);
      }

      public ActionResult Patient()
      {
         MVCxColumnComboBoxProperties cmb = new MVCxColumnComboBoxProperties()
         {
            ValueField = "Oid",
            ValueType = typeof(int)
         };
         cmb.Columns.Add("Code");
         cmb.Columns.Add("Name");
         cmb.CustomFiltering += (e) =>
         {
            string[] cols = { "Code", "Name", "FullAddress", "Phone", "CMND", "CCCD", "BHYT" };
            e.FilterExpression = GroupOperator.Or(cols.Select(c =>
                  new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(c), e.Filter))
               ).ToString();
            e.CustomHighlighting = cols.ToDictionary(c => c, c => e.Filter);
         };
         cmb.BindList(Models.Patient.ComboboxData());
         return GridExtensionBase.GetComboBoxCallbackResult(cmb);
      }

      public ActionResult District(int? province)
      {
         MVCxColumnComboBoxProperties cmb = new MVCxColumnComboBoxProperties()
         {
            ValueField = "Oid",
            ValueType = typeof(int),
            TextField = "Name",
         };
         cmb.BindList(Models.District.ComboboxForProvince(province));
         return GridExtensionBase.GetComboBoxCallbackResult(cmb);
      }

      public ActionResult Ward(int? district)
      {
         MVCxColumnComboBoxProperties cmb = new MVCxColumnComboBoxProperties()
         {
            ValueField = "Oid",
            ValueType = typeof(int),
            TextField = "Name",
         };
         cmb.BindList(Models.Ward.ComboboxForDistrict(district));
         return GridExtensionBase.GetComboBoxCallbackResult(cmb);
      }
   }
}