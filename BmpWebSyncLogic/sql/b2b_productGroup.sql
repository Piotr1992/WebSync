USE [ERPXL_Rurex]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create or ALTER proc [bmp].[b2b_productGroup]
as
declare @productGroupCurrent table(
	id int NOT NULL, 
	parent int NULL,
	lastmod int NOT NULL
) 

insert into @productGroupCurrent(id, parent, lastmod)
select TwG_GIDNumer id, isnull(twg_gronumer,0) parent, TwG_CzasModyfikacji lastmod
from cdn.TwrGrupy
where TwG_GIDTyp=-16 and TwG_GIDNumer<>0

DECLARE @TempProductGroupDifferences table(
	idGroup int NOT NULL,
	parent int NULL,
	synchGroup int NOT NULL
);

insert into cdn.BMPC_ProductGroupArchive 
	select [cur].id id, isnull([cur].parent, 0) parent, [cur].lastmod lastmod from @productGroupCurrent [cur]
		where exists( select 1 from cdn.BMPC_ProductGroupArchiveData [arch] where [cur].id=[arch].id and [cur].lastmod=[arch].lastmod and [arch].isSynchronized=1 );

insert into @TempProductGroupDifferences
	select DISTINCT [current].id, isnull([current].parent, 0) parent, [current].lastmod from @productGroupCurrent [current]
		where [current].id NOT IN ( select [archive].id FROM cdn.BMPC_ProductGroupArchive [archive] );	

insert into @TempProductGroupDifferences
	select DISTINCT [current].id, isnull([current].parent, 0) parent, [current].lastmod from @productGroupCurrent [current]
		join cdn.BMPC_ProductGroupArchive [archive] on [archive].id = [current].id
			where [current].lastmod > [archive].lastmod and [current].parent <> isnull([archive].parent, 0)





declare @productGroupNamesCurrent table(
	id int NOT NULL,
	pl varchar(255) NULL, 
	en varchar(2048)  NULL, 
	de varchar(2048)  NULL, 
	lastmod int NOT NULL
) 

insert into @productGroupNamesCurrent(id, pl, en, de, lastmod) 
select
	id
	,TwG_Nazwa 'pl'
	,(select TLM_Tekst from CDN.Tlumaczenia where id=TLM_Numer and TLM_Typ=-16 and tlm_pole=2 /*name*/ and TLM_Jezyk=703 /*english*/) 'en'
	, (select TLM_Tekst from CDN.Tlumaczenia where id=TLM_Numer and TLM_Typ=-16 and tlm_pole=2 /*name*/ and TLM_Jezyk=704 /*deutsch*/) 'de'
	,isnull(TwG_CzasModyfikacji, '') lastmod
from cdn.TwrGrupy join @productGroupCurrent on TwG_GIDNumer=id and TwG_GIDTyp=-16

DECLARE @TempProductGroupNamesDifferences table(
	idNames int NOT NULL,
	plNames varchar(255) NULL, 
	enNames varchar(2048)  NULL, 
	deNames varchar(2048) NULL,
	synchNames int NOT NULL
)

insert into cdn.BMPC_ProductGroupNamesArchive 
	select [cur].id id, isnull([cur].pl, '') pl, isnull([cur].en, '') en, isnull([cur].de, '') de, [cur].lastmod lastmod from @productGroupNamesCurrent [cur]
		where exists( select 1 from cdn.BMPC_ProductGroupArchiveData [arch] where [cur].id=[arch].id and [cur].lastmod=[arch].lastmod and [arch].isSynchronized=1 );

insert into @TempProductGroupNamesDifferences	
	select DISTINCT [current].id, isnull([current].pl, '') pl, isnull([current].en, '') en, isnull([current].de, '') de, [current].lastmod from @productGroupNamesCurrent [current]
		where [current].id NOT IN ( select [archive].id FROM cdn.BMPC_ProductGroupNamesArchive [archive] );	

insert into @TempProductGroupNamesDifferences
		select DISTINCT [current].id, isnull([current].pl, '') pl, isnull([current].en, '') en, isnull([current].de, '') de, [current].lastmod from @productGroupNamesCurrent [current]
		join cdn.BMPC_ProductGroupNamesArchive [archive] on [archive].id = [current].id
			where [current].lastmod > [archive].lastmod and
				  [current].pl <> isnull([archive].pl, '') or
				  [current].en <> isnull([archive].en, '') or
				  [current].de <> isnull([archive].de, '');





declare @productGroupCodesCurrent table(
	id int NOT NULL,
	pl varchar(50) NULL, 
	en varchar(2048) NULL, 
	de varchar(2048) NULL, 
	lastmod int NOT NULL
) 

insert into @productGroupCodesCurrent(id, pl, en, de, lastmod)
select
	id
	,TwG_Kod 'pl'
	,(select TLM_Tekst from CDN.Tlumaczenia where id=TLM_Numer and TLM_Typ=-16 and tlm_pole=1 /*name*/ and TLM_Jezyk=703 /*english*/) 'en'
	,(select TLM_Tekst from CDN.Tlumaczenia where id=TLM_Numer and TLM_Typ=-16 and tlm_pole=1 /*name*/ and TLM_Jezyk=704 /*deutsch*/) 'de'
	,isnull(TwG_CzasModyfikacji, '') lastmod
from cdn.TwrGrupy
join @productGroupCurrent on TwG_GIDNumer=id and TwG_GIDTyp=-16

DECLARE @TempProductGroupCodesDifferences table(
	idCodes int NOT NULL,
	plCodes varchar(50) NULL, 
	enCodes varchar(2048) NULL, 
	deCodes varchar(2048) NULL,
	synchCodes int NOT NULL 
)

insert into cdn.BMPC_ProductGroupCodesArchive 
	select [cur].id id, isnull([cur].pl, '') pl, isnull([cur].en, '') en, isnull([cur].de, '') de, [cur].lastmod lastmod from @productGroupCodesCurrent [cur]
		where exists( select 1 from cdn.BMPC_ProductGroupArchiveData [arch] where [cur].id=[arch].id and [cur].lastmod=[arch].lastmod and [arch].isSynchronized=1 );

insert into @TempProductGroupCodesDifferences	
	select DISTINCT [current].id, isnull([current].pl, '') pl, isnull([current].en, '') en, isnull([current].de, '') de, [current].lastmod from @productGroupCodesCurrent [current]
		where [current].id NOT IN ( select [archive].id FROM cdn.BMPC_ProductGroupCodesArchive [archive] );	

insert into @TempProductGroupCodesDifferences
		select DISTINCT [current].id, isnull([current].pl, '') pl, isnull([current].en, '') en, isnull([current].de, '') de, [current].lastmod from @productGroupCodesCurrent [current]
			join cdn.BMPC_ProductGroupCodesArchive [archive] on [archive].id = [current].id
			where [current].lastmod > [archive].lastmod and
			[current].pl <> isnull([archive].pl, '') or
			[current].en <> isnull([archive].en, '') or
			[current].de <> isnull([archive].de, '');





declare @productGroupFilesCurrent table(
	id int NOT NULL,
	DAB_ID int NULL,
	lastmod int NOT NULL
) 

insert into @productGroupFilesCurrent(id, DAB_ID, lastmod)
select id, DAB_ID, isnull(lastmod, '') from @productGroupCurrent 
join cdn.DaneObiekty on id=DAO_ObiNumer and DAO_ObiTyp=-16
join cdn.DaneBinarne on dab_id=DAO_DABId

DECLARE @TempProductGroupFilesDifferences table(
	idFiles int NOT NULL,
	DAB_ID int NULL,	
	synchFiles int NOT NULL
)

insert into @TempProductGroupFilesDifferences
select DISTINCT [current].id, [current].DAB_ID, isnull([current].lastmod, '') from @productGroupFilesCurrent [current]
	where [current].DAB_ID NOT IN ( select [archive].DAB_ID FROM cdn.BMPC_ProductGroupFilesArchive [archive] );				

insert into @TempProductGroupFilesDifferences
	select DISTINCT [current].id, [current].DAB_ID, isnull([current].lastmod, '') from @productGroupFilesCurrent [current]
		left join cdn.BMPC_ProductGroupFilesArchive [archive] on [current].DAB_ID = [archive].DAB_ID
			where [current].lastmod > [archive].lastmod and [current].id <> isnull([archive].id, 0);





insert into cdn.BMPC_ProductGroupArchiveData(id, parent, lastmod, isSynchronized) 
	select [tmp].idGroup, isnull([tmp].parent, 0), [tmp].synchGroup, 0 from @TempProductGroupDifferences [tmp]
		where not exists( select 1 from cdn.BMPC_ProductGroupArchiveData [archieve] 
							where [tmp].idGroup=[archieve].id and [tmp].parent=[archieve].parent and [tmp].synchGroup=[archieve].lastmod );

insert into cdn.BMPC_ProductGroupArchiveData(id, plNames, enNames, deNames, lastmod, isSynchronized) 
	select [tmp].idNames, isnull([tmp].plNames, ''), isnull([tmp].enNames, ''), isnull([tmp].deNames, ''), [tmp].synchNames, 0 from @TempProductGroupNamesDifferences [tmp]
		where not exists( select 1 from cdn.BMPC_ProductGroupArchiveData [archieve] where [tmp].idNames=[archieve].id and [tmp].plNames=[archieve].plNames 
									and [tmp].enNames=[archieve].enNames and [tmp].deNames=[archieve].deNames and [tmp].synchNames=[archieve].lastmod );

insert into cdn.BMPC_ProductGroupArchiveData(id, plCodes, enCodes, deCodes, lastmod, isSynchronized) 
	select [tmp].idCodes, isnull([tmp].plCodes, ''), isnull([tmp].enCodes, ''), isnull([tmp].deCodes, ''), [tmp].synchCodes, 0 from @TempProductGroupCodesDifferences [tmp]
		where not exists( select 1 from cdn.BMPC_ProductGroupArchiveData [archieve] where [tmp].idCodes=[archieve].id and [tmp].plCodes=[archieve].plCodes 
									and [tmp].enCodes=[archieve].enCodes and [tmp].deCodes=[archieve].deCodes and [tmp].synchCodes=[archieve].lastmod );

insert into cdn.BMPC_ProductGroupArchiveData(id, DAB_ID, lastmod, isSynchronized) 
		select [tmp].idFiles, isnull([tmp].DAB_ID, 0), [tmp].synchFiles, 0 from @TempProductGroupFilesDifferences [tmp]
		where not exists( select 1 from cdn.BMPC_ProductGroupArchiveData [archieve] where [tmp].idFiles=[archieve].id and [tmp].DAB_ID=[archieve].DAB_ID
									and [tmp].synchFiles=[archieve].lastmod );





declare @idsToUpdate table (ids int);

insert into @idsToUpdate 
	select distinct id from 
		(
			select DISTINCT [tmp].idGroup id from @TempProductGroupDifferences [tmp] 
				inner join cdn.BMPC_ProductGroupArchiveData [arch] 
					on [tmp].idGroup = [arch].id and [tmp].synchGroup = [arch].lastmod 
						where [arch].isSynchronized=0
			union
			select DISTINCT [tmp].idNames id from @TempProductGroupNamesDifferences [tmp] 
				inner join cdn.BMPC_ProductGroupArchiveData [arch] 
					on [tmp].idNames = [arch].id and [tmp].synchNames = [arch].lastmod 
						where [arch].isSynchronized=0
			union
			select DISTINCT [tmp].idCodes id from @TempProductGroupCodesDifferences [tmp] 
				inner join cdn.BMPC_ProductGroupArchiveData [arch] 
					on [tmp].idCodes = [arch].id and [tmp].synchCodes = [arch].lastmod 
						where [arch].isSynchronized=0
			union
			select DISTINCT [tmp].idFiles id from @TempProductGroupFilesDifferences [tmp] 
				inner join cdn.BMPC_ProductGroupArchiveData [arch] 
					on [tmp].idFiles = [arch].id and [tmp].synchFiles = [arch].lastmod 
						where [arch].isSynchronized=0
		) v





select id, parent, lastmod from @productGroupCurrent join @idsToUpdate on ids=id

select id, pl, en, de, lastmod from @productGroupNamesCurrent join @idsToUpdate on ids=id

select id, pl, en, de, lastmod from @productGroupCodesCurrent join @idsToUpdate on ids=id

select id, DAB_ID value, lastmod from @productGroupFilesCurrent join @idsToUpdate on ids=id
