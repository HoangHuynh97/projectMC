
--update lastdate, lastuser
update dbo.Medical set LastDate=DateModify, LastUser=UserModify
GO

update dbo.Medical set LastDate=y.DateModify, LastUser=y.UserModify
from dbo.Medical x inner join (
	select a.Medical, a.DateModify, a.UserModify
	from dbo.MedicalProcess a inner join (
		select m.OID, MAX(mp.DateModify) as DateModify
		from dbo.Medical m inner join dbo.MedicalProcess mp on m.OID=mp.Medical
		where m.DateModify < mp.DateModify
		group by m.OID
	) b on a.Medical=b.OID and a.DateModify=b.DateModify
) y on x.OID=y.Medical

GO


--update Explanation
declare @expl table (Id int, Expl nvarchar(MAX))

insert into @expl(Id, Expl)
select m.OID, m.Note
from dbo.Medical m
where m.Note is not null

insert into @expl(Id, Expl)
select m.OID, mr.Name
from dbo.Medical m inner join dbo.MedicalReason mr on m.Reason=mr.OID
where m.Status=5

insert into @expl(Id, Expl)
select m.OID, mc.Name
from dbo.Medical m inner join dbo.MedicalCancel mc on m.Cancel=mc.OID
where m.Status=4

insert into @expl(Id, Expl)
select m.OID, m.EndEvaluation
from dbo.Medical m
where m.EndEvaluation is not null and m.Status != 4 and m.Status != 5

update dbo.Medical set Explanation = 
	SUBSTRING((Select N'; ' + a.Expl 
		from @expl a
		where a.Id=dbo.Medical.OID
		for xml path('')), 3, 1000) 
