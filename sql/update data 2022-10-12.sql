update dbo.MedicalStatus set Name = N'Dừng can thiệp' where Oid=4
update dbo.MedicalStatus set Name = N'Kết thúc can thiệp' where Oid=3

update dbo.Service set OrderIndex=5
update dbo.Service set OrderIndex=0 where Oid=2
update dbo.Service set OrderIndex=1 where Oid=1
update dbo.Service set OrderIndex=2 where Oid=3
update dbo.Service set OrderIndex=3 where Oid=4
update dbo.Service set OrderIndex=4 where Oid=5
update dbo.Service set OrderIndex=99 where Oid=11

update dbo.ProcessStatus set Name=N'Chưa đạt mục tiêu', OrderIndex=0 where Oid=4
update dbo.ProcessStatus set OrderIndex=1 where oid=1
update dbo.ProcessStatus set Name=N'Đạt 1 phần mục tiêu', OrderIndex=2 where Oid=2
update dbo.MedicalProcess set Status=2 where Status=3
update dbo.Medical set ProcessStatus=2 where ProcessStatus=3
delete dbo.ProcessStatus where OID=3

update dbo.Specialize set Name=N'Điều dưỡng' where OID=4

update dbo.MedicalCancel set OrderIndex=0 where OID=3
update dbo.MedicalCancel set OrderIndex=1 where OID=2
update dbo.MedicalCancel set OrderIndex=2, Name=N'Từ chối can thiệp' where OID=4
update dbo.MedicalCancel set OrderIndex=3, Name=N'Bất hợp tác' where OID=1
insert into dbo.MedicalCancel (Name, OrderIndex) Values (N'Không đủ điều kiện sức khỏe', 4)
insert into dbo.MedicalCancel (Name, OrderIndex) Values (N'Chuyển chỗ ở', 5)
insert into dbo.MedicalCancel (Name, OrderIndex) Values (N'Khác', 6)

update dbo.Medical set EndSuccess=1 where ProcessStatus=1
update dbo.Medical set EndSuccess=0 where ProcessStatus!=1