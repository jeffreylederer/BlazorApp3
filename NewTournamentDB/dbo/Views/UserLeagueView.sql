CREATE View dbo.UserLeagueView
as
SELECT 
      u.id
      ,g.LeagueName
	  ,u.username
      ,l.[Roles] as [league role]
	  ,u.Roles as [site role]
	  ,g.active
  FROM [User] u
  left outer join [UserLeague] l on u.id=l.userid
  left outer join League g on g.id = l.LeagueId


  -- select * from UserLeagueView