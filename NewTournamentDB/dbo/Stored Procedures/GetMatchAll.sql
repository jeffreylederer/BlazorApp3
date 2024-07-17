CREATE procedure [dbo].[GetMatchAll]
@WeekId int
as
select L.* from (
select S.ID, M1.NickName as Skip1, m5.NickName as Vice1, M2.NickName as Lead1,  M3.NickName as Skip2, m6.NickName as Vice2,M4.NickName as Lead2, T1.TeamNo as Team1, T2.TeamNo as Team2, S.GameDate as Date, M.Rink, T1.DivisionId as Division1, T2.DivisionId as Division2
from [dbo].[Match] M
inner join [dbo].[Schedule] S on S.Id = M.WeekId
inner join [dbo].[Team] T1 on M.TeamNo1=T1.id
inner join [dbo].[Team] T2 on M.TeamNo2=T2.id
left outer join [dbo].[Player] P1 on p1.id=t1.[Skip]
left outer join [dbo].[Player] P2 on p2.id=t1.Lead
left outer join [dbo].[Player] P3 on p3.id=t2.[Skip]
left outer join [dbo].[Player] P4 on p4.id=t2.Lead
left outer join [dbo].[Player] P5 on p5.id=t1.ViceSkip
left outer join [dbo].[Player] P6 on p6.id=t2.ViceSkip

left outer join [dbo].[Membership] M1 on p1.membershipid = m1.id
left outer join [dbo].[Membership] M2 on p2.membershipid = m2.id
left outer join [dbo].[Membership] M3 on p3.membershipid = m3.id
left outer join [dbo].[Membership] M4 on p4.membershipid = m4.id
left outer join [dbo].[Membership] M5 on p5.membershipid = m5.id
left outer join [dbo].[Membership] M6 on p6.membershipid = m6.id

union

select S.ID, M3.NickName as Skip1, m6.NickName as Vice1, M4.NickName as Lead1,  M1.NickName as Skip2, m5.NickName as Vice2, M2.NickName as Lead2, T2.TeamNo as Team1, T1.TeamNo as Team2, S.GameDate as Date, M.Rink,T1.DivisionId as Division1, T2.DivisionId as Division2
from [dbo].[Match] M
inner join [dbo].[Schedule] S on S.Id = M.WeekId
inner join [dbo].[Team] T1 on M.TeamNo1=T1.id
inner join [dbo].[Team] T2 on M.TeamNo2=T2.id
left outer join [dbo].[Player] P1 on p1.id=t1.[Skip]
left outer join [dbo].[Player] P2 on p2.id=t1.Lead
left outer join [dbo].[Player] P3 on p3.id=t2.[Skip]
left outer join [dbo].[Player] P4 on p4.id=t2.Lead
left outer join [dbo].[Player] P5 on p5.id=t1.ViceSkip
left outer join [dbo].[Player] P6 on p6.id=t2.ViceSkip

left outer join [dbo].[Membership] M1 on p1.membershipid = m1.id
left outer join [dbo].[Membership] M2 on p2.membershipid = m2.id
left outer join [dbo].[Membership] M3 on p3.membershipid = m3.id
left outer join [dbo].[Membership] M4 on p4.membershipid = m4.id
left outer join [dbo].[Membership] M5 on p5.membershipid = m5.id
left outer join [dbo].[Membership] M6 on p6.membershipid = m6.id
) L
where l.id = @WeekId and l.Rink <> -1
order by l.Rink

-- exec [GetMatchAll] 3053
