create or alter proc bmp.b2b_productGroup
as
--drop table if exists #groups
/* order is important! */

/* groups */
select  
	TwG_GIDNumer id
	,nullif(twg_gronumer,-1) parent
into #groups
from cdn.TwrGrupy where TwG_GIDTyp=-16

select * from #groups

/* names */
select 
	id
	,'pl' lang
	,TwG_Nazwa value
from cdn.TwrGrupy join #groups on TwG_GIDNumer=id and TwG_GIDTyp=-16

/* coes */
select 
	id
	,'pl' lang
	,TwG_Kod value
from cdn.TwrGrupy join #groups on TwG_GIDNumer=id and TwG_GIDTyp=-16

