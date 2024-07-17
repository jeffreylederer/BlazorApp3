CREATE procedure [dbo].[TeamAllowDelete]
@leagueid int
as
SELECT T.[id]
      ,m1.fullname as [skip]
      ,m2.fullname as [ViceSkip]
      ,m3.Fullname as [Lead]
      ,T.[Leagueid]
	  ,t.DivisionId as Division
      ,[TeamNo]
	  ,m5.cnt
  FROM [dbo].[Team] t
  outer apply (select count(*) as cnt from match m 
	left outer join team t1 on t1.id = m.TeamNo1 or t1.id = m.TeamNo2
	where t1.id = t.id and t1.Leagueid=@leagueid) m5
  left outer join player p1 on p1.id = t.[skip]
  left outer join player p2 on p2.id = t.ViceSkip
  left outer join player p3 on p3.id = t.[lead]
  left outer join membership m1 on m1.id = p1.MembershipId
  left outer join membership m2 on m2.id = p2.MembershipId
  left outer join membership m3 on m3.id = p3.MembershipId
  where t.Leagueid=@leagueid
  order by teamno
