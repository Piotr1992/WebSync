USE [ERPXL_Rurex]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create or alter procedure [CDN].[BMPC_Product]
as
DECLARE @productCurrent table(
	id int NOT NULL,
	idtype int NOT NULL,
	idlp int NOT NULL,
	name varchar(30),
	description nvarchar(4000),
	code varchar(15),
	symbol varchar(15),
	ean varchar(15),
	producer int,
	new int,
	promotion int,
	sale int,
	price_catalog decimal(10, 2),
	price_promotion decimal(10, 2),
	price_sale decimal(10, 2),
	vat decimal(5, 2),
	warehouse int,
	lastmod int
)

insert @productCurrent(idtype, id, idlp, name, description, lastmod) 
select Twr_GIDTyp, Twr_GIDNumer, Twr_GIDLp, Twr_Nazwa+isnull(' '+Twr_Nazwa1, ''), isnull(TwO_Opis, ''), Twr_LastModL
from cdn.TwrKarty left join cdn.TwrOpisy on TwO_TwrNumer=Twr_GIDNumer and TwO_TwrLp=1
where Twr_Archiwalny<>1 and Twr_GIDNumer<>0

update @productCurrent set code=isnull( (select top 1 twr_kod from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp), '')
update @productCurrent set symbol=isnull( (select top 1 Twr_Katalog from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp), '')
update @productCurrent set ean=isnull( (select top 1 twr_ean from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp), '')
update @productCurrent set producer=isnull( (select top 1 Twr_PrdNumer from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp), 0)
update @productCurrent set new=isnull( (select top 1 0 from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp), 0)
update @productCurrent set promotion=isnull( (select top 1 0 from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp), 0)
update @productCurrent set sale=isnull( (select top 1 0 from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp), 0)
update @productCurrent set price_catalog=isnull( (select top 1 cast(TwC_Wartosc as decimal(10,2)) from cdn.TwrCeny where TwC_TwrNumer=id and TwC_TwrTyp=idtype and TwC_TwrLp=1 and TwC_DataOd<>0) , 0.00)
update @productCurrent set price_promotion=isnull( (select top 1 cast(TwC_Wartosc as decimal(10,2)) from cdn.TwrCeny where TwC_TwrNumer=id and TwC_TwrTyp=idtype and TwC_TwrLp=2 and TwC_DataOd<>0) , 0.00)
update @productCurrent set price_sale=isnull( (select top 1 cast(TwC_Wartosc as decimal(10,2)) from cdn.TwrCeny where TwC_TwrNumer=id and TwC_TwrTyp=idtype and TwC_TwrLp=3 and TwC_DataOd<>0) , 0.00)
update @productCurrent set vat=isnull( ((select top 1 cast(Twr_StawkaPodSpr as decimal(10,2)) from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp)) , 0.00 )
update @productCurrent set warehouse=isnull( (select top 1 Twr_MagNumer from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp), 0)
update @productCurrent set lastmod=isnull( (select top 1 Twr_LastModL from cdn.TwrKarty where Twr_GIDNumer=id and Twr_GIDTyp=idtype and Twr_GIDLp=idlp), 0)

DECLARE @TempProductDifferences table(
	id int NOT NULL,
	idtype int NOT NULL,
	idlp int NOT NULL,
	name varchar(30),
	description nvarchar(4000),
	code varchar(15),
	symbol varchar(15),
	ean varchar(15),
	producer int,
	new int,
	promotion int,
	sale int,
	price_catalog decimal(10, 2),
	price_promotion decimal(10, 2),
	price_sale decimal(10, 2),
	vat decimal(5, 2),
	warehouse int,
	lastmod int
)

drop table if exists #tmpProductDifferences
select [current].* into #tmpProductDifferences from @productCurrent [current]
	where [current].lastmod NOT IN ( select [archive].lastmod FROM cdn.BMPC_ProductArchive [archive] 
										where [archive].id = [current].id )
insert into @TempProductDifferences select * from #tmpProductDifferences												  



DECLARE @productNamesCurrent table(
	id int null,
	pl varchar(30),
	en varchar(30),
	de varchar(30),
	lastmod int NOT NULL
)

insert into @productNamesCurrent
select id, 
	isnull( name, '' ),
	isnull( ( select TLM_Tekst from CDN.Tlumaczenia where TLM_Typ=idtype and TLM_Numer=id and TLM_Lp=idlp and tlm_pole=2 and TLM_Jezyk=703 ), '' ),
	isnull( ( select TLM_Tekst from CDN.Tlumaczenia where TLM_Typ=idtype and TLM_Numer=id and TLM_Lp=idlp and tlm_pole=2 and TLM_Jezyk=704 ), '' ),
	lastmod
from @productCurrent

DECLARE @TempProductNamesDifferences table(
	id int null,
	pl varchar(30),
	en varchar(30),
	de varchar(30),
	lastmod int NOT NULL
)

drop table if exists #tmpProductNamesDifferences
select * into #tmpProductNamesDifferences from @productNamesCurrent [current]
	where [current].lastmod NOT IN ( select [archive].lastmod from cdn.BMPC_ProductNamesArchive [archive]
										where [archive].id = [current].id )
insert into @TempProductNamesDifferences select * from #tmpProductNamesDifferences										



DECLARE @productDescriptionCurrent table(
	id int null,
	pl nvarchar(4000),
	en nvarchar(4000),
	de nvarchar(4000),
	lastmod int NOT NULL
)

insert into @productDescriptionCurrent
select id, 
	isnull( description, '' ),
	isnull( ( select TLM_Tekst from CDN.Tlumaczenia where TLM_Typ=idtype and TLM_Numer=id and TLM_Lp=idlp and tlm_pole=3 and TLM_Jezyk=703 ), '' ),
	isnull( ( select TLM_Tekst from CDN.Tlumaczenia where TLM_Typ=idtype and TLM_Numer=id and TLM_Lp=idlp and tlm_pole=3 and TLM_Jezyk=704 ), '' ),
	lastmod
from @productCurrent

DECLARE @TempProductDescriptionDifferences table(
	id int null,
	pl nvarchar(4000),
	en nvarchar(4000),
	de nvarchar(4000),
	lastmod int NOT NULL
)

drop table if exists #tmpProductDescriptionDifferences
select * into #tmpProductDescriptionDifferences from @productDescriptionCurrent [current]
	where [current].lastmod NOT IN ( select [archive].lastmod from cdn.BMPC_ProductDescriptionArchive [archive]
										where [archive].id = [current].id )
insert into @TempProductDescriptionDifferences select * from #tmpProductDescriptionDifferences										



DECLARE @productFilesCurrent table(
	files1 int NOT NULL,
	files2 int NOT NULL,
	lastmod int NOT NULL
)

insert into @productFilesCurrent
select id, DAB_ID, DAB_CzasModyfikacji from @productCurrent
join cdn.DaneObiekty on id=DAO_ObiNumer and DAO_ObiTyp=16
join cdn.DaneBinarne on dab_id=DAO_DABId

DECLARE @TempProductFilesDifferences table(
	files1 int NOT NULL,
	files2 int NOT NULL,
	lastmod int NOT NULL
)

drop table if exists #tmpProductFilesDifferences
select * into #tmpProductFilesDifferences from @productFilesCurrent [current]
	where [current].lastmod NOT IN ( select [archive].lastmod from cdn.BMPC_ProductFilesArchive [archive]
										where [archive].files2 = [current].files2 )
insert into @TempProductFilesDifferences select * from #tmpProductFilesDifferences										



DECLARE @productGroupsCurrent table(
	groups1 int NOT NULL,
	groups2 int NOT NULL,
	lastmod int NOT NULL
)

insert into @productGroupsCurrent
select TwL_GIDNumer, TwL_GrONumer, TwL_TStampDataMod from @productCurrent
join cdn.TwrLinki on TwL_GIDTyp=idtype and TwL_GIDNumer=id
where TwL_GrOTyp=-16 and TwL_GrONumer<>0

DECLARE @TempProductGroupsDifferences table(
	groups1 int NOT NULL,
	groups2 int NOT NULL,
	lastmod int NOT NULL
)

drop table if exists #tmpProductGroupsDifferences
select * into #tmpProductGroupsDifferences from @productGroupsCurrent [current]
where [current].lastmod NOT IN ( select [archive].lastmod from cdn.BMPC_ProductGroupsArchive [archive]
									where [archive].groups1 = [current].groups1 )
insert into @TempProductGroupsDifferences select * from #tmpProductGroupsDifferences



DECLARE @productFeaturesCurrent table(
	id int NOT NULL,
	feature_id int NOT NULL,
	value varchar(512) NOT NULL,
	view_order int NOT NULL,
	lastmod int NOT NULL
)

insert into @productFeaturesCurrent(id, feature_id, value, view_order, lastmod)
select 	id, Atr_AtkId, isnull( Atr_Wartosc, '' ), Atr_Pozycja, Atr_LastMod from @productCurrent
join cdn.Atrybuty on Atr_ObiNumer=id and Atr_ObiTyp=idtype

DECLARE @TempProductFeaturesDifferences table(
	id int NOT NULL,
	feature_id int NOT NULL,
	value varchar(512) NOT NULL,
	view_order int NOT NULL,
	lastmod int NOT NULL
)

drop table if exists #tmpProductFeaturesDifferences	
select * into #tmpProductFeaturesDifferences from @productFeaturesCurrent [current]
where [current].lastmod NOT IN ( select [archive].lastmod from cdn.BMPC_ProductFeaturesArchive [archive] 
									where [archive].feature_id = [current].feature_id )
insert into @TempProductFeaturesDifferences select * from #tmpProductFeaturesDifferences



DECLARE @productConnectionsCurrent table(
	id INT NOT NULL,
	product_id INT NOT NULL,
	connection_type_id INT NOT NULL,
	lastmod int NOT NULL
)

insert into @productConnectionsCurrent(id, product_id, connection_type_id, lastmod)
select id, 0, 0, lastmod from @productCurrent where 1=0

DECLARE @TempProductConnectionsDifferences table(
	id INT NOT NULL,
	product_id INT NOT NULL,
	connection_type_id INT NOT NULL,
	lastmod int NOT NULL
)

drop table if exists #tmpProductConnectionsDifferences
select * into #tmpProductConnectionsDifferences from @productConnectionsCurrent [current]
where [current].lastmod NOT IN ( select [archive].lastmod from cdn.BMPC_ProductConnectionsArchive [archive] 
									where [archive].id = [current].id )
insert into @TempProductConnectionsDifferences select * from #tmpProductConnectionsDifferences





/*		0 products		*/
select
	id,	code, symbol, ean, producer, new, promotion, sale, price_catalog, price_promotion, 
	price_sale, vat, warehouse
from @TempProductDifferences

/*		1 names			*/
select id, pl, en, de from @TempProductNamesDifferences

/*		2 description			*/
select id, pl, en, de from @TempProductDescriptionDifferences

/*		3 files		*/
select files1 id, files2 value from @TempProductFilesDifferences

/*		4 groups		*/
select groups1 id, groups2 value from @TempProductGroupsDifferences

/*		5 features		*/
select id, feature_id, value, view_order from @TempProductFeaturesDifferences

/*		6 connections	*/
select id, product_id, connection_type_id from @TempProductConnectionsDifferences












