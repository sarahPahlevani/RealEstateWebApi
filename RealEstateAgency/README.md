## for db first migration 
#### run this command in DAL project
Scaffold-DbContext -Connection "Server=.;Database=RealEstateDbLocal;Integrated Security=True;Trusted_Connection=True;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -context RealEstateDbContext -Project RealEstateAgency.DAL -force

#### sql server db
Scaffold-DbContext -Connection "Server=178.32.186.120,65210;Database=RealEstateDb;User Id=sa;Password=alt.321;Integrated Security=True;Trusted_Connection=True;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -context RealEstateDbContext -Project RealEstateAgency.DAL -force

#### sql server test db
Scaffold-DbContext -Connection "Server=178.32.186.120,65210;Database=RealEstateDbTest;User Id=sa;Password=alt.321;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -context RealEstateDbContext -Project RealEstateAgency.DAL -force


#### remove all the datas
```sql

delete BasicInformation.ImportantPlaceTypeTranslate
delete BasicInformation.PropertyFeatureTranslate
delete BasicInformation.PropertyLabelTranslate
delete BasicInformation.PropertyStatusTranslate
delete BasicInformation.PropertyTypeTranslate


delete Estate.PropertyAdditionalDetail
delete Estate.PropertyDetail
delete Estate.PropertyInvolveFeature
delete Estate.PropertyPrice
delete Estate.PropertyLocation
delete Estate.PropertyAttachment
delete Estate.PropertyFloorPlan
delete Estate.PropertyImage
delete Estate.Property

update CRM.RequestAction set RequestActionFollowUp_Reference = NULL 
delete CRM.RequestActionFollowUp
delete CRM.RequestAction
delete CRM.RequestAgent
delete CRM.Request
```