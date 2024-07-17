/****** Script for SelectTopNRows command from SSMS  ******/
CREATE procedure dbo.MembershipAllowDelete
as
SELECT  [id]
      ,[FirstName]
      ,[LastName]
      ,[FullName]
      ,[shortname]
      ,[NickName]
      ,[Wheelchair]
	  ,P1.cnt
  FROM [dbo].[Membership] M
  outer apply (select count(*) as cnt from player p 
  left outer join membership m1 on m1.id=p.[MembershipId]
  where m1.id = m.id) P1