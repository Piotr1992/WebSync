USE [ERPXL_Rurex]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   proc [bmp].[b2b_divisionDataSynchronizedAndNot] @trueOrFalse bit, @bmpccta AS CDN.BMPC_ConfigTablesArchive READONLY 
as
if @trueOrFalse=1
BEGIN
	update cdn.BMPC_ProductGroupArchiveData 
		set isSynchronized=1
	from @bmpccta [cta]
		inner join cdn.BMPC_ProductGroupArchiveData [arch] on [cta].id = [arch].id and [cta].lastmod=[arch].lastmod;

	delete from cdn.BMPC_ProductGroupArchive;
	delete from cdn.BMPC_ProductGroupNamesArchive;
	delete from cdn.BMPC_ProductGroupCodesArchive;
	delete from cdn.BMPC_ProductGroupFilesArchive;
END

  

  




