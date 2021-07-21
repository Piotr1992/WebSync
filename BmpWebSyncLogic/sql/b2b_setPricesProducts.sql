create or alter procedure [bmp].[b2b_setStatusWarehouseProducts] @id int
AS
select Twr_GIDNumer product_id
	,cast(isnull((select TwC_Wartosc from cdn.TwrCeny where TwC_TwrNumer=Twr_GIDNumer and TwC_TwrLp in (1) and TwC_DataOd<>0), 0) as decimal(10, 2)) price_catalog
	,cast(isnull((select TwC_Wartosc from cdn.TwrCeny where TwC_TwrNumer=Twr_GIDNumer and TwC_TwrLp=2 and TwC_DataOd<>0), 0) as decimal(10, 2)) price_promotion
	,cast(isnull((select TwC_Wartosc from cdn.TwrCeny where TwC_TwrNumer=Twr_GIDNumer and TwC_TwrLp=3 and TwC_DataOd<>0), 0) as decimal(10, 2)) price_sale
	,Twr_StawkaPodSpr vat
from cdn.TwrKarty
where Twr_GIDNumer = @id
