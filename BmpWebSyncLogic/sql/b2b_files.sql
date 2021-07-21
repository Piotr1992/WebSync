create or ALTER procedure [bmp].[b2b_files]
as
DECLARE @productCurrent table(
	id int NOT NULL,
	idtype int NOT NULL,
	idlp int NOT NULL,
	name varchar(30),
	description nvarchar(4000),
	lastmod int
)

insert into @productCurrent(idtype, id, idlp, name, description, lastmod) 
select 
	Twr_GIDTyp, Twr_GIDNumer, Twr_GIDLp, Twr_Nazwa+isnull(' '+Twr_Nazwa1, ''), isnull(TwO_Opis, ''), Twr_LastModL
from cdn.TwrKarty 
left join cdn.TwrOpisy on TwO_TwrNumer=Twr_GIDNumer and TwO_TwrLp=1
where Twr_Archiwalny<>1 and Twr_GIDNumer<>0

DECLARE @TempProductDifferences table(
	id int NOT NULL,
	idtype int NOT NULL,
	idlp int NOT NULL,
	name varchar(30),
	description nvarchar(4000),
	lastmod int
)

drop table if exists #tmpProductDifferences2
select [current].* into #tmpProductDifferences2 from @productCurrent [current]
	where [current].lastmod NOT IN ( select [archive].lastmod FROM cdn.BMPC_ProductAttachmentArchive [archive] 
										where [archive].id = [current].id )
insert into @TempProductDifferences select * from #tmpProductDifferences2





DECLARE @productFileCurrent table(
	id int NOT NULL
)

insert into @productFileCurrent
select 
	distinct
	dab_id id	
from @TempProductDifferences 
join cdn.DaneObiekty on id=DAO_ObiNumer and DAO_ObiTyp=16
join cdn.DaneBinarne d on dab_id=DAO_DABId
where DAB_DBGId>0 and DAB_PKPrawa=1



/* files */
select 
	dab_id id
	,'files/'+cast(DAB_ID as varchar(50))+'.'+DAB_Rozszerzenie path
	,dateadd(second,DAB_CzasModyfikacji,'1990-01-01T00:00:00.000') date
	,replace(DAB_Nazwa,' ','-')+'.'+DAB_Rozszerzenie name
	,type type
	,DAB_Dane dabdane
	,DAB_Rozmiar dabrozmiar	
from @productFileCurrent
join cdn.DaneBinarne d on dab_id=id
join bmp.b2b_filetypes on dbgid=DAB_DBGId





