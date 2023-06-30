--reupdate text in Medical.DiagnosticsStr 
update dbo.Medical set DiagnosticsStr = b.FixedName
from dbo.Medical a inner join 
(
	select m.OID, m.DiagnosticsStr,
		SUBSTRING(
		(
			select '; ' + d.Name as [text()]
			from dbo.MedicalDiagnostics md inner join dbo.Diagnostic d on md.Diagnostics=d.OID
			where md.Medicals = m.OID
			order by d.Code
			for xml path (''), TYPE
		).value('text()[1]', 'nvarchar(max)')
		, 2, 10000) as FixedName
	from dbo.Medical m 
) b
on a.OID=b.OID
