insert into dbo.PatientDisability(Patients, Disabilities)
select distinct m.Patient, md.Disabilities
from dbo.MedicalDisabilities md inner join dbo.Medical m on md.Medicals=m.OID

update dbo.Patient set DisabilitiesStr = 
	SUBSTRING((Select '; ' + d.Name 
		from dbo.PatientDisability pd inner join dbo.Disability d on pd.Disabilities=d.OID 
		where pd.Patients=dbo.Patient.OID
		for xml path('')), 3, 1000) 
where GCRecord is null

update dbo.Medical set TreatmentsStr=null, Service=null where Status=5

delete dbo.MedicalTreatments
from dbo.MedicalTreatments mt inner join dbo.Medical m on mt.Medicals=m.OID
where m.Status=5

drop table dbo.MedicalDisabilities

alter table dbo.Medical
 drop column DisabilitiesStr