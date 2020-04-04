
----Template Default data------
Insert Into CRT_Template(TemplateID,ClientID,InstanceID,TemplateName,Tier2,Tier3,incidentdate,policystatus,Accnumber,Rootcause,Idnumber,howcomreceived,contactno,Compregulatory
,emailaddress,feedbackregulatory,productcategory,Overalloutcome,Producttype,Compensation,Compcategory,Regulatedcost,Nature,Value,TCF,DissatisfactionLevel,SatisfactionResolution,IsTemplate,IsActive)
values(newid(),null,null,'Default CRT','Tier 2','Tier 3','Incident Date','policy Status','Account Number'
,'Root Cause','Id number','How complaint received','contact no','Complaint from regulatory body','email address','Feedback from regulatory body','Product category'
,'Overall outcome','Product type','Compensation','Complaint category','Regulated cost','Nature of complaint','Value','TCF Outcome','Level of dissatisfaction','Level of satisfaction after resolution communicated',1,1)
---------------------------------------

----Division Type----------------

Insert Into mst_ProjectTermCategory(ProjectTermcategoryID,CategoryName,IsDefault,IsActive)Values(newid(),'Division Type',1,1)


insert Into mst_projectterm(TermID,TermCode,Term,TermCategory,IsDefault,IsActive,ProjectTermcategoryID)
Values(newid(),null,'Life','Division Type',1,1,'84FC9514-9F76-41D5-9918-398753975727')
insert Into mst_projectterm(TermID,TermCode,Term,TermCategory,IsDefault,IsActive,ProjectTermcategoryID)
Values(newid(),null,'Non-Life','Division Type',1,1,'84FC9514-9F76-41D5-9918-398753975727')
insert Into mst_projectterm(TermID,TermCode,Term,TermCategory,IsDefault,IsActive,ProjectTermcategoryID)
Values(newid(),null,'Not applicable','Division Type',1,1,'84FC9514-9F76-41D5-9918-398753975727')
insert Into mst_projectterm(TermID,TermCode,Term,TermCategory,IsDefault,IsActive,ProjectTermcategoryID)
Values(newid(),null,'Others','Division Type',1,1,'84FC9514-9F76-41D5-9918-398753975727')
---------------

Alter table usr_UserRole add ClientID uniqueidentifier
Alter table usr_UserRole add CompanyID uniqueidentifier
Alter table usr_UserRole add DivisionID uniqueidentifier	

----------

--7F0F32A3-4599-48E9-9360-0E00A6EE3A95  = jignesh.gohil@isoftnetech.com  =  Level 1
--F65BF909-4669-4503-8494-156CF1A969B1  = Brilliance  = BU 
--0FEECD7A-0D7C-45C0-A9D2-4EC76FD3EA18  = Akash = level 3
--82082F04-F182-412C-A0FB-8320C6FC43C2  = Neha.mordia@isoftnetech.com =  Admin user
--25F5F426-9194-4476-8B06-925E7831EB18  = dave.choudhury@isoftnetech.com  =Super Admin
--B6EA9E46-65E4-43BC-82DF-A76FC2CB0024  = Bhumika = level 2

--C32C8B5F-44E9-43BA-8D84-16E86ECB972B-CRT Portal reports
--DB92974F-4613-45FE-A26F-1C56FC2059C6-Drop-select configuration
--4FE962A1-6404-4B3B-8E5B-45B86F7022C6-Field Label configuration
--D328B7D1-C872-403B-87B0-DC43812DA75A-CRT Admin
--D70E0DC4-2DA3-4050-9399-80361D0A85E1-User
--9369A2AF-3354-4559-B9FD-1F78F11A451A-Division
--5E9AA569-2D4E-4776-BAA8-24F9D9816EF5-Company
--54D425FB-6066-436C-9571-170812DE4861-Client Setup
--9542E046-7587-4718-B069-EC092FDE4F27-Complaints Reporting Tool
--7738DEBC-3CE8-4F05-B6C9-6E343A600D54-Dashboard

---
Update usr_UserRole set ClientID='AE36B531-4181-4EF8-900C-3F8A4F14D249',CompanyID=null,DivisionID=null where UserID='25F5F426-9194-4476-8B06-925E7831EB18' And RoleID='0BF4C837-2C69-4559-A96D-F3E307797736'
Update usr_UserRole set ClientID='AE36B531-4181-4EF8-900C-3F8A4F14D249',CompanyID=null,DivisionID=null where UserID='82082F04-F182-412C-A0FB-8320C6FC43C2' And RoleID='68C1BAA6-6AD8-41D6-B591-9DE0DAC103A4'
Update usr_UserRole set ClientID='AE36B531-4181-4EF8-900C-3F8A4F14D249',CompanyID=null,DivisionID=null where UserID='7F0F32A3-4599-48E9-9360-0E00A6EE3A95' And RoleID='0EDB7FB4-6F41-4503-92E0-D2B150648DB5'
Update usr_UserRole set ClientID='AE36B531-4181-4EF8-900C-3F8A4F14D249',CompanyID='FF6BE25E-775A-AFA6-15C4-024FFBF5A504',DivisionID=null where UserID='B6EA9E46-65E4-43BC-82DF-A76FC2CB0024' And RoleID='B8EDB4B0-B622-4827-9C34-0B50FB40A95A'
Update usr_UserRole set ClientID='AE36B531-4181-4EF8-900C-3F8A4F14D249',CompanyID='FF6BE25E-775A-AFA6-15C4-024FFBF5A504',DivisionID='277E4972-7139-9F79-42F9-72534C1364AB' where UserID='0FEECD7A-0D7C-45C0-A9D2-4EC76FD3EA18' And RoleID='DEB075B0-4CCB-46F0-B79D-A4729DFC1C58'

select * from usr_Previllage where MenuName='Client Setup' 

Insert Into usr_Previllage(PrevillageID,MenuName,MenuTitle,ParentMenuID,DisplayName,IsActive,IsDefault)
Values(newid(),'CRT Admin','CRT Admin',NULL,'CRT Admin',1,1)

Insert Into usr_Previllage(PrevillageID,MenuName,MenuTitle,ParentMenuID,DisplayName,IsActive,IsDefault)
Values(newid(),'Field Label configuration','Field Label configuration',NULL,'Field Label configuration',1,1)

Insert Into usr_Previllage(PrevillageID,MenuName,MenuTitle,ParentMenuID,DisplayName,IsActive,IsDefault)
Values(newid(),'Drop-select configuration','Drop-select configuration',NULL,'Drop-select configuration',1,1)

Insert Into usr_Previllage(PrevillageID,MenuName,MenuTitle,ParentMenuID,DisplayName,IsActive,IsDefault)
Values(newid(),'CRT Portal reports','CRT Portal reports',NULL,'CRT Portal reports',1,1)
Insert Into usr_Previllage(PrevillageID,MenuName,MenuTitle,ParentMenuID,DisplayName,IsActive,IsDefault)
Values(newid(),'Organisation admin','Organisation admin',NULL,'Organisation admin',1,1)

update usr_RolePrevillages set Isenable=0
select *from usr_RolePrevillages where RoleID='0BF4C837-2C69-4559-A96D-F3E307797736'

-----insert user role right data--
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','C32C8B5F-44E9-43BA-8D84-16E86ECB972B',NULL,1,1,0,0,0)
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','DB92974F-4613-45FE-A26F-1C56FC2059C6',NULL,1,1,0,0,0)
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','4FE962A1-6404-4B3B-8E5B-45B86F7022C6',NULL,1,1,0,0,0)
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','D328B7D1-C872-403B-87B0-DC43812DA75A',NULL,1,1,1,1,1)
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','D70E0DC4-2DA3-4050-9399-80361D0A85E1',NULL,1,1,1,1,1)
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','9369A2AF-3354-4559-B9FD-1F78F11A451A',NULL,1,1,1,1,1)
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','5E9AA569-2D4E-4776-BAA8-24F9D9816EF5',NULL,1,1,1,1,1)
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','54D425FB-6066-436C-9571-170812DE4861',NULL,1,1,1,1,1)
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','9542E046-7587-4718-B069-EC092FDE4F27',NULL,1,1,1,1,1)
insert into usr_RolePrevillages(RolePrevillageID,RoleID,PrevillageID,ClientID,IsEnable,IsView,IsCreate,IsUpdate,IsDelete)
values(newid(),'0BF4C837-2C69-4559-A96D-F3E307797736','57D26994-B1E5-4CA2-9CA8-65B166A2D0FD',NULL,1,1,1,1,1)

------------------------
update  usr_RolePrevillages set isview=0,iscreate=0,isupdate=0,Isdelete=0 where isenable=1 And previllageid='54D425FB-6066-436C-9571-170812DE4861'


--label config--
--name of the organisation
--change label name defaullt in db
---
update CRT_Template set                  
  Tier2                = 'Tier 2'
,Tier3				   = 'Tier 3'
,incidentdate		   = 'Complaint incident date'
,policystatus		   = 'Policy status'
,Accnumber			   = 'Account number'
,Rootcause			   = 'Root cause'
,Idnumber			   = 'ID number'
,howcomreceived		   = 'How complaint received'
,contactno			   = 'Contact number'
,Compregulatory		   = 'Complaint from regulatory body'
,emailaddress		   = 'Email address'
,feedbackregulatory	   = 'Feedback from regulatory body'
,productcategory	   = 'Product category'
,Overalloutcome		   = 'Overall outcome'
,Producttype		   = 'Product type'
,Compensation		   = 'Compensation/Payment'
,Compcategory		   = 'Complaint category'
,Regulatedcost		   = 'Regulated costs'
,Nature				   = 'Nature of complaint'
,Value				   = 'Value'
,TCF				   = 'TCF outcome'
,DissatisfactionLevel  = 'Level of dissatisfaction'
,SatisfactionResolution= 'Satisfaction post resolution'
where TemplateID='4F50D4F0-D3D0-4E90-8341-CBBEC67F007B'

---------------
--left menu
--clientsetup(/ClientSetup/Client)
--orgAdmin(/OrganisationAdmin/OrganisationList)
--CRTAdmin(/AdmCrt/CRTadmin)
--dashboard(/Dashboard/Dashboard)
--complaints(/CRT/CRTList)

---inner page--
--Company(/AdmCompany/Company/)
--division(/AdmDivision/Division)
--Reports(/CRT/CRTReports)

---------------
insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Complaint Types','Level 3',1)
insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Complaint Notes Report','Level 3',1)
insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Complaint Data without Notes','Level 3',1)
insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Complaint Turnaround Time','Level 3',1)

insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Complaint Types rollup','Level 2',1)
insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Complaint data rollup','Level 2',1)
insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Complaint Turnaround Time rollup','Level 2',1)

insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Level1 Complaint Types rollup','Level 1',1)
insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Level1 Complaint data rollup','Level 1',1)
insert into mst_ReportType(ReportTypeID,ReportTypeName,RoleName,IsActive)Values(
newid(),'Level1 Complaint Turnaround Time rollup','Level 1',


