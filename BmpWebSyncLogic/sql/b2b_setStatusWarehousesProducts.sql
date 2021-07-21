create or alter procedure [bmp].[b2b_setStatusWarehousesProducts]
as
select Twr_GIDNumer, Twr_Stan from dbo.b2b_stany( 0 )