using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Core
{
   [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
   public class FunctionAttribute : AuthorizeAttribute
   {
      private AuthorizationContext context;

      public FunctionAttribute(string name)
      {
         Name = name;
      }

      public string Name { get; private set; }

      protected override bool AuthorizeCore(HttpContextBase httpContext)
      {
         if (!base.AuthorizeCore(httpContext))
            return false;
         var module = context.Controller.GetType().Assembly.ManifestModule.Name;
         var logics = context.ActionDescriptor.GetCustomAttributes(typeof(LogicAttribute), false).Select(p => ((LogicAttribute)p).Name).ToArray();
         if (logics.Length > 0)
         {
            foreach (var l in logics)
            {
               if (!httpContext.CheckPermission(module, Name, l))
                  return false;
            }
            return true;
         }
         else
            return httpContext.CheckPermission(module, Name);
      }

      public override void OnAuthorization(AuthorizationContext filterContext)
      {
         context = filterContext;
         base.OnAuthorization(filterContext);
         if (filterContext.HttpContext.User.Identity.IsAuthenticated && !Roles.Contains(Constant.Role.System) && !ApplicationDbContext.Current.CompanyId.HasValue)
            filterContext.Result = new JavaScriptResult() { Script = "WebApp.selectCompany();" };
      }

      protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
      {
         if (string.IsNullOrEmpty(filterContext.HttpContext.Request.Form["DXCallbackName"]))
            filterContext.Result = new JavaScriptResult() { Script = "WebApp.AccessDeny();" };
         else
            filterContext.Result = new ContentResult() { Content = "/*DX*/WebApp.AccessDeny();" };
      }
   }

   [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
   public class LogicAttribute : Attribute
   {
      public LogicAttribute(string name)
      {
         Name = name;
      }

      public string Name { get; private set; }
   }

   [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
   public class AuditAttribute : Attribute
   {
      public string Name { get; private set; }
      public string PropertyKey { get; private set; }

      public AuditAttribute(string name, string propertyKey) : base()
      {
         Name = name;
         PropertyKey = propertyKey;
      }
   }

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute, IClientValidatable
   {
      public string OnlyIf { get; set; }
      public RequiredAttribute() : base()
      {
         ErrorMessage = "Không được để trống";
      }

      public override string FormatErrorMessage(string name)
      {
         return Language.T(ErrorMessage);
      }

      protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
      {
         if (!string.IsNullOrEmpty(OnlyIf))
         {
            try
            {
               var v = validationContext.ObjectType.GetMethod(OnlyIf).Invoke(validationContext.ObjectInstance, null);
               if (!Convert.ToBoolean(v)) return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }
            catch
            {
               throw new Exception("Error when call validate on server: " + OnlyIf);
            }
         }
         return base.IsValid(value, validationContext);
      }


      IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
      {
         var result = new ModelClientValidationRequiredRule(Language.T(ErrorMessage));
         result.ValidationType = "requiredif";
         if (!string.IsNullOrEmpty(OnlyIf))
         {
            result.ValidationParameters.Add("onlyif", OnlyIf);
         }
         yield return result;
      }
   }

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class EqualAttribute : System.ComponentModel.DataAnnotations.CompareAttribute, IClientValidatable
   {
      public string OnlyIf { get; set; }

      public EqualAttribute(string property, string message) : base(property)
      {
         ErrorMessage = message;
      }

      public override string FormatErrorMessage(string name)
      {
         return Language.T(ErrorMessage);
      }

      protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
      {
         if (!string.IsNullOrEmpty(OnlyIf))
         {
            try
            {
               var v = validationContext.ObjectType.GetMethod(OnlyIf).Invoke(validationContext.ObjectInstance, null);
               if (!Convert.ToBoolean(v)) return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }
            catch
            {
               throw new Exception("Error when call validate on server: " + OnlyIf);
            }
         }
         return base.IsValid(value, validationContext);
      }

      IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
      {
         var result = new ModelClientValidationEqualToRule(Language.T(ErrorMessage), OtherProperty);
         result.ValidationType = "equaltoif";
         if (!string.IsNullOrEmpty(OnlyIf))
         {
            result.ValidationParameters.Add("onlyif", OnlyIf);
         }
         yield return result;
      }
   }

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class EmailAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute, IClientValidatable
   {
      public EmailAttribute()
      {
         ErrorMessage = "Email không hợp lệ";
      }

      public override bool IsValid(object value)
      {
         var s = value as string;
         if (string.IsNullOrEmpty(s)) return true;
         string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
         + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
         + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

         var r = new Regex(validEmailPattern, RegexOptions.IgnoreCase);
         return r.IsMatch(s);
      }

      public override string FormatErrorMessage(string name)
      {
         return Language.T(ErrorMessage);
      }

      IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
      {
         var result = new ModelClientValidationRule();
         result.ErrorMessage = Language.T(ErrorMessage);
         result.ValidationType = "email";
         yield return result;
      }
   }

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class RangeAttribute : System.ComponentModel.DataAnnotations.RangeAttribute, IClientValidatable
   {
      public RangeAttribute(int min, int max) : base(min, max)
      {
         ErrorMessage = "Số trong phạm vi {0} - {1}";
      }

      public RangeAttribute(double min, double max) : base(min, max)
      {
         ErrorMessage = "Số trong phạm vi {0} - {1}";
      }

      public override string FormatErrorMessage(string name)
      {
         return Language.T(ErrorMessage, Minimum, Maximum);
      }

      IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
      {
         yield return new ModelClientValidationRangeRule(Language.T(ErrorMessage, Minimum, Maximum), Minimum, Maximum);
      }
   }

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class LengthAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute, IClientValidatable
   {
      public LengthAttribute(int length) : base(length)
      {
         ErrorMessage = "Tối đa {1} ký tự";
      }

      public LengthAttribute(int min, int max) : base(max)
      {
         MinimumLength = min;
         ErrorMessage = "Yêu cầu từ {0} đến {1} ký tự";
      }

      public override string FormatErrorMessage(string name)
      {
         return Language.T(ErrorMessage, MinimumLength, MaximumLength);
      }

      IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
      {
         yield return new ModelClientValidationStringLengthRule(Language.T(ErrorMessage, MinimumLength, MaximumLength), MinimumLength, MaximumLength);
      }
   }

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class RegexAttribute : System.ComponentModel.DataAnnotations.RegularExpressionAttribute, IClientValidatable
   {
      public RegexAttribute(string pattern, string message) : base(pattern)
      {
         ErrorMessage = message;
      }

      public override string FormatErrorMessage(string name)
      {
         return Language.T(ErrorMessage);
      }

      IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
      {
         yield return new ModelClientValidationRegexRule(Language.T(ErrorMessage), Pattern);
      }
   }

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class RemoteAttribute : System.Web.Mvc.RemoteAttribute, IClientValidatable
   {
      public RemoteAttribute(string action, string controller, string message) : base(action, controller)
      {
         ErrorMessage = message;
         HttpMethod = "POST";
      }

      public override string FormatErrorMessage(string name)
      {
         return Language.T(ErrorMessage);
      }


      IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
      {
         yield return new ModelClientValidationRemoteRule(Language.T(ErrorMessage), GetUrl(context), HttpMethod, AdditionalFields);
      }
   }

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class TemplateAttribute : System.ComponentModel.DataAnnotations.UIHintAttribute
   {
      public TemplateAttribute(string name) : base(name) { }
   }

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class CaptionAttribute : System.ComponentModel.DisplayNameAttribute
   {
      public CaptionAttribute(string name) : base(name) { }
   }
}