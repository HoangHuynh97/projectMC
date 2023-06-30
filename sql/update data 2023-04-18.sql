--Medical.Service to multi values
insert into dbo.MedicalServices (Medicals, Services)
select m.OID, m.Service
from dbo.Medical m
where m.GCRecord is null AND m.Service is not null

update dbo.Medical Set ServicesStr = s.Name
from dbo.Medical m inner join dbo.Service s on m.Service=s.OID
where m.GCRecord is null