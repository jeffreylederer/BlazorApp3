/****** Script for SelectTopNRows command from SSMS  ******/
CREATE procedure [dbo].[PlayerAllowDelete]
@leagueid int
as
SELECT P.[id]
	  ,T1.cnt
	  ,M.FullName
	  ,M.LastName
	  ,M.FirstName
  FROM [dbo].[Player] P
  outer apply (select count(*) as cnt  from team t 
  left outer join player P1 on p1.id = t.[skip] or p1.id = t.ViceSkip or p1.id = t.[Lead]
  where p.id = p1.id and t.Leagueid=@leagueid) T1
  left outer join membership m on m.id = p.MembershipId
  where Leagueid=@leagueid