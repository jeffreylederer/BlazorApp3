CREATE procedure [dbo].[ScheduleAllowDelete]
@leagueid int
as
	SELECT S.id
	 ,[GameDate]
      ,[Cancelled]
      ,[WeekDate]
	  ,PlayOffs
	  ,m1.cnt
  FROM [dbo].[Schedule] S
  outer apply (select count(*) as cnt from match m 
	left outer join [Schedule] S1 on S1.id = m.WeekId
	where S1.id = S.id and S1.Leagueid=@leagueid) m1
	where s.Leagueid = @leagueid
	order by [GameDate]