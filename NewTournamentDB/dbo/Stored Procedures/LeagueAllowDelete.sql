CREATE procedure [dbo].[LeagueAllowDelete]
as
SELECT L.[id]
      ,L.[LeagueName]
      ,L.[Active]
      ,L.[TeamSize]
      ,L.[rowversion]
      ,L.[TiesAllowed]
      ,L.[PointsCount]
      ,L.[WinPoints]
      ,L.[TiePoints]
      ,L.[ByePoints]
      ,L.[StartWeek]
	  ,L.PointsLimit
	  ,L.Divisions
	  ,L.PlayOffs
	  ,S1.cnt
  FROM [dbo].[League] L
  outer apply (select count(*) as cnt from schedule S
  left outer join League l1 on l1.id = S.Leagueid where l1.id  = l.id) S1
  order by LeagueName