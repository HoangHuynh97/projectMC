update dbo.MedicalProcess set ValidData=0
update dbo.MedicalProcess set ValidData=1 where Status is not null

update dbo.MedicalProcess set AtFacility=0

update x set x.ProcessInd=x.Ind
from (
select Medical, ProcessInd, ROW_NUMBER() OVER (Partition By Medical Order by ProcessDate, DateCreate) as Ind
from dbo.MedicalProcess
where GCRecord is null and AtFacility=0
) x