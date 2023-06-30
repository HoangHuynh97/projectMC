﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace WebModule.MCNV.DataModel
{

    /// <summary>
    /// Hồ sơ PHCN
    /// </summary>
    public partial class Medical : CompanyObject
    {
        string fCode;
        [ColumnDefaultValue("")]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>(nameof(Code), ref fCode, value); }
        }
        Patient fPatient;
        [ExplicitLoading(1)]
        public Patient Patient
        {
            get { return fPatient; }
            set { SetPropertyValue<Patient>(nameof(Patient), ref fPatient, value); }
        }
        Area fArea;
        [ExplicitLoading(1)]
        public Area Area
        {
            get { return fArea; }
            set { SetPropertyValue<Area>(nameof(Area), ref fArea, value); }
        }
        Facility fFacility;
        [ExplicitLoading(1)]
        public Facility Facility
        {
            get { return fFacility; }
            set { SetPropertyValue<Facility>(nameof(Facility), ref fFacility, value); }
        }
        string fEvaluation;
        [Size(SizeAttribute.Unlimited)]
        [ColumnDefaultValue("")]
        public string Evaluation
        {
            get { return fEvaluation; }
            set { SetPropertyValue<string>(nameof(Evaluation), ref fEvaluation, value); }
        }
        EvaluationTool fEvaluationTool;
        [ExplicitLoading(1)]
        [ColumnDefaultValue("")]
        public EvaluationTool EvaluationTool
        {
            get { return fEvaluationTool; }
            set { SetPropertyValue<EvaluationTool>(nameof(EvaluationTool), ref fEvaluationTool, value); }
        }
        DateTime? fEvaluationDate;
        public DateTime? EvaluationDate
        {
            get { return fEvaluationDate; }
            set { SetPropertyValue<DateTime?>(nameof(EvaluationDate), ref fEvaluationDate, value); }
        }
        MedicalStatus fStatus;
        [ExplicitLoading(1)]
        public MedicalStatus Status
        {
            get { return fStatus; }
            set { SetPropertyValue<MedicalStatus>(nameof(Status), ref fStatus, value); }
        }
        DateTime? fCompletedDate;
        public DateTime? CompletedDate
        {
            get { return fCompletedDate; }
            set { SetPropertyValue<DateTime?>(nameof(CompletedDate), ref fCompletedDate, value); }
        }
        MedicalCancel fCancel;
        [ExplicitLoading(1)]
        public MedicalCancel Cancel
        {
            get { return fCancel; }
            set { SetPropertyValue<MedicalCancel>(nameof(Cancel), ref fCancel, value); }
        }
        string fNote;
        [Size(SizeAttribute.Unlimited)]
        [ColumnDefaultValue("")]
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue<string>(nameof(Note), ref fNote, value); }
        }
        string fEvaluationPoint;
        public string EvaluationPoint
        {
            get { return fEvaluationPoint; }
            set { SetPropertyValue<string>(nameof(EvaluationPoint), ref fEvaluationPoint, value); }
        }
        string fEndEvaluation;
        [Size(SizeAttribute.Unlimited)]
        public string EndEvaluation
        {
            get { return fEndEvaluation; }
            set { SetPropertyValue<string>(nameof(EndEvaluation), ref fEndEvaluation, value); }
        }
        EvaluationTool fEndEvaluationTool;
        [ExplicitLoading(1)]
        public EvaluationTool EndEvaluationTool
        {
            get { return fEndEvaluationTool; }
            set { SetPropertyValue<EvaluationTool>(nameof(EndEvaluationTool), ref fEndEvaluationTool, value); }
        }
        string fEndEvaluationPoint;
        public string EndEvaluationPoint
        {
            get { return fEndEvaluationPoint; }
            set { SetPropertyValue<string>(nameof(EndEvaluationPoint), ref fEndEvaluationPoint, value); }
        }
        bool? fEndSuccess;
        public bool? EndSuccess
        {
            get { return fEndSuccess; }
            set { SetPropertyValue<bool?>(nameof(EndSuccess), ref fEndSuccess, value); }
        }
        string fTreatmentsStr;
        [Size(SizeAttribute.Unlimited)]
        public string TreatmentsStr
        {
            get { return fTreatmentsStr; }
            set { SetPropertyValue<string>(nameof(TreatmentsStr), ref fTreatmentsStr, value); }
        }
        string fDiagnosticsStr;
        [Size(SizeAttribute.Unlimited)]
        public string DiagnosticsStr
        {
            get { return fDiagnosticsStr; }
            set { SetPropertyValue<string>(nameof(DiagnosticsStr), ref fDiagnosticsStr, value); }
        }
        MedicalReason fReason;
        [ExplicitLoading(1)]
        public MedicalReason Reason
        {
            get { return fReason; }
            set { SetPropertyValue<MedicalReason>(nameof(Reason), ref fReason, value); }
        }
        string fPlanGoal;
        [Size(SizeAttribute.Unlimited)]
        public string PlanGoal
        {
            get { return fPlanGoal; }
            set { SetPropertyValue<string>(nameof(PlanGoal), ref fPlanGoal, value); }
        }
        DateTime? fPlanDate;
        public DateTime? PlanDate
        {
            get { return fPlanDate; }
            set { SetPropertyValue<DateTime?>(nameof(PlanDate), ref fPlanDate, value); }
        }
        string fPlanNote;
        [Size(SizeAttribute.Unlimited)]
        public string PlanNote
        {
            get { return fPlanNote; }
            set { SetPropertyValue<string>(nameof(PlanNote), ref fPlanNote, value); }
        }
        string fServicesStr;
        [Size(SizeAttribute.Unlimited)]
        public string ServicesStr
        {
            get { return fServicesStr; }
            set { SetPropertyValue<string>(nameof(ServicesStr), ref fServicesStr, value); }
        }
        DateTime fLastDate;
        public DateTime LastDate
        {
            get { return fLastDate; }
            set { SetPropertyValue<DateTime>(nameof(LastDate), ref fLastDate, value); }
        }
        WebApp.DataModel.User fLastUser;
        public WebApp.DataModel.User LastUser
        {
            get { return fLastUser; }
            set { SetPropertyValue<WebApp.DataModel.User>(nameof(LastUser), ref fLastUser, value); }
        }
        string fExplanation;
        [Size(SizeAttribute.Unlimited)]
        public string Explanation
        {
            get { return fExplanation; }
            set { SetPropertyValue<string>(nameof(Explanation), ref fExplanation, value); }
        }
        [Association(@"MedicalEvaluationAttachments", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Attachment> EvaluationAttachments { get { return GetCollection<Attachment>(nameof(EvaluationAttachments)); } }
        [ExplicitLoading(1)]
        [Association(@"MedicalTreatments", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Treatment> Treatments { get { return GetCollection<Treatment>(nameof(Treatments)); } }
        [Association(@"MedicalCompletedAttachments", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Attachment> CompletedAttachments { get { return GetCollection<Attachment>(nameof(CompletedAttachments)); } }
        [Association(@"MedicalDiagnostics", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Diagnostic> Diagnostics { get { return GetCollection<Diagnostic>(nameof(Diagnostics)); } }
        [Association(@"MedicalDoctors", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Doctor> Doctors { get { return GetCollection<Doctor>(nameof(Doctors)); } }
        [Association(@"MedicalEndDoctors", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Doctor> EndDoctors { get { return GetCollection<Doctor>(nameof(EndDoctors)); } }
        [Association(@"MedicalPlanAttachments", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Attachment> PlanAttachments { get { return GetCollection<Attachment>(nameof(PlanAttachments)); } }
        [Association(@"MedicalPlanDoctors", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Doctor> PlanDoctors { get { return GetCollection<Doctor>(nameof(PlanDoctors)); } }
        [ExplicitLoading(1)]
        [Association(@"MedicalServices", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Service> Services { get { return GetCollection<Service>(nameof(Services)); } }
        [Association(@"MedicalPlanReferencesMedical")]
        public XPCollection<MedicalPlan> MedicalPlans { get { return GetCollection<MedicalPlan>(nameof(MedicalPlans)); } }
        [Association(@"MedicalProcessReferencesMedical"), Aggregated]
        public XPCollection<MedicalProcess> Processes { get { return GetCollection<MedicalProcess>(nameof(Processes)); } }
    }

}
